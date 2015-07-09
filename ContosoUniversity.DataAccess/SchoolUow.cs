using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Models;
using System;
using System.Data.Entity;

namespace ContosoUniversity.DataAccess
{
    public class SchoolUow : ISchoolUow, IDisposable
    {
        private readonly DbContext _dbContext;
        private readonly ICoursesRepository _courses;
        private readonly IRepository<Department> _departments;
        private readonly IStudentsRepository _students;
        private readonly IInstructorsRepository _instructors;
        private readonly IEnrollmentsRepository _enrollments;
        private readonly IRepository<OfficeAssignment> _offices;

        public SchoolUow(DbContext context,
                         ICoursesRepository courses,
                         IRepository<Department> departments,
                         IStudentsRepository students,
                         IInstructorsRepository instructors,
                         IEnrollmentsRepository enrollments,
                         IRepository<OfficeAssignment> offices)
        {
            _dbContext = context;

            _dbContext.Configuration.LazyLoadingEnabled = false;
            
            _courses = courses;
            _departments = departments;
            _students = students;
            _instructors = instructors;
            _enrollments = enrollments;
            _offices = offices;
        }

        public ICoursesRepository Courses
        {
            get { return _courses; }
        }

        public IRepository<Department> Departments
        {
            get { return _departments; }
        }

        public IEnrollmentsRepository Enrollments
        {
            get { return _enrollments; }
        }

        public IInstructorsRepository Instructors
        {
            get { return _instructors; }
        }

        public IRepository<OfficeAssignment> OfficeAssignments
        {
            get { return _offices; }
        }

        public IRepository<Person> People
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IStudentsRepository Students
        {
            get { return _students; }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        #region IDispossable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }
            }
        }

        #endregion
    }
}
