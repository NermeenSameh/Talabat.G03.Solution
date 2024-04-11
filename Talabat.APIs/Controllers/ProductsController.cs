using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Repositories.Contract;

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
			var products = await _productRepo.GetAllAsync();

			return Ok( products );
		}
    }
}
