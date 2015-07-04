using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ContosoUniversity.DataAccess.Repositories
{
    public class CoursesRepository : EfRepository<Course>, ICoursesRepository
    {
        public CoursesRepository(DbContext context) : base(context) { }

        public override Course GetById(int id)
        {
            return DbSet.Include(c => c.Department)
                        .Where(c => c.Id == id)
                        .Single();
        }

        public IEnumerable<Course> GetByDepartment(int? departmentId)
        {
            int id = departmentId.GetValueOrDefault();

            return DbSet.Where(c => !departmentId.HasValue || c.DepartmentId == id)
                        .Include(course => course.Department)
                        .ToList();
        }

        public int UpdateCourseCredits(int multiplier)
        {
            return DbContext.Database.ExecuteSqlCommand(
                "UPDATE Course SET Credits = Credits * {0}",
                multiplier);
        }
    }
}
