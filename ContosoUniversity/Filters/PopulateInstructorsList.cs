using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.ViewModels;
using System.Web.Mvc;

namespace ContosoUniversity.Filters
{
    public class PopulateInstructorsList : ActionFilterAttribute
    {
        public ISchoolUow UoW { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model as IHaveInstructorSelectList;

            if (model == null) return;

            model.InstructorSelectList = UoW.Instructors.GetAll().ToSelectList(
                textMember: "FullName");
        }
    }
}