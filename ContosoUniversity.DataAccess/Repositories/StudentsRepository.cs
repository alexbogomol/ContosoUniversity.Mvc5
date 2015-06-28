using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ContosoUniversity.DataAccess.Repositories
{
    public class StudentsRepository : EfRepository<Student>, IStudentsRepository
    {
        public StudentsRepository(DbContext context) : base(context) { }

        public IEnumerable<EnrollmentStatistics> GetEnrollmentStatistics()
        {
            string query = @"SELECT EnrollmentDate, COUNT(*) AS StudentCount 
                             FROM Person 
                             WHERE Discriminator = 'Student' 
                             GROUP BY EnrollmentDate";

            var data = DbContext.Database.SqlQuery<EnrollmentStatistics>(query);

            return data.ToList();

            /*--> ordinary way
            var data = from student in db.Students
                       group student by student.EnrollmentDate into dateGroup
                       select new EnrollmentDateGroup
                       {
                           EnrollmentDate = dateGroup.Key,
                           StudentCount = dateGroup.Count()
                       };
            */
        }
    }
}
