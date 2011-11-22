// Type: System.Web.Mvc.ViewUserControl`1
// Assembly: System.Web.Mvc, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 2\Assemblies\System.Web.Mvc.dll

namespace System.Web.Mvc
{
    public class ViewUserControl<TModel> : ViewUserControl
    {
        public new AjaxHelper<TModel> Ajax { get; }
        public new HtmlHelper<TModel> Html { get; }
        public new TModel Model { get; }
        public new ViewDataDictionary<TModel> ViewData { get; set; }
        protected override void SetViewData(ViewDataDictionary viewData);
    }
}
