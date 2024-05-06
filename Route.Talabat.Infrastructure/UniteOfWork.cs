using Route.Talabat.Core;
using Route.Talabat.Core.Entities.Baskets;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Infrastructure.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure
{
	public class UniteOfWork : IUniteOfWork
	{
		private readonly StoreContext _dbContext;

		//	private Dictionary<string, GenericRepository<BaseEntity>> _repositories;
		private Hashtable _repositories;
		public UniteOfWork(StoreContext dbContext)
        {
			_dbContext = dbContext;
			_repositories = new Hashtable();
		}
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			var key = typeof(TEntity).Name;	
			if(!_repositories.ContainsKey(key) )
			{
				var repository =  new GenericRepository<TEntity>(_dbContext);

				_repositories.Add(key, repository);

			}

			return _repositories[key] as IGenericRepository<TEntity>;
				
		}

		public async Task<int> CompleteAsync()
		=> await _dbContext.SaveChangesAsync();

		public async ValueTask DisposeAsync()
		 => await _dbContext.DisposeAsync();

	}
}
