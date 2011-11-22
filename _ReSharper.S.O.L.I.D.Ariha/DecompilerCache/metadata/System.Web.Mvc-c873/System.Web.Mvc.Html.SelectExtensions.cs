// Type: System.Web.Mvc.Html.SelectExtensions
// Assembly: System.Web.Mvc, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files (x86)\Microsoft ASP.NET\ASP.NET MVC 2\Assemblies\System.Web.Mvc.dll

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    public static class SelectExtensions
    {
        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name);
        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, string optionLabel);

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name,
                                                 IEnumerable<SelectListItem> selectList);

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name,
                                                 IEnumerable<SelectListItem> selectList, object htmlAttributes);

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name,
                                                 IEnumerable<SelectListItem> selectList,
                                                 IDictionary<string, object> htmlAttributes);

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name,
                                                 IEnumerable<SelectListItem> selectList, string optionLabel);

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name,
                                                 IEnumerable<SelectListItem> selectList, string optionLabel,
                                                 object htmlAttributes);

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name,
                                                 IEnumerable<SelectListItem> selectList, string optionLabel,
                                                 IDictionary<string, object> htmlAttributes);

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                       Expression<Func<TModel, TProperty>> expression,
                                                                       IEnumerable<SelectListItem> selectList);

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                       Expression<Func<TModel, TProperty>> expression,
                                                                       IEnumerable<SelectListItem> selectList,
                                                                       object htmlAttributes);

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                       Expression<Func<TModel, TProperty>> expression,
                                                                       IEnumerable<SelectListItem> selectList,
                                                                       IDictionary<string, object> htmlAttributes);

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                       Expression<Func<TModel, TProperty>> expression,
                                                                       IEnumerable<SelectListItem> selectList,
                                                                       string optionLabel);

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                       Expression<Func<TModel, TProperty>> expression,
                                                                       IEnumerable<SelectListItem> selectList,
                                                                       string optionLabel, object htmlAttributes);

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                       Expression<Func<TModel, TProperty>> expression,
                                                                       IEnumerable<SelectListItem> selectList,
                                                                       string optionLabel,
                                                                       IDictionary<string, object> htmlAttributes);

        public static MvcHtmlString ListBox(this HtmlHelper htmlHelper, string name);

        public static MvcHtmlString ListBox(this HtmlHelper htmlHelper, string name,
                                            IEnumerable<SelectListItem> selectList);

        public static MvcHtmlString ListBox(this HtmlHelper htmlHelper, string name,
                                            IEnumerable<SelectListItem> selectList, object htmlAttributes);

        public static MvcHtmlString ListBox(this HtmlHelper htmlHelper, string name,
                                            IEnumerable<SelectListItem> selectList,
                                            IDictionary<string, object> htmlAttributes);

        public static MvcHtmlString ListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                  Expression<Func<TModel, TProperty>> expression,
                                                                  IEnumerable<SelectListItem> selectList);

        public static MvcHtmlString ListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                  Expression<Func<TModel, TProperty>> expression,
                                                                  IEnumerable<SelectListItem> selectList,
                                                                  object htmlAttributes);

        public static MvcHtmlString ListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                  Expression<Func<TModel, TProperty>> expression,
                                                                  IEnumerable<SelectListItem> selectList,
                                                                  IDictionary<string, object> htmlAttributes);
    }
}
