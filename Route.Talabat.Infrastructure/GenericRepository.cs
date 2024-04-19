using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Specifications;
using Route.Talabat.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbContext;

		public GenericRepository(StoreContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			/// if (typeof(T) == typeof(Product))
			/// 	return (IEnumerable<T>)await _dbContext.Set<Product>().Include(P => P.Brands).Include(P => P.Category).ToListAsync();
			return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
		}


		public async Task<T?> GetAsync(int id)
		{
			/// if (typeof(T) == typeof(Product))
			/// 	return await _dbContext.Set<Product>().Where(P => P.Id == id).Include(P => P.Brands).Include(P => P.Category).FirstOrDefaultAsync() as T;

			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).ToListAsync();
		}


		public async Task<T?> GetWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecifications(spec).FirstOrDefaultAsync();
		}
		
		
		private IQueryable<T> ApplySpecifications(ISpecifications<T> spec)
		{
			return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
		}



	}
}
