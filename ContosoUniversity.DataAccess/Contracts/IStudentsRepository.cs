using ContosoUniversity.Models;
using System.Collections.Generic;

namespace ContosoUniversity.DataAccess.Contracts
{
    public interface IStudentsRepository : IRepository<Student>
    {
        IEnumerable<EnrollmentStatistics> GetEnrollmentStatistics();
    }
}