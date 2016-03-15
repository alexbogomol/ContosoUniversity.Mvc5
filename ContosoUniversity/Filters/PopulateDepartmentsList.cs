using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.ViewModels;
using System.Web.Mvc;

namespace ContosoUniversity.Filters
{
    public class PopulateDepartmentsList : ActionFilterAttribute
    {
        public ISchoolUow UoW { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model as IHaveDepartmentSelectList;

            if (model == null) return;

            model.DepartmentSelectList = UoW.Departments.GetAll().ToSelectList();
        }
    }
}