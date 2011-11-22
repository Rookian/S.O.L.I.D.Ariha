// Type: System.Web.Mvc.ControllerBase
// Assembly: System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: c:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 3\Assemblies\System.Web.Mvc.dll

using System.Web.Routing;

namespace System.Web.Mvc
{
    public abstract class ControllerBase : IController
    {
        public ControllerContext ControllerContext { get; set; }
        public TempDataDictionary TempData { get; set; }
        public bool ValidateRequest { get; set; }
        public IValueProvider ValueProvider { get; set; }
        public ViewDataDictionary ViewData { get; set; }
        public dynamic ViewModel { get; }

        #region IController Members

        void IController.Execute(RequestContext requestContext);

        #endregion

        protected virtual void Execute(RequestContext requestContext);
        protected abstract void ExecuteCore();
        protected virtual void Initialize(RequestContext requestContext);
    }
}
