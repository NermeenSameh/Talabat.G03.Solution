using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Route.Talabat.Core.Entities.Order_Aggregate;
using Route.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.Data
{
    public class StoreContext : DbContext
	{
		public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }



		/// protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		/// 	=> optionsBuilder.UseSqlServer("connectionString");

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}


		public DbSet<Product> Products { get; set; }

		public DbSet<ProductBrand> Brands { get; set; }

		public DbSet<ProductCategory> Categories { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderItem> OrderItems { get; set; }

		public DbSet<DeliveryMethod> DeliveryMethods { get; set;}
	}
}
