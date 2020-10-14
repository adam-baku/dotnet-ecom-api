using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface IBaseRepository<TEntity>
        where TEntity : EntityAbstract
    {
        void Persist(TEntity entity);
        TEntity Find(Guid id);
    }
}
