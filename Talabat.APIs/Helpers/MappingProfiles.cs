using AutoMapper;
using Route.Talabat.Core.Entities.Baskets;
using Route.Talabat.Core.Entities.Identity;
using Route.Talabat.Core.Entities.Order_Aggregate;
using Route.Talabat.Core.Entities.Product;
using Talabat.APIs.DTOs;
using Address = Route.Talabat.Core.Entities.Identity.Address;

namespace Talabat.APIs.Helpers
{
	public class MappingProfiles : Profile
	{

		public MappingProfiles()
		{

			CreateMap<Product, ProductToReturnDto>()
				.ForMember(d => d.Brands, O => O.MapFrom(s => s.Brands.Name))
				.ForMember(d => d.Category, O => O.MapFrom(s => s.Category.Name))
				//.ForMember(P => P.PictureUrl, O => O.MapFrom(S => $"{}/{S.PictureUrl}"))
				.ForMember(P => P.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());


			CreateMap<CustomerBasketDto, CustomerBasket>();
			CreateMap<BasketItemDto, BasketItem>();

			CreateMap<AddressDto, Address>().ReverseMap();

			CreateMap<Order, OrderToReturnDto>()
				.ForMember(d => d.DeliveryMethod, O => O.MapFrom(s => s.DeliveryMethod.ShortName))
				.ForMember(d => d.DeliveryMethodCost, O => O.MapFrom(c => c.DeliveryMethod.Cost));

			CreateMap<OrderItem, OrderItemDto>()
				.ForMember(d => d.ProductName, O => O.MapFrom(s => s.Product.ProductName))
				.ForMember(d => d.ProductId, O => O.MapFrom(s => s.Product.ProductId))
				.ForMember(d => d.PictureUrl, O => O.MapFrom(s => s.Product.PictureUrl))
				.ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());
		}
	}
}
