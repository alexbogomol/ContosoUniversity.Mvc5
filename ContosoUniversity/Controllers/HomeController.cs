using ContosoUniversity.DataAccess.Contracts;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ISchoolUow uow)
        {
            UoW = uow;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var stats = UoW.Students.GetEnrollmentStatistics();

            return View(stats);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}