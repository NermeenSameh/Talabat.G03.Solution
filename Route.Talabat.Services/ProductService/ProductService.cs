using Route.Talabat.Core;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.Services.Contract;
using Route.Talabat.Core.Specifications.Product_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;

namespace Route.Talabat.Service.ProductService
{
	public class ProductService : IProductService
	{
		private readonly IUniteOfWork _uniteOfWork;

		public ProductService(IUniteOfWork uniteOfWork)
		{
			_uniteOfWork = uniteOfWork;
		}

		public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(specParams);


			var products = await _uniteOfWork.Repository<Product>().GetAllWithSpecAsync(spec);

			return products;
		}
		public async Task<Product?> GetProductAsync(int productId)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(productId);
			var product = await _uniteOfWork.Repository<Product>().GetIdWithSpecAsync(spec);

			return product;
		}
		public async Task<int> GetCountAsync(ProductSpecParams specParams)
		{
			var countSpec = new ProductWithFilterationForCountSpecifications(specParams);


			var count = await _uniteOfWork.Repository<Product>().GetCountAsync(countSpec);
			return count;
		}

		public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
		  => await _uniteOfWork.Repository<ProductBrand>().GetAllAsync();

		public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
		  => await _uniteOfWork.Repository<ProductCategory>().GetAllAsync();

	}
}
