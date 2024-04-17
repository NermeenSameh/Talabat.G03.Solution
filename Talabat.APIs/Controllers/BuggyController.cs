﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Infrastructure.Data;

namespace Talabat.APIs.Controllers
{

	public class BuggyController : BaseApiController
	{
		private readonly StoreContext _dbContext;

		public BuggyController(StoreContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet("notfound")] // Get : api/buggy/notfound

		public ActionResult GetNotFoundRequest()
		{
			var product = _dbContext.Products.Find(100);
			if (product is null)
				return NotFound();

			return Ok(product);
		}

		[HttpGet("servererror")] // Get : api/buggy/servererror

		public ActionResult GetServerError()
		{
			var product = _dbContext.Products.Find(100);

			var productToReturn = product.ToString(); // will Throw Exception

			return Ok(productToReturn);
		}

		[HttpGet("badrequest")] // Get : api/buggy/badrequest
		public ActionResult GetBadRequest()
		{
			return BadRequest();
		}

		[HttpGet("badrequest/{id}")] // Get : api/buggy/badrequest/five

		public ActionResult GetBadRequest(int id) // Validation Reeor
		{
			return Ok();
		}

		
	}
}
