using Common;
using System.Threading.Tasks;

namespace Product.Domain
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<bool> ProductExistsAsync(string title);
    }
}
