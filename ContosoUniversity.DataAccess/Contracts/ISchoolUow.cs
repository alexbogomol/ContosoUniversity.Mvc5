using ContosoUniversity.Models;

namespace ContosoUniversity.DataAccess.Contracts
{
    /// <summary>
    /// Contract for the School "Unit of Work"
    /// </summary>
    /// <remarks>
    /// Taken from CodeCamper by John Papa:
    /// https://github.com/johnpapa/CodeCamper
    /// </remarks>
    public interface ISchoolUow
    {
        // Save pending changes to the data store.
        void Commit();

        // Repositories available
        IStudentsRepository Students { get; }
        IEnrollmentsRepository Enrollments { get; }
        ICoursesRepository Courses { get; }
        IRepository<Department> Departments { get; }
        IInstructorsRepository Instructors { get; }
        IRepository<OfficeAssignment> OfficeAssignments { get; }
        IRepository<Person> People { get; }
    }
}
