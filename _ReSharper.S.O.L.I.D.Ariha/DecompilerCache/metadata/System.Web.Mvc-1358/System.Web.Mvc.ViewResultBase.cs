// Type: System.Web.Mvc.ViewResultBase
// Assembly: System.Web.Mvc, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files\Microsoft ASP.NET\ASP.NET MVC 2\Assemblies\System.Web.Mvc.dll

namespace System.Web.Mvc
{
    public abstract class ViewResultBase : ActionResult
    {
        public TempDataDictionary TempData { get; set; }
        public IView View { get; set; }
        public ViewDataDictionary ViewData { get; set; }
        public ViewEngineCollection ViewEngineCollection { get; set; }
        public string ViewName { get; set; }
        public override void ExecuteResult(ControllerContext context);
        protected abstract ViewEngineResult FindView(ControllerContext context);
    }
}
