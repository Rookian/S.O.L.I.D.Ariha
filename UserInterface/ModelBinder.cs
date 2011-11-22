using System.Web.Mvc;
using Core.Common;

namespace UserInterface
{
    public class ModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProvider = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            //valueProvider.AttemptedValue

            return base.BindModel(controllerContext, bindingContext);
        }
    }
}