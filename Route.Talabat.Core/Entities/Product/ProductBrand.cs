using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities.Baskets;

namespace Route.Talabat.Core.Entities.Product
{
    public class ProductBrand : BaseEntity
    {

        public string Name { get; set; } = null!;

        //public ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
