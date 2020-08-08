using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Tienda.Modelos.Entities;

namespace Tienda.AccesoDatos.Mapping
{
   public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");
            builder.Property(c => c.Name)
                .HasColumnName("Name");
            builder.Property(c => c.Description)
                .HasColumnName("Description");
            builder.Property(c => c.Value)
                .HasColumnName("Value");

            builder.ToTable("Product")
                .HasKey(c => c.Id);
        }
    }
}
