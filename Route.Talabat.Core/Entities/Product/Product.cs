﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities.Baskets;

namespace Route.Talabat.Core.Entities.Product
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string PictureUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public int BrandId { get; set; } // Foreign Key Column => ProductBrand

        public int CategoryId { get; set; }

        public ProductBrand Brands { get; set; } = null!;  // Navigational Property [ONE]

        public ProductCategory Category { get; set; } = null!;  // Navigational Property [ONE]

    }
}
