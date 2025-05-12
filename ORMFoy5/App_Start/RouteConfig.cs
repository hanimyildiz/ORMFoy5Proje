using System.Web.Mvc;
using System.Web.Routing;

namespace ORMFoy5
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ORMFoy5.Controllers" } // Sadece Controllers ad alanını kullan
            );
        }
    }
}