using ContosoUniversity.DataAccess;
using ContosoUniversity.DataAccess.Contracts;
using Ninject.Modules;
using Ninject.Web.Common;

namespace ContosoUniversity.Infrastructure.Ninject
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISchoolUow>().To<SchoolUow>().InRequestScope();
        }
    }
}