using Autofac;
using ContosoUniversity.DataAccess;
using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.DataAccess.Repositories;
using ContosoUniversity.Models;
using System.Data.Entity;

namespace ContosoUniversity.Infrastructure.Autofac
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SchoolContext>().As<DbContext>().InstancePerRequest();

            builder.RegisterType<CoursesRepository>().As<ICoursesRepository>().InstancePerRequest();
            builder.RegisterType<StudentsRepository>().As<IStudentsRepository>().InstancePerRequest();
            builder.RegisterType<InstructorsRepository>().As<IInstructorsRepository>().InstancePerRequest();
            builder.RegisterType<EnrollmentsRepository>().As<IEnrollmentsRepository>().InstancePerRequest();
            builder.RegisterType<DepartmentsRepository>().As<IAsyncRepository<Department>>().InstancePerRequest();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterGeneric(typeof(EfAsyncRepository<>)).As(typeof(IAsyncRepository<>)).InstancePerRequest();

            builder.RegisterType<SchoolUow>().As<ISchoolUow>().InstancePerRequest();
        }
    }
}