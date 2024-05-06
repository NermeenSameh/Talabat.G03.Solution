﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Entities.Baskets
{
    public class BasketItem
    {

        public int Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Brand { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
