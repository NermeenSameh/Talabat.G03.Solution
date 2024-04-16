using Route.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specifications.Product_Specs
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
	{
        // This Constructor will be Used for Creating an Object , That will be Used to Get All Product
        public ProductWithBrandAndCategorySpecifications():base()
        {
            Includes.Add(P => P.Brands);
            Includes.Add(P => P.Category);

        }

    }
}
