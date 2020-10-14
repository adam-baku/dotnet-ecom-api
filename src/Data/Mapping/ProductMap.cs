using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product.Domain.Product>
    {
        public void Configure(EntityTypeBuilder<Product.Domain.Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.ProductId)
                .IsUnique();

            builder.HasIndex(p => p.Title)
                .IsUnique();

            builder.Property(p => p.ProductId)
                .HasColumnName("ProductId")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.Title)
                .HasColumnName("Title")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(p => p.AvailableQuantity)
                .HasColumnName("AvailableQuantity")
                .HasColumnType("int")
                .IsRequired();

            builder.OwnsOne(
                p => p.Price,
                price => {
                    price.Property(p => p.Net)
                        .HasColumnName("NetPrice")
                        .HasColumnType("decimal(7,2)")
                        .IsRequired();

                    price.Property(p => p.Gross)
                        .HasColumnName("GrossPrice")
                        .HasColumnType("decimal(7,2)")
                        .IsRequired();

                    price.OwnsOne(
                        p => p.Tax,
                        tax => {
                            tax.Property(p => p.Value)
                                .HasColumnName("TaxValue")
                                .HasColumnType("decimal(5,2)")
                                .IsRequired();
                        }
                    );

                    price.Property(p => p.Currency)
                        .HasColumnName("Currency")
                        .HasColumnType("varchar(3)")
                        .IsRequired();
                }
            );
        }
    }
}
