using Route.Talabat.Core.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Entities.Order_Aggregate
{
	public class Order : BaseEntity
	{
		public string BuyerEmail { get; set; } = null!;

		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

		public OrderStatus Status { get; set; } = OrderStatus.Pending;

		public Address ShappingAddress { get; set; } = null!;

		public int DeliveryMethodId { get; set; } // FOreign Key 
		public DeliveryMethod DeliveryMethod { get; set; } = null!;  // Navigational Property  [ONE]

		public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();  // Navigational Property [MANY]

		public decimal Subtotal { get; set; }

		//[NotMapped]
		//public decimal Total => Subtotal + DeliveryMethod.Cost;

        public decimal GetTotal() => Subtotal + DeliveryMethod.Cost;

		public string PaymentIntenId { get; set; } = string.Empty;
    }
}
