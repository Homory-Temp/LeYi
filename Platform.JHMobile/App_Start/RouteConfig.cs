using System.Web.Mvc;
using System.Web.Routing;

namespace Platform.JHMobile
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(name: "Default", url: "{controller}/{action}/{id}", defaults: new { controller = "首页", action = "首页", id = UrlParameter.Optional });
        }
    }
}
