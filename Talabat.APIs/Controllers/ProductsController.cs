using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.Services.Contract;
using Route.Talabat.Core.Specifications.Product_Specs;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : BaseApiController
	{
		private readonly IProductService _productService;

		/// private readonly IGenericRepository<Product> _productRepo;
		/// private readonly IGenericRepository<ProductBrand> _brandRepo;
		/// private readonly IGenericRepository<ProductCategory> _categoryRepo;
		private readonly IMapper _mapper;

		public ProductsController(
			IProductService productService,
			/// IGenericRepository<Product> productRepo,
			/// IGenericRepository<ProductBrand> brandRepo,
			/// IGenericRepository<ProductCategory> categoryRepo,
			
			IMapper mapper
			)
		{
			/// _productRepo = productRepo;
			/// _brandRepo = brandRepo;
			/// _categoryRepo = categoryRepo;
		
			_productService = productService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
		{
			var products = await _productService.GetProductsAsync(specParams);
			
			var count = await _productService.GetCountAsync(specParams);
			
			var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

			return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, count,data));
		}
		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var product = await _productService.GetProductAsync(id);

			if (product is null)
				return NotFound(new ApiResponse(404));


			return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
		}


		[HttpGet("brands")] // Get : /api/Products/brands

		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await _productService.GetBrandsAsync();

			return Ok(brands);

		}


		[HttpGet("categories")] // Get : /api/Products/categories

		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
		{
			var categories = await _productService.GetCategoriesAsync();
			return Ok(categories);
		}



	}
}
