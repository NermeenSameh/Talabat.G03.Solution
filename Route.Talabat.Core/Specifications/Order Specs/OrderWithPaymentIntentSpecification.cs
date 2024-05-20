using Route.Talabat.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specifications.Order_Specs
{
	public class OrderWithPaymentIntentSpecification : BaseSpecifications<Order>
	{


		public OrderWithPaymentIntentSpecification(string? paymentIntenId)
		: base(O => O.PaymentIntenId == paymentIntenId)
		{

		}
	}
}
