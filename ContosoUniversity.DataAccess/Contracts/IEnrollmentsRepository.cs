using ContosoUniversity.Models;
using System.Collections.Generic;

namespace ContosoUniversity.DataAccess.Contracts
{
    public interface IEnrollmentsRepository : IRepository<Enrollment>
    {
        IEnumerable<Enrollment> GetByCourse(int? courseId);
        IEnumerable<Enrollment> GetByStudent(int? studentId);
    }
}
