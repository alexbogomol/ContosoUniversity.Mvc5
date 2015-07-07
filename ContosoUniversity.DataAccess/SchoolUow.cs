using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.DataAccess.Repositories;
using ContosoUniversity.Models;
using System;

namespace ContosoUniversity.DataAccess
{
    public class SchoolUow : ISchoolUow, IDisposable
    {
        private readonly SchoolContext _dbContext;
        private readonly ICoursesRepository _courses;
        private readonly IRepository<Department> _departments;
        private readonly IStudentsRepository _students;
        private readonly IInstructorsRepository _instructors;
        private readonly IEnrollmentsRepository _enrollments;
        private readonly IRepository<OfficeAssignment> _offices;

        public SchoolUow()
        {
            _dbContext = new SchoolContext();
            _dbContext.Configuration.LazyLoadingEnabled = false;
            //_dbContext.Configuration.ProxyCreationEnabled = false;
            //_dbContext.Configuration.ValidateOnSaveEnabled = false;

            // TODO: we need a factory here (later)
            _courses = new CoursesRepository(_dbContext);
            _departments = new EfRepository<Department>(_dbContext);
            _students = new StudentsRepository(_dbContext);
            _instructors = new InstructorsRepository(_dbContext);
            _enrollments = new EnrollmentsRepository(_dbContext);
            _offices = new EfRepository<OfficeAssignment>(_dbContext);
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
