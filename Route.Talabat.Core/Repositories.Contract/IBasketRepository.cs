﻿using Route.Talabat.Core.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Repositories.Contract
{
    public interface IBasketRepository
	{
		Task<CustomerBasket?> GetBasketAsync(string basketId);

		Task<CustomerBasket?> UpdateBasketAsync (CustomerBasket basket);

		Task<bool> DeleteBasketAsync (string basketId);


	}
}
