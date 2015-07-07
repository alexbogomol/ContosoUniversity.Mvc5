using ContosoUniversity.Models;
using System.Collections.Generic;

namespace ContosoUniversity.DataAccess.Contracts
{
    public interface ICoursesRepository : IRepository<Course>
    {
        int UpdateCourseCredits(int multiplier);
        IEnumerable<Course> GetByDepartment(int? departmentId);
        IEnumerable<Course> GetByInstructor(int? instructorId);
    }
}
