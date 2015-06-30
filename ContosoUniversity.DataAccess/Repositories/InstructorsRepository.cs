using ContosoUniversity.Models;
using System.Data.Entity;
using System.Linq;

namespace ContosoUniversity.DataAccess.Repositories
{
    public class InstructorsRepository : EfRepository<Instructor>
    {
        public InstructorsRepository(DbContext context) : base(context) { }

        public override IQueryable<Instructor> GetAll()
        {
            return DbSet.Include(i => i.OfficeAssignment)
                        .Include(i => i.Courses.Select(c => c.Department));
        }

        public override Instructor GetById(int id)
        {
            return GetAll().Single(i => i.Id == id);
        }
    }
}
