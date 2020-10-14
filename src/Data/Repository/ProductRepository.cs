using Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Product.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ProductRepository : BaseDbRepositoryAbstract<ProductDbContext>, IProductRepository
    {
        public ProductRepository(ProductDbContext db) : base(db)
        {
        }

        public async Task AddAsync(Product.Domain.Product entity)
        {
            dbContext.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product.Domain.Product entity)
        {
            dbContext.Update(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Product.Domain.Product> FindAsync(Guid id) =>
            await dbContext.Products.Where(p => p.EntityId == id).FirstOrDefaultAsync();

        public async Task<bool> ProductExistsAsync(string title) =>
            await dbContext.Products.AnyAsync(p => p.Title == title);
    }
}
