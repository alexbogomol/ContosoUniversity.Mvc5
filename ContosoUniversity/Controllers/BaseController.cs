using ContosoUniversity.DataAccess.Contracts;
using Microsoft.Web.Mvc;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ContosoUniversity.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ISchoolUow UoW { get; set; }

        protected ActionResult RedirectToAction<TController>(
            Expression<Action<TController>> action)
            where TController : Controller
        {
            return ControllerExtensions.RedirectToAction(this, action);
        }

        protected override void Dispose(bool disposing)
        {
            bool toBeDisposed = UoW != null 
                             && UoW is IDisposable
                             && !ControllerContext.IsChildAction;

            if (toBeDisposed)
            {
                ((IDisposable)UoW).Dispose();
                UoW = null;
            }

            base.Dispose(disposing);
        }
    }
}