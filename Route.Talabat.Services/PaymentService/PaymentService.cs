using Microsoft.Extensions.Configuration;
using Route.Talabat.Core;
using Route.Talabat.Core.Entities.Baskets;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Services.Contract;
using Stripe;
using Product = Route.Talabat.Core.Entities.Product.Product;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities.Order_Aggregate;

namespace Route.Talabat.Service.PaymentService
{
	public class PaymentService : IPaymentService
	{
		private readonly IConfiguration _configuration;
		private readonly IBasketRepository _basketRepo;
		private readonly IUniteOfWork _uniteOfWork;

		public PaymentService(
			IConfiguration configuration,
			IBasketRepository basketRepo,
			IUniteOfWork uniteOfWork
			)
		{
			_configuration = configuration;
			_basketRepo = basketRepo;
			_uniteOfWork = uniteOfWork;
		}
		public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
		{
			StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

			var basket = await _basketRepo.GetBasketAsync(basketId);

			if (basket is null) return null;

			var shippingPrice = 0m;

			if (basket.DeliveryMethodId.HasValue)
			{
				var deliveryMethod = await _uniteOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);

				shippingPrice = deliveryMethod.Cost;
				basket.ShippingPrice = shippingPrice;
			}

			if (basket.Items?.Count > 0)
			{
				var ProductRepo = _uniteOfWork.Repository<Product>();

				foreach (var item in basket.Items)
				{

					var product = await ProductRepo.GetByIdAsync(item.Id);
					if (item.Price != product.Price)
						item.Price = product.Price;

				}



			}

			PaymentIntent paymentIntent;
			PaymentIntentService paymentIntentService = new PaymentIntentService();

			if (string.IsNullOrEmpty(basket.PaymentIntentId)) // Create New Payment Intent
			{
				var options = new PaymentIntentCreateOptions()
				{
					Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)shippingPrice * 100,
					Currency = "usd",
					PaymentMethodTypes = new List<string> { "card" }

				};
				paymentIntent = await paymentIntentService.CreateAsync(options);  // Integration With Stripe

				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClientSecret = paymentIntent.ClientSecret;

			}
			else  // Update Existing Payment Intent
			{
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = (long)basket.Items.Sum(item => item.Price * 100 * item.Quantity) + (long)shippingPrice * 100,
				};

				await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
			}

			await _basketRepo.UpdateBasketAsync(basket);
			
			return basket;
		}
	}
}
