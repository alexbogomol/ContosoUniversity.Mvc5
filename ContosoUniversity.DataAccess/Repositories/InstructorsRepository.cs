using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Models;
using System.Data.Entity;
using System.Linq;

namespace ContosoUniversity.DataAccess.Repositories
{
    public class InstructorsRepository : EfRepository<Instructor>, IInstructorsRepository
    {
        public InstructorsRepository(DbContext context) : base(context) { }

        public override IQueryable<Instructor> GetAll()
        {
            return DbSet.Include(i => i.OfficeAssignment)
                        .Include(i => i.Courses.Select(c => c.Department));
        }

        public override Instructor GetById(int id)
        {
            return GetAll().Where(i => i.Id == id).Single();
        }

        public Instructor GetByIdWithOffice(int id)
        {
            return DbSet.Include(i => i.OfficeAssignment)
                        .Where(i => i.Id == id)
                        .Single();
        }
    }
}
