using ContosoUniversity.Models;

namespace ContosoUniversity.DataAccess.Contracts
{
    public interface ICoursesRepository : IRepository<Course>
    {
        int UpdateCourseCredits(int multiplier);
    }
}
