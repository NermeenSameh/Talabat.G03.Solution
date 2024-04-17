using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities.Employee;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Specifications.EmployeeSpecs;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{

	public class EmployeeController : BaseApiController
	{
		private readonly IGenericRepository<Employee> _employeeRepo;

		public EmployeeController(IGenericRepository<Employee> employeeRepo )
        {
			_employeeRepo = employeeRepo;
		}

		[HttpGet]

		public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
		{
			var spec = new EmployeeWithDepartmentSpecifications();
			var employee = await _employeeRepo.GetAllWithSpecAsync(spec);


			return Ok(employee);
		}

		[HttpGet("{id}")]

		public async Task<ActionResult<Employee>> GetEmployee(int id)
		{
			var spec = new EmployeeWithDepartmentSpecifications(id);
			var employee = await _employeeRepo.GetWithSpecAsync(spec);

			if(employee is null) 
				return NotFound(new ApiResponse(404));

			return Ok(employee);

		}
	}
}
