using AutoMapper;
using Route.Talabat.Core.Entities;
using Talabat.APIs.DTOs;

namespace Talabat.APIs.Helpers
{
	public class MappingProfiles : Profile
	{

		public MappingProfiles()
		{
			CreateMap<Product, ProductToReturnDto>()
				.ForMember(d => d.Brands, O => O.MapFrom(s => s.Brands.Name))
				.ForMember(d => d.Category, O => O.MapFrom(s => s.Category.Name));


		}
	}
}
