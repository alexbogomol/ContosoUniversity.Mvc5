using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ContosoUniversity.DataAccess.Repositories
{
    public class EnrollmentsRepository : EfRepository<Enrollment>, IEnrollmentsRepository
    {
        public EnrollmentsRepository(DbContext context) : base(context) { }

        public IEnumerable<Enrollment> GetByCourse(int? courseId)
        {
            return DbSet.Include(e => e.Student)
                        .Where(e => e.CourseId == courseId.Value)
                        .ToList();
        }

        public IEnumerable<Enrollment> GetByStudent(int? studentId)
        {
            return DbSet.Include(e => e.Course)
                        .Where(e => e.StudentId == studentId.Value)
                        .ToList();
        }
    }
}
