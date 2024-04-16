using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Specifications;
using Route.Talabat.Core.Specifications.Product_Specs;
using System.Collections.Generic;
using Talabat.APIs.DTOs;

namespace Talabat.APIs.Controllers
{

	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> productRepo, IMapper mapper)
		{
			_productRepo = productRepo;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
		{
			var spec = new ProductWithBrandAndCategorySpecifications();
			var products = await _productRepo.GetAllWithSpecAsync(spec);

			return Ok(_mapper.Map <IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);
			var product = await _productRepo.GetWithSpecAsync(spec);

			if (product is null)
				return NotFound(new { Massage = "Not FOund", StatusCode = 404 });


			return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
		}
	}
}
