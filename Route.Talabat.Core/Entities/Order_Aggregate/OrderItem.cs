using Route.Talabat.Core.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Entities.Order_Aggregate
{
	public class OrderItem : BaseEntity
	{
		public OrderItem(ProductItemOrder product, decimal price, int quantity)
		{
			Product = product;
			Price = price;
			Quantity = quantity;
		}

        private OrderItem()
        {
            
        }

        public ProductItemOrder Product { get; set; } = null!;

		public decimal Price { get; set; }

		public int Quantity { get; set; }



	}
}
