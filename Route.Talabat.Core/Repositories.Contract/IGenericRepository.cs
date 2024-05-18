using Route.Talabat.Core.Entities.Baskets;
using Route.Talabat.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T?> GetByIdAsync (int id);

		Task<IReadOnlyList<T>> GetAllAsync ();

		Task<T?> GetIdWithSpecAsync(ISpecifications<T> spec);

		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

		Task<int> GetCountAsync(ISpecifications<T> spec);


		void AddAsync(T entity);
		void Delete(T entity);
		void Update(T entity);
	}
}
