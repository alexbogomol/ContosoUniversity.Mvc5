using ContosoUniversity.DataAccess.Contracts;
using System;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ISchoolUow UoW { get; set; }

        protected override void Dispose(bool disposing)
        {
            if (UoW != null && UoW is IDisposable)
            {
                ((IDisposable)UoW).Dispose();
                UoW = null;
            }
            base.Dispose(disposing);
        }
    }
}