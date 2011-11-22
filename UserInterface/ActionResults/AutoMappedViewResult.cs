using System;
using System.Web.Mvc;
using Core.Interfaces;

namespace UserInterface.ActionResults
{
    public class AutoMappedViewResult : ViewResult
    {
        public static Func<Type, object> CreateDepencyCallBack = type => Activator.CreateInstance(type);

        public T DependencyCallback<T>()
        {
            return (T)CreateDepencyCallBack(typeof(T));
        }

        public AutoMappedViewResult(Type type)
        {
            ViewModelType = type;
        }

        public Type ViewModelType { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            ViewData.Model = DependencyCallback<IMappingService>().Map(ViewData.Model, ViewData.Model.GetType(), ViewModelType);
            base.ExecuteResult(context);
        }
    }
}