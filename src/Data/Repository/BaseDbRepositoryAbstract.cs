using baseDbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Data.Repository
{
    public abstract class BaseDbRepositoryAbstract<TDbContext>
        where TDbContext : baseDbContext
    {
        protected readonly TDbContext dbContext;

        public BaseDbRepositoryAbstract(TDbContext db)
        {
            this.dbContext = db;
        }
    }
}
