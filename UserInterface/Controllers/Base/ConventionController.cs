using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using UserInterface.ActionResults;
using UserInterface.Constants;
using UserInterface.Extensions;
using System.Web.Routing;

namespace UserInterface.Controllers.Base
{
    public abstract class ConventionController : Controller
    {
        public const int PageSize = 5;

        public AutoMappedViewResult AutoMappedView<TModel>(object Model)
        {
            ViewData.Model = Model;
            return new AutoMappedViewResult(typeof(TModel))
            {
                ViewData = ViewData,
                TempData = TempData
            };
        }

        public CommandResult Command<TMessage, TResult>(TMessage message, Func<TResult, ActionResult> success,
                                                Func<TMessage, ActionResult> failure)
        {
            return new CommandResult<TMessage, TResult>(message, success, failure);
        }

        public CommandResult Command<TMessage>(TMessage message, Func<TMessage, ActionResult> result)
        {
            return new CommandResult<TMessage, TMessage>(message, result, result);
        }

        public RedirectToRouteResult RedirectToAction<TController>(Expression<Func<TController, object>> actionExpression)
        {
            string controllerName = typeof(TController).GetControllerName();
            string actionName = actionExpression.GetActionName();

            EnsureAjaxFlag();
            return RedirectToAction(actionName, controllerName);
        }

        public RedirectToRouteResult RedirectToAction<TController>(Expression<Func<TController, object>> actionExpression,
                                                                   IDictionary<string, object> dictionary)
        {
            string controllerName = typeof(TController).GetControllerName();
            string actionName = actionExpression.GetActionName();

            return RedirectToAction(actionName, controllerName,
                                    new RouteValueDictionary(dictionary));
        }

        public RedirectToRouteResult RedirectToAction<TController>(Expression<Func<TController, object>> actionExpression,
                                                                   object values)
        {
            string controllerName = typeof(TController).GetControllerName();
            string actionName = actionExpression.GetActionName();

            return RedirectToAction(actionName, controllerName,
                                    new RouteValueDictionary(values));
        }

        public bool IsAjax
        {
            get { return Request.IsAjaxRequest() || (TempData.ContainsKey(Keys.AjaxTempKey)); }
        }

        private void EnsureAjaxFlag()
        {
            if (IsAjax)
                TempData[Keys.AjaxTempKey] = true;

            else if (TempData.ContainsKey(Keys.AjaxTempKey))
                TempData.Remove(Keys.AjaxTempKey);
        }

    }
}