﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Entities.Order_Aggregate
{
	public class ProductItemOrder
	{
		public ProductItemOrder(int productId, string productName, string pictureUrl)
		{
			ProductId = productId;
			ProductName = productName;
			PictureUrl = pictureUrl;
		}
        private ProductItemOrder()
        {
            
        }


        public int ProductId { get; set; }

		public string ProductName { get; set; } = null!;

		public string PictureUrl { get; set; } = null!;
	}
}
