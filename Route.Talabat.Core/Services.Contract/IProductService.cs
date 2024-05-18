using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.Specifications.Product_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Services.Contract
{
	public interface IProductService
	{

		Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams);

		Task<Product?> GetProductAsync(int productId);

		Task<int> GetCountAsync(ProductSpecParams specParams);


		Task<IReadOnlyList<ProductBrand>> GetBrandsAsync();

		Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync();
	}
}
