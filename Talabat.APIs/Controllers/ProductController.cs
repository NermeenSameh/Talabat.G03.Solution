using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Repositories.Contract;

namespace Talabat.APIs.Controllers
{
	
	public class ProductController : BaseApiController
	{
        public ProductController(IGenericRepository<Product> productRepo )
        {
            
        }

    }
}
