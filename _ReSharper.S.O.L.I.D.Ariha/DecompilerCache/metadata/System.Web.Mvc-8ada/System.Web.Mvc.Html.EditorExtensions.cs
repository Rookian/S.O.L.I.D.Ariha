// Type: System.Web.Mvc.Html.EditorExtensions
// Assembly: System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 3\Assemblies\System.Web.Mvc.dll

using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    public static class EditorExtensions
    {
        public static MvcHtmlString Editor(this HtmlHelper html, string expression);
        public static MvcHtmlString Editor(this HtmlHelper html, string expression, object additionalViewData);
        public static MvcHtmlString Editor(this HtmlHelper html, string expression, string templateName);

        public static MvcHtmlString Editor(this HtmlHelper html, string expression, string templateName,
                                           object additionalViewData);

        public static MvcHtmlString Editor(this HtmlHelper html, string expression, string templateName,
                                           string htmlFieldName);

        public static MvcHtmlString Editor(this HtmlHelper html, string expression, string templateName,
                                           string htmlFieldName, object additionalViewData);

        public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                              Expression<Func<TModel, TValue>> expression);

        public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                              Expression<Func<TModel, TValue>> expression,
                                                              object additionalViewData);

        public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                              Expression<Func<TModel, TValue>> expression,
                                                              string templateName);

        public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                              Expression<Func<TModel, TValue>> expression,
                                                              string templateName, object additionalViewData);

        public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                              Expression<Func<TModel, TValue>> expression,
                                                              string templateName, string htmlFieldName);

        public static MvcHtmlString EditorFor<TModel, TValue>(this HtmlHelper<TModel> html,
                                                              Expression<Func<TModel, TValue>> expression,
                                                              string templateName, string htmlFieldName,
                                                              object additionalViewData);

        public static MvcHtmlString EditorForModel(this HtmlHelper html);
        public static MvcHtmlString EditorForModel(this HtmlHelper html, object additionalViewData);
        public static MvcHtmlString EditorForModel(this HtmlHelper html, string templateName);
        public static MvcHtmlString EditorForModel(this HtmlHelper html, string templateName, object additionalViewData);
        public static MvcHtmlString EditorForModel(this HtmlHelper html, string templateName, string htmlFieldName);

        public static MvcHtmlString EditorForModel(this HtmlHelper html, string templateName, string htmlFieldName,
                                                   object additionalViewData);
    }
}
