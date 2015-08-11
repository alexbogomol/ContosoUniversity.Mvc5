using ContosoUniversity.DataAccess.Contracts;
using ContosoUniversity.ViewModels;
using System.Web.Mvc;

namespace ContosoUniversity.Filters
{
    public class PopulateInstructorsList : ActionFilterAttribute
    {
        private readonly ISchoolUow UoW;

        public PopulateInstructorsList() { }

        public PopulateInstructorsList(ISchoolUow uow)
        {
            UoW = uow;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var model = filterContext.Controller.ViewData.Model as IHaveInstructorSelectList;

            if (model == null) return;

            model.InstructorSelectList = UoW.Instructors.GetAll().ToSelectList(
                textMember: "FullName");
        }
    }
}