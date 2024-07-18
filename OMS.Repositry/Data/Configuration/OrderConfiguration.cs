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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.HasKey(o => o.Id);
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.TotaL).HasColumnType("decimal(18,2)");
            builder.Property(o => o.status).IsRequired().HasMaxLength(50);
            builder.Property(o => o.PaymentMethod).IsRequired();

            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(o => o.CustomerId);

            builder.HasMany(o => o.Items)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId);
        }
    }
}
