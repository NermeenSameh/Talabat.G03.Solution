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
		public ProductWithBrandAndCategorySpecifications(string sort)
			: base()
		{
			AddIncludes();

			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort)
				{
					case "priceAsc":
						AddOrderBy(P => P.Price);
						break;

					case "priceDesc":
						AddOrderByDesc(P => P.Price);
						break;
					default:
						AddOrderBy(P => P.Name);
						break;
				}
			}
			else
				AddOrderBy(P => P.Name);

		}

		// This Constructor will be Used for Creating an object, that will be used to Get A specific product with id
		public ProductWithBrandAndCategorySpecifications(int id)
			: base(P => P.Id == id)
		{
			AddIncludes();
		}
		private void AddIncludes()
		{
			base.Includes.Add(P => P.Brands);
			base.Includes.Add(P => P.Category);
		}

	}
}
