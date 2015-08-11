using ContosoUniversity.Filters;
using Ninject.Modules;
using Ninject.Web.Mvc.FilterBindingSyntax;
using System.Web.Mvc;

namespace ContosoUniversity.Infrastructure.Ninject
{
    public class FiltersModule : NinjectModule
    {
        public override void Load()
        {
            this.BindFilter<PopulateDepartmentsList>(FilterScope.Action, 0);
        }
    }
}