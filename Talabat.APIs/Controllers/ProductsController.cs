using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Specifications;
using Route.Talabat.Core.Specifications.Product_Specs;
using System.Collections.Generic;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IGenericRepository<ProductBrand> _brandRepo;
		private readonly IGenericRepository<ProductCategory> _categoryRepo;
		private readonly IMapper _mapper;

		public ProductsController(IGenericRepository<Product> productRepo,
			IGenericRepository<ProductBrand> brandRepo,
			IGenericRepository<ProductCategory> categoryRepo,
			IMapper mapper
			)
		{
			_productRepo = productRepo;
			_brandRepo = brandRepo;
			_categoryRepo = categoryRepo;
			_mapper = mapper;
		}

		[Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme )]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(specParams);

		
			var products = await _productRepo.GetAllWithSpecAsync(spec);

			
			var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

			
			var countSpec = new ProductWithFilterationForCountSpecifications(specParams);

			
			var count = await _productRepo.GetCountAsync(countSpec);

			return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, count,data));
		}
		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);
			var product = await _productRepo.GetWithSpecAsync(spec);

			if (product is null)
				return NotFound(new ApiResponse(404));


			return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
		}


		[HttpGet("brands")] // Get : /api/Products/brands

		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await _brandRepo.GetAllAsync();

			return Ok(brands);

		}


		[HttpGet("categories")] // Get : /api/Products/categories

		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
		{
			var categories = await _categoryRepo.GetAllAsync();
			return Ok(categories);
		}



	}
}
