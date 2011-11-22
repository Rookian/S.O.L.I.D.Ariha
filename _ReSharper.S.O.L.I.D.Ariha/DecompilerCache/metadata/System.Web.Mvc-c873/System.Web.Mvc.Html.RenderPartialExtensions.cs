// Type: System.Web.Mvc.Html.RenderPartialExtensions
// Assembly: System.Web.Mvc, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 2\Assemblies\System.Web.Mvc.dll

using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    public static class RenderPartialExtensions
    {
        public static void RenderPartial(this HtmlHelper htmlHelper, string partialViewName);
        public static void RenderPartial(this HtmlHelper htmlHelper, string partialViewName, ViewDataDictionary viewData);
        public static void RenderPartial(this HtmlHelper htmlHelper, string partialViewName, object model);

        public static void RenderPartial(this HtmlHelper htmlHelper, string partialViewName, object model,
                                         ViewDataDictionary viewData);
    }
}
