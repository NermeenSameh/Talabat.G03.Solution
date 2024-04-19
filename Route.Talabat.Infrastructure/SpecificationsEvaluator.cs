using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure
{
	internal class SpecificationsEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecifications<TEntity> spec)
		{
			var query = inputQuery; 
			if(spec.Criteria is not null) 
				query = query.Where(spec.Criteria);

			// query = _dbContext.Set<Product>().Where(P => P.Id == 1)
			// Includes
			// 1. P => P.Brand
			// 2. P => P.Category

			if(spec.OrderBy is not null)
				query = query.OrderBy(spec.OrderBy);
			
			else if(spec.OrderByDesc is not null) 
				query = query.OrderByDescending(spec.OrderByDesc); 


			query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

			// _dbContext.Set<Product>().Where(P => P.Id == 1).Include(P => P.Brand)
			// _dbContext.Set<Product>().Where(P => P.Id == 1).Include(P => P.Brand).Include(P => P.Category)


			return query;
		}

	}
}
