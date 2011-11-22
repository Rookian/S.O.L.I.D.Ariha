using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Core.ReflectorExtension;

namespace UserInterface.Extensions
{
    public static class MvcExtensions
    {
        public static SelectList CreateSelectList<T>(List<T> list, Expression<Func<T, object>> dataValueField, Expression<Func<T, object>> dataTextField)
        {
            string valueMember = Reflector.GetPropertyName(dataValueField);
            string textMember = Reflector.GetPropertyName(dataTextField);

            return new SelectList(list, valueMember, textMember);
        }

        public static SelectList CreateSelectList<T>(List<T> list, Expression<Func<T, object>> dataValueField, Expression<Func<T, object>> dataTextField, object selectedValue)
        {
            string valueMember = Reflector.GetPropertyName(dataValueField);
            string textMember = Reflector.GetPropertyName(dataTextField);

            return new SelectList(list, valueMember, textMember, selectedValue);
        }

        public static string GetControllerName(this Type controllerType)
        {
            return controllerType.Name.Replace("Controller", string.Empty);
        }

        public static string GetActionName(this LambdaExpression actionExpression)
        {
            return ((MethodCallExpression)actionExpression.Body).Method.Name;
        }

        public static string StandardOverlayCreateButton(this HtmlHelper htmlHelper)
        {
            //<a href="/Employee/Create" rel="#overlay" >
            //<button type="button">
            //Create</button>
            //</a>

            string link = HtmlHelper.GenerateLink(htmlHelper.ViewContext.RequestContext, RouteTable.Routes, String.Empty,
                                                  "Default", "Create", htmlHelper.ViewContext.Controller.GetType().GetControllerName(), null,
                                                  new Dictionary<string, object> { { "rel", "#overlay" } }).Remove(9,1);

            link = link.Insert(link.IndexOf('>') + 1, "<button type=\"button\">Create</button>");

            return link;
        }
    }
}