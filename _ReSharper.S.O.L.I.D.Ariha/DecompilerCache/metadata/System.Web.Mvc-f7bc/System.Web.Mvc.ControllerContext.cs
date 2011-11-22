// Type: System.Web.Mvc.ControllerContext
// Assembly: System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: c:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 3\Assemblies\System.Web.Mvc.dll

using System.Web;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public class ControllerContext
    {
        public ControllerContext();
        protected ControllerContext(ControllerContext controllerContext);
        public ControllerContext(HttpContextBase httpContext, RouteData routeData, ControllerBase controller);
        public ControllerContext(RequestContext requestContext, ControllerBase controller);
        public virtual ControllerBase Controller { get; set; }
        public virtual HttpContextBase HttpContext { get; set; }
        public virtual bool IsChildAction { get; }
        public ViewContext ParentActionViewContext { get; }
        public RequestContext RequestContext { get; set; }
        public virtual RouteData RouteData { get; set; }
    }
}
