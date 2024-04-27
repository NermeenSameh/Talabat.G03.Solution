using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities.Baskets;

namespace Route.Talabat.Core.Entities.Employee
{
    public class Employee : BaseEntity
	{
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int? Age { get; set; }

        public Department Department { get; set; } // Navigational Property [One]
    }
}
