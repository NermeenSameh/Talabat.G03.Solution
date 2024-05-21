﻿using Route.Talabat.Core.Entities.Baskets;
using Route.Talabat.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Services.Contract
{
	public interface IPaymentService
	{

		Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);


		Task<Order?> UpdateOrderStatus(string paymentIntentId, bool isPaid);
	}
}
