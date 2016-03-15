using Autofac;
using Autofac.Integration.Mvc;
using ContosoUniversity.DataAccess;
using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.DataAccess.Repositories;
using ContosoUniversity.Models;
using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;

namespace ContosoUniversity.Config
{
    public class AutofacConfig
    {
        public static void Init()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            RegisterDataAccessModule(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterDataAccessModule(ContainerBuilder builder)
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