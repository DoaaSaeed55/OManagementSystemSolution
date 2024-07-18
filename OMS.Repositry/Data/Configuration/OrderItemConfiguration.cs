using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OMS.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.Repositry.Data.Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {

            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.Quentity).IsRequired();
            builder.Property(oi => oi.Price).HasColumnType("decimal(18,2)");
            builder.Property(oi => oi.Discount).HasColumnType("decimal(18,2)");

            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.Items)
                   .HasForeignKey(oi => oi.OrderId);

            builder.HasOne(oi => oi.Products)
                   .WithMany(p => p.OrderItems)
                   .HasForeignKey(oi => oi.ProductId);

        }
    }
}
