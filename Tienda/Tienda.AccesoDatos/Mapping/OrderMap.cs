using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tienda.Modelos.Entities;

namespace Tienda.AccesoDatos.Mapping
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");
            builder.Property(c => c.CustomerName)
                .HasColumnName("CustomerName");
            builder.Property(c => c.CustomerEmail)
                .HasColumnName("CustomerEmail");
            builder.Property(c => c.CustomerMobile)
                .HasColumnName("CustomerMobile");
            builder.Property(c => c.Status)
                .HasColumnName("Status");
            builder.Property(c => c.CreatedAt)
                .HasColumnName("CreatedAt");
            builder.Property(c => c.UpdatedAt)
                .HasColumnName("UpdatedAt");
            builder.Property(c => c.Value)
                .HasColumnName("Value");
            builder.Property(c => c.ProductId)
                .HasColumnName("ProductId");

            builder.ToTable("Order")
                .HasKey(c => c.Id);

            builder.HasOne(c => c.Product)
               .WithMany(c => c.Orders)
               .HasForeignKey(c => c.ProductId);
        }
    }
}
