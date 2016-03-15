using ContosoUniversity.DataAccess;
using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.DataAccess.Repositories;
using ContosoUniversity.Models;
using Ninject.Modules;
using Ninject.Web.Common;
using System.Data.Entity;

namespace ContosoUniversity.Infrastructure.Ninject
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<SchoolContext>().InRequestScope();

            Bind<ICoursesRepository>().To<CoursesRepository>().InRequestScope();
            Bind<IStudentsRepository>().To<StudentsRepository>().InRequestScope();
            Bind<IInstructorsRepository>().To<InstructorsRepository>().InRequestScope();
            Bind<IEnrollmentsRepository>().To<EnrollmentsRepository>().InRequestScope();
            Bind(typeof(IRepository<>)).To(typeof(EfRepository<>)).InRequestScope();

            Bind<IAsyncRepository<Department>>().To<DepartmentsRepository>().InRequestScope();
            Bind(typeof(IAsyncRepository<>)).To(typeof(EfAsyncRepository<>)).InRequestScope();

            Bind<ISchoolUow>().To<SchoolUow>().InRequestScope();
        }
    }
}