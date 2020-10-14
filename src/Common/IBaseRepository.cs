using System;
using System.Threading.Tasks;

namespace Common
{
    public interface IBaseRepository<TEntity>
        where TEntity : EntityAbstract
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<TEntity> FindAsync(Guid id);
    }
}
