using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace UserInterface
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        public static Func<Type, object> CreateDependencyCallback = type =>
        {
            throw new  InvalidOperationException("The dependency callback for the StructureMapControllerFactory is not configured!");
        };

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }
            return  CreateDependencyCallback(controllerType) as Controller;
        }
    }
}
