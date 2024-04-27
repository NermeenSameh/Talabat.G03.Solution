using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities.Baskets;
using Route.Talabat.Core.Repositories.Contract;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{

    public class BasketController : BaseApiController
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepository,
			IMapper mapper
			)
		{
			_basketRepository = basketRepository;
			_mapper = mapper;
		}

		[HttpGet] // GET: /api/basket/id

		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket = await _basketRepository.GetBasketAsync(id);
			return Ok(basket ?? new CustomerBasket(id));
		}

		[HttpPost] // POST : /api/basket
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
		{
			var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);

			var createdOrUpdated = await _basketRepository.UpdateBasketAsync(mappedBasket);

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
