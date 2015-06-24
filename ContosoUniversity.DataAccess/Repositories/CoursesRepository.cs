using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Models;
using System.Data.Entity;

namespace ContosoUniversity.DataAccess.Repositories
{
    public class CoursesRepository : EfRepository<Course>, ICoursesRepository
    {
        public CoursesRepository(DbContext context) : base(context) { }

        public int UpdateCourseCredits(int multiplier)
        {
            return DbContext.Database.ExecuteSqlCommand(
                "UPDATE Course SET Credits = Credits * {0}",
                multiplier);
        }
    }
}
