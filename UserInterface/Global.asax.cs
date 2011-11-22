using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UserInterface
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{favicon}", new { favicon = @"(./)?favicon.ico(/.*)?" });
            routes.IgnoreRoute("{*allpngs}", new { allpngs = @".*\.png(/.*)?" });
            routes.IgnoreRoute("{non}", new { non = @"(./)?non(/.*)?" });

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Employee", action = "Index", id = "" }  // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.DefaultBinder = new ModelBinder();
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
        }
    }
}