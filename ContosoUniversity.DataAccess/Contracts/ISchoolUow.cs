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
        IRepository<Student> Students { get; }
        IRepository<Enrollment> Enrollments { get; }
        IRepository<Course> Courses { get; }
        IRepository<Department> Departments { get; }
        IRepository<Instructor> Instructors { get; }
        IRepository<OfficeAssignment> OfficeAssignments { get; }
        IRepository<Person> People { get; }
    }
}
