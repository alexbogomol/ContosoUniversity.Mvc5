using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.Models;
using System;

namespace ContosoUniversity.DataAccess
{
    public class SchoolUow : ISchoolUow, IDisposable
    {
        private readonly SchoolContext _dbContext;
        private readonly EfRepository<Course> _courses;
        private readonly EfRepository<Department> _departments;

        public SchoolUow()
        {
            _dbContext = new SchoolContext();
            _dbContext.Configuration.LazyLoadingEnabled = false;
            _dbContext.Configuration.ProxyCreationEnabled = false;
            _dbContext.Configuration.ValidateOnSaveEnabled = false;

            // TODO: we need a factory here (later)
            _courses = new EfRepository<Course>(_dbContext);
            _departments = new EfRepository<Department>(_dbContext);
        }

        public IRepository<Course> Courses
        {
            get { return _courses; }
        }

        public IRepository<Department> Departments
        {
            get { return _departments; }
        }

        public IRepository<Enrollment> Enrollments
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Instructor> Instructors
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<OfficeAssignment> OfficeAssignments
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Person> People
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IRepository<Student> Students
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Commit()
        {
            throw new NotImplementedException();
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
