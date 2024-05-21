using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities.Baskets;
using Route.Talabat.Core.Entities.Order_Aggregate;
using Route.Talabat.Core.Services.Contract;
using Stripe;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
	[Authorize]
	public class PaymentController : BaseApiController
	{
		private readonly IPaymentService _paymentService;
		private readonly ILogger<PaymentController> _logger;

		// This is your Stripe CLI webhook secret for testing ypur endpoint locally.
		const string WhSecret = "whsec_d293457753f93948a435fabe93117915ff5708303b23f452c32c0e27066e9fb3";

		public PaymentController(
			IPaymentService paymentService,
			ILogger<PaymentController> logger)
		{
			_paymentService = paymentService;
			_logger = logger;
		}
		[ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		[HttpPost("{basketid}")]
		public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
		{
			var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

			if (basket is null) return BadRequest(new ApiResponse(400, "An Error with your basket"));

			return Ok(basket);

		}



		[HttpPost("webhook")]
		public async Task<IActionResult> WebHook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

			var stripeEvent = EventUtility.ConstructEvent(json,
				Request.Headers["Stripe-Signature"], WhSecret);

			var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

			Order order;


			switch (stripeEvent.Type)
			{
				case Events.PaymentIntentSucceeded:
					order = await _paymentService.UpdateOrderStatus(paymentIntent.Id, true);

					_logger.LogInformation("Order Is Succeeded {0}", order?.PaymentIntenId);
					_logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);
					break;
				case Events.PaymentIntentPaymentFailed:
					order = await _paymentService.UpdateOrderStatus(paymentIntent.Id, false);

					_logger.LogInformation("Order Is Failed {0}", order?.PaymentIntenId);
					_logger.LogInformation("Unhandled event type: {0}", stripeEvent.Type);

					break;
			}


			return Ok();
		}

	}

}



