// Type: System.Web.Mvc.DefaultControllerFactory
// Assembly: System.Web.Mvc, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Windows\assembly\GAC_MSIL\System.Web.Mvc\2.0.0.0__31bf3856ad364e35\System.Web.Mvc.dll

using System;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public class DefaultControllerFactory : IControllerFactory
    {
        public DefaultControllerFactory();

        #region IControllerFactory Members

        public virtual IController CreateController(RequestContext requestContext, string controllerName);
        public virtual void ReleaseController(IController controller);

        #endregion

        protected internal virtual IController GetControllerInstance(RequestContext requestContext, Type controllerType);
        protected internal virtual Type GetControllerType(RequestContext requestContext, string controllerName);
    }
}
