using ContosoUniversity.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.DataAccess.Repositories
{
    public class DepartmentsRepository : EfAsyncRepository<Department>
    {
        public DepartmentsRepository(DbContext context) : base(context) { }

        public override IQueryable<Department> GetAll()
        {
            return DbSet.OrderBy(d => d.Name);
        }

        public override async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await DbSet.Include(d => d.Administrator)
                              .ToListAsync();
        }

        public override async Task<Department> GetByIdAsync(int id)
        {
            return await DbSet.Include(d => d.Administrator)
                              .Where(d => d.Id == id)
                              .SingleAsync();

            // Create and execute raw SQL query.
            //string query = "SELECT * FROM Department WHERE Id = @p0";
            //Department department = await db.Departments.SqlQuery(query, id).SingleOrDefaultAsync();
        }
    }
}
