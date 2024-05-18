using Route.Talabat.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
	public class OrderDTo
	{
		[Required]
		public string BuyerEmail { get; set; } = null!;
		[Required]
		public  string BasketId { get; set; } = null!;
		[Required]
		public int DeliveryMethodId { get; set; }

        public AddressDto ShippingAddress { get; set; } = null!;
	}
}
