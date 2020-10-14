using baseDbContext = Microsoft.EntityFrameworkCore.DbContext;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Data.Mapping;

namespace Data.DbContext
{
    public class ProductDbContext : baseDbContext
    {
        public DbSet<Product.Domain.Product> Products { get; set; }

        public ProductDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());
        }
    }
}
