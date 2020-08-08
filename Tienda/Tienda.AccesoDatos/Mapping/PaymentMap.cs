using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tienda.Modelos.Entities;

namespace Tienda.AccesoDatos.Mapping
{
   public class PaymentMap : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");
            builder.Property(c => c.Date)
                .HasColumnName("Date");
            builder.Property(c => c.RequestId)
                .HasColumnName("RequestId");
            builder.Property(c => c.MethodName)
                .HasColumnName("MethodName");
            builder.Property(c => c.Authorization)
                .HasColumnName("Authorization");
            builder.Property(c => c.IssuerName)
                .HasColumnName("IssuerName");
            builder.Property(c => c.Receipt)
                .HasColumnName("Receipt");
            builder.Property(c => c.Status)
                .HasColumnName("Status");
            builder.Property(c => c.OrderId)
                .HasColumnName("OrderId");

            builder.ToTable("Payment")
                .HasKey(c => c.Id);

            builder.HasOne(c => c.Order)
               .WithMany(c => c.Payments)
               .HasForeignKey(c => c.OrderId);
        }
    }
}
