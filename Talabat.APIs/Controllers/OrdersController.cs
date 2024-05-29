using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities.Order_Aggregate;
using Route.Talabat.Core.Services.Contract;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
	//[ApiExplorerSettings(IgnoreApi = true)]
	[Authorize]
	public class OrdersController : BaseApiController
	{
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;

		public OrdersController(IOrderService orderService,
			IMapper mapper
			)
		{
			_orderService = orderService;
			_mapper = mapper;
		}

		[ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
		[HttpPost] // POST : /api/Orders

		public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDTo orderDto)
		{
			var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);

			var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);

			if (order is null) return BadRequest(new ApiResponse(400));

			return Ok(_mapper.Map<Order , OrderToReturnDto>(order));

		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser(string email)
		{
			var orders = await _orderService.GetOrdersForUserAsync(email);	

			return Ok(_mapper.Map <IReadOnlyList<Order> , IReadOnlyList<OrderToReturnDto>>(orders));
		}

		[ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id , string email)
		{
			var order = await _orderService.GetOrderByIdForUserAsync(id, email);

			if (order is null) return NotFound(new ApiResponse(404));

			return Ok(_mapper.Map<OrderToReturnDto>(order));
		}


		
		[HttpGet("deliveryMethods")]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
		{
			var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();

			return Ok(deliveryMethods);
		}
	
	
	}
}
