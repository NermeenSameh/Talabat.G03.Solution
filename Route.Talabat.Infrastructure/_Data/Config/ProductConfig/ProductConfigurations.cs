using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Route.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure._Data.Config.ProductConfig
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(P => P.Description)
                .IsRequired();

            builder.Property(P => P.PictureUrl)
                .IsRequired();


            builder.Property(P => P.Price)
                .HasColumnType("decimal (12,2)");

            builder.HasOne(P => P.Brands)
                .WithMany()
                .HasForeignKey(P => P.BrandId);

            builder.HasOne(P => P.Category)
                .WithMany()
                .HasForeignKey(P => P.CategoryId);



        }
    }
}
