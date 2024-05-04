using AutoMapper;
using Route.Talabat.Core.Entities.Baskets;
using Route.Talabat.Core.Entities.Identity;
using Route.Talabat.Core.Entities.Product;
using Talabat.APIs.DTOs;

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
				.ForMember(P =>P.PictureUrl , O =>O.MapFrom<ProductPictureUrlResolver>());


			CreateMap<CustomerBasketDto, CustomerBasket>();
			CreateMap<BasketItemDto, BasketItem>();

			CreateMap<Address , AddressDto>().ReverseMap();
		}
	}
}
