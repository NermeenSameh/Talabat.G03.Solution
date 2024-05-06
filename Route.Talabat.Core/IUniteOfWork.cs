using Route.Talabat.Core.Entities.Baskets;
using Route.Talabat.Core.Entities.Order_Aggregate;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.Repositories.Contract;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core
{
	public interface IUniteOfWork : IAsyncDisposable 
	{

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();

    }
}
