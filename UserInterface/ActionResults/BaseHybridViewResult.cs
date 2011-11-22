using System;
using System.Web.Mvc;
using UserInterface.Constants;

namespace UserInterface.ActionResults
{
    public abstract class BaseHybridViewResult : ActionResult
    {
        public const string DefaultViewName = "Grid";

        public string ViewNameForAjaxRequest { get; protected set; }
        public object ViewModel { get; protected set; }

        public static Func<Type, object> CreateDependencyCallback = type => Activator.CreateInstance(type);

        public T CreateDependency<T>()
        {
            return (T)CreateDependencyCallback(typeof(T));
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            var usePartial = ShouldUsePartial(context);
            ActionResult res = GetInnerViewResult(usePartial);

            res.ExecuteResult(context);
        }

        private ActionResult GetInnerViewResult(bool usePartial)
        {
            ViewDataDictionary viewDataDictionary = new ViewDataDictionary(ViewModel);
            if (String.IsNullOrEmpty(ViewNameForAjaxRequest))
            {
                ViewNameForAjaxRequest = DefaultViewName;
            }

            if (usePartial)
            {
                return new PartialViewResult { ViewData = viewDataDictionary, ViewName = ViewNameForAjaxRequest };
            }

            return new ViewResult { ViewData = viewDataDictionary };
        }

        private static bool ShouldUsePartial(ControllerContext context)
        {
            return context.HttpContext.Request.IsAjaxRequest() || (context.Controller.TempData.ContainsKey(Keys.AjaxTempKey));
        }
    }
}