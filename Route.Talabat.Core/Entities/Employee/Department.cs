using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities.Baskets;

namespace Route.Talabat.Core.Entities.Employee
{
    public class Department : BaseEntity
	{

		public string Name { get; set; }
        public DateOnly DateOfCreation { get; set; }
   
	
	}
}
