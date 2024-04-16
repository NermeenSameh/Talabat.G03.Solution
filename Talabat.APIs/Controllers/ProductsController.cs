using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Specifications;
using Route.Talabat.Core.Specifications.Product_Specs;

namespace Talabat.APIs.Controllers
{
	
	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepo;

		public ProductsController(IGenericRepository<Product> productRepo )
        {
			_productRepo = productRepo;
		
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var spec = new ProductWithBrandAndCategorySpecifications();
			var products = await _productRepo.GetAllWithSpecAsync(spec);

			return Ok( products );
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);
			var product = await _productRepo.GetWithSpecAsync(spec);

			if( product is null ) 
				return NotFound(new {Massage = "Not FOund" , StatusCode = 404});


			return Ok( product );
		}
    }
}
