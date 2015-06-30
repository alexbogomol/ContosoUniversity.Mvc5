using System.Web.Mvc;
using System.Web.Routing;

namespace ContosoUniversity.Config
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "InstructorMasterDetail",
                url: "{controller}/{instructorId}/Course/{courseId}",
                defaults: new
                {
                    controller = "Instructor",
                    action = "Index",
                    instructorId = UrlParameter.Optional,
                    courseId = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
