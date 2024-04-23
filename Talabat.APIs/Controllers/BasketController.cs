using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Repositories.Contract;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{

	public class BasketController : BaseApiController
	{
		private readonly IBasketRepository _basketRepository;

		public BasketController(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
		}

		[HttpGet] // GET: /api/basket/id

		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket = await _basketRepository.GetBasketAsync(id);
			return Ok(basket ?? new CustomerBasket(id));
		}

		[HttpPost] // POST : /api/basket
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
		{
			var createdOrUpdated = await _basketRepository.UpdateBasketAsync(basket);

			if (createdOrUpdated is null) return BadRequest(new ApiResponse(400));

			return Ok(createdOrUpdated);

		}

		[HttpDelete] // DELETE : /api/basket
		public async Task DeleteBasket(string id)
		{
			await _basketRepository.DeleteBasketAsync(id);
		}

	}
}
