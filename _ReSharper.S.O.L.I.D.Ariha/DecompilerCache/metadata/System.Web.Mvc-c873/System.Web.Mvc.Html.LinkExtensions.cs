// Type: System.Web.Mvc.Html.LinkExtensions
// Assembly: System.Web.Mvc, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 2\Assemblies\System.Web.Mvc.dll

using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace System.Web.Mvc.Html
{
    public static class LinkExtensions
    {
        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName);

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName,
                                               object routeValues);

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName,
                                               object routeValues, object htmlAttributes);

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName,
                                               RouteValueDictionary routeValues);

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName,
                                               RouteValueDictionary routeValues,
                                               IDictionary<string, object> htmlAttributes);

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName,
                                               string controllerName);

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName,
                                               string controllerName, object routeValues, object htmlAttributes);

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName,
                                               string controllerName, RouteValueDictionary routeValues,
                                               IDictionary<string, object> htmlAttributes);

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName,
                                               string controllerName, string protocol, string hostName, string fragment,
                                               object routeValues, object htmlAttributes);

        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName,
                                               string controllerName, string protocol, string hostName, string fragment,
                                               RouteValueDictionary routeValues,
                                               IDictionary<string, object> htmlAttributes);

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, object routeValues);

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText,
                                              RouteValueDictionary routeValues);

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName);

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName,
                                              object routeValues);

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName,
                                              RouteValueDictionary routeValues);

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, object routeValues,
                                              object htmlAttributes);

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText,
                                              RouteValueDictionary routeValues,
                                              IDictionary<string, object> htmlAttributes);

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName,
                                              object routeValues, object htmlAttributes);

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName,
                                              RouteValueDictionary routeValues,
                                              IDictionary<string, object> htmlAttributes);

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName,
                                              string protocol, string hostName, string fragment, object routeValues,
                                              object htmlAttributes);

        public static MvcHtmlString RouteLink(this HtmlHelper htmlHelper, string linkText, string routeName,
                                              string protocol, string hostName, string fragment,
                                              RouteValueDictionary routeValues,
                                              IDictionary<string, object> htmlAttributes);
    }
}
