using ContosoUniversity.DataAccess.Contracts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ContosoUniversity.DataAccess.Repositories
{
    public class EfAsyncRepository<T> : EfRepository<T>, IAsyncRepository<T> where T : class
    {
        public EfAsyncRepository(DbContext context) : base(context) { }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }
    }
}
