using Route.Talabat.Core.Entities.Order_Aggregate;


namespace Route.Talabat.Core.Services.Contract
{
	public interface IOrderService
	{
		Task<Order> CreateOrderAsync(string buyerEmail,string basketId,string deliveryMethodId , Address shippingAddress);

		Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);

		Task<Order> GetOrderByIdForUserAsync(string buyerEmail , int orderId);

		Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();


	}
}
