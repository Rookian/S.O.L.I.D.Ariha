using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Core.Common;
using Core.Common.Paging;
using Core.Common.Paging.Interfaces;
using Core.Domain.Model;
using UserInterface.Models;

namespace UserInterface.Extensions
{
    public static class MvcExtensions
    {
        public static SelectList ToSelectList<T>(this IEnumerable<T> list) where T : IDropdownList, new()
        {
            IEnumerable<T> result = list ?? new List<T> { new T() };

            string value = Reflector.GetPropertyName<T>(x => x.Id);
            string text = Reflector.GetPropertyName<T>(x => x.Text);

            return new SelectList(result, value, text);
        }

        public static SelectList ToSelectList<T>(this IEnumerable<T> list, Expression<Func<T, object>> dataValueField, Expression<Func<T, object>> dataTextField) where T : new()
        {
            IEnumerable<T> result = list ?? new List<T> { new T() };

            string value = Reflector.GetPropertyName(dataValueField);
            string text = Reflector.GetPropertyName(dataTextField);

            return new SelectList(result, value, text);
        }

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                       Expression<Func<TModel, TProperty>> expression,
                                                                       IEnumerable<IDropdownList> selectList, object selectedValue)
        {
            string value = Reflector.GetPropertyName<IDropdownList>(x => x.Id);
            string text = Reflector.GetPropertyName<IDropdownList>(x => x.Text);

            int? selectedId = (int)selectedValue;

            List<IDropdownList> list = selectList.ToList();
            if (selectedId == 0)
            {
                list.Insert(0, new EmptyDropdownItem());
            }

            return htmlHelper.DropDownListFor(expression, new SelectList(list, value, text, selectedValue));
        }

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                               Expression<Func<TModel, TProperty>> expression,
                                                               IEnumerable<IDropdownList> selectList)
        {
            string value = Reflector.GetPropertyName<IDropdownList>(x => x.Id);
            string text = Reflector.GetPropertyName<IDropdownList>(x => x.Text);

            return htmlHelper.DropDownListFor(expression, new SelectList(selectList, value, text));
        }


        public static string GetControllerName(this Type controllerType)
        {
            return controllerType.Name.Replace("Controller", string.Empty);
        }

        public static string GetActionName(this LambdaExpression actionExpression)
        {
            return ((MethodCallExpression)actionExpression.Body).Method.Name;
        }

        public static string GetActionName(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.GetRequiredString("action");
        }

        public static string GetControllerName(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.Controller.GetType().GetControllerName();
        }

        public static void RenderPager(this HtmlHelper htmlHelper)
        {
            string action = GetActionName(htmlHelper);
            string controller = GetControllerName(htmlHelper);

            htmlHelper.RenderPartial("Pager", new PagerViewModel
                                                  {
                                                      Action = action,
                                                      Controller = controller,
                                                      PagerInfo = ((IPagedList)htmlHelper.ViewData.Model).PagerInfo
                                                  });

        }

        public static string StandardOverlayCreateButton(this HtmlHelper htmlHelper)
        {
            //<a href="/Employee/Create" rel="#overlay" >
            //<button type="button">
            //Create</button>
            //</a>
            string controller = GetControllerName(htmlHelper);

            TagBuilder tagBuilder = new TagBuilder("a");
            tagBuilder.Attributes.Add("href", String.Format("/{0}/{1}", controller, "Create"));
            tagBuilder.Attributes.Add("rel", "#overlay");
            tagBuilder.Attributes.Add("style", "text-decoration:none");
            tagBuilder.InnerHtml = "<button type=\"button\">Create</button>";
            return tagBuilder.ToString();
        }

        public static string EditImageButton(this HtmlHelper helper, int id)
        {
            string controller = GetControllerName(helper);
            string url = String.Format("/{0}/Edit/{1}", controller, id);
            return ImageButton(helper, url, "Edit", "/Content/Images/application_edit.png", "#overlay", id);
        }

        public static string DeleteImageButton(this HtmlHelper helper, int id)
        {
            string controller = GetControllerName(helper);
            string name = Reflector.GetPropertyName<IGridViewModel>(x => x.EditAndDeleteId);

            string url = String.Format("/{0}/Delete?{1}={2}", controller, name, id);

            return ImageButton(helper, url, "Delete", "/Content/Images/application_delete.png", "#yesno", "modalInput", id);
        }

        public static string ImageButton(this HtmlHelper helper, string url, string altText, string imageFile, string imageLinkRel, int id)
        {
            return string.Format("<a id=\"{0}\" class=\"specialbutton\" href=\"{1}\" rel=\"{2}\"><img src=\"{3}\" alt=\"{4}\" /></a>", id, url, imageLinkRel, imageFile, altText);
        }

        public static string ImageButton(this HtmlHelper helper, string url, string altText, string imageFile, string imageLinkRel, string cssClass, int id)
        {
            return string.Format("<a id=\"{0}\" class=\"{1} specialbutton\" href=\"{2}\" rel=\"{3}\"><img src=\"{4}\" alt=\"{5}\" /></a>", id, cssClass, url, imageLinkRel, imageFile, altText);
        }
    }
}