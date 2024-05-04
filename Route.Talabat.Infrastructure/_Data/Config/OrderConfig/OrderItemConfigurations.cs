using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure._Data.Config.OrderConfig
{
	internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{

			builder.OwnsOne(orderItem => orderItem.Product, product => product.WithOwner());

			builder.Property(orderItem => orderItem.Price)
				.HasColumnType("decimal(12,2)");
		}
	}
	
}
