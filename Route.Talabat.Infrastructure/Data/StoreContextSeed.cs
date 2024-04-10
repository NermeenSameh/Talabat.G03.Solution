using Route.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Data
{
	public static class StoreContextSeed
	{

		public async static Task SeedAsync(StoreContext _dbContext)
		{
			if (_dbContext.Brands.Count() == 0)
			{
				var brandsData = File.ReadAllText("../Route.Talabat.Infrastructure/Data/DataSeed/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

				if (brands?.Count > 0)
				{

					foreach (var brand in brands)
					{
						_dbContext.Set<ProductBrand>().Add(brand);
					}
					await _dbContext.SaveChangesAsync();

				} 
			}

			if (_dbContext.Categories.Count() == 0)
			{
				var categoryData = File.ReadAllText("../Route.Talabat.Infrastructure/Data/DataSeed/categories.json");
				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData);

				if (categories?.Count > 0)
				{

					foreach (var category in categories)
					{
						_dbContext.Set<ProductCategory>().Add(category);
					}
					await _dbContext.SaveChangesAsync();

				}
			}

			if (_dbContext.Products.Count() == 0)
			{
				var productData = File.ReadAllText("../Route.Talabat.Infrastructure/Data/DataSeed/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productData);

				if (products?.Count > 0)
				{

					foreach (var product in products)
					{
						_dbContext.Set<Product>().Add(product);
					}
					await _dbContext.SaveChangesAsync();

				}
			}

		}
	}
}
