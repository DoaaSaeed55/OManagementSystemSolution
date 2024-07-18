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
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.InvoiceDate).IsRequired();
            builder.Property(i => i.TotalAmount).HasColumnType("decimal(18,2)");

            builder.HasOne(i => i.Order)
                   .WithOne(o => o.Invoice)
                   .HasForeignKey<Invoice>(i => i.OrderId);
        }
    }
}
