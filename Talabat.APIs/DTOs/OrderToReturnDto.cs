using Route.Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.DTOs
{
	public class OrderToReturnDto
	{
		public int Id { get; set; }
		public string BuyerEmail { get; set; } = null!;

		public DateTimeOffset OrderDate { get; set; }

		public string Status { get; set; } = null!;

		public Address ShippingAddress { get; set; } = null!;

		public string DeliveryMethod { get; set; } = null!;
		public decimal DeliveryMethodCost { get; set; } 

		public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>();  

		public decimal Subtotal { get; set; }

		public decimal Total { get; set; }

		public string PaymentIntenId { get; set; } = string.Empty;
	}

}

