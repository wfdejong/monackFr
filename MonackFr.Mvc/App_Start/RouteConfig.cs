using System.Web.Mvc;
using System.Web.Routing;

namespace MonackFr.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Install",
                url: "install",
                defaults: new {controller = "Main", action = "Install"}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Main", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}
