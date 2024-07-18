using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Repositry.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Stock).IsRequired();

            builder.HasMany(p => p.OrderItems)
                   .WithOne(oi => oi.Products)
                   .HasForeignKey(oi => oi.ProductId);
        }
    }
}
