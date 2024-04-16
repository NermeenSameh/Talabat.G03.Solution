using Route.Talabat.Core.Entities.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specifications.EmployeeSpecs
{
	public class EmployeeWithDepartmentSpecifications : BaseSpecifications<Employee>
	{

        public EmployeeWithDepartmentSpecifications()
            :base()
        {
            Includes.Add(E => E.Department);
            
        }
        public EmployeeWithDepartmentSpecifications(int id):base( E => E.Id == id)
        {
			Includes.Add(E => E.Department);
		}


    }
}
