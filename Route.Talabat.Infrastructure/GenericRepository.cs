using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Repositories.Contract;
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
		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
		}

		public async Task<T?> GetAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}
	}
}
