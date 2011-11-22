// Type: System.Web.Mvc.DefaultModelBinder
// Assembly: System.Web.Mvc, Version=2.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\Program Files\Microsoft ASP.NET\ASP.NET MVC 2\Assemblies\System.Web.Mvc.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Web.Mvc
{
    public class DefaultModelBinder : IModelBinder
    {
        protected internal ModelBinderDictionary Binders { get; set; }
        public static string ResourceClassKey { get; set; }

        #region IModelBinder Members

        public virtual object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);

        #endregion

        protected virtual void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext,
                                            PropertyDescriptor propertyDescriptor);

        protected virtual object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext,
                                             Type modelType);

        protected static string CreateSubIndexName(string prefix, int index);
        protected static string CreateSubIndexName(string prefix, string index);
        protected internal static string CreateSubPropertyName(string prefix, string propertyName);

        protected IEnumerable<PropertyDescriptor> GetFilteredModelProperties(ControllerContext controllerContext,
                                                                             ModelBindingContext bindingContext);

        protected virtual PropertyDescriptorCollection GetModelProperties(ControllerContext controllerContext,
                                                                          ModelBindingContext bindingContext);

        protected virtual object GetPropertyValue(ControllerContext controllerContext,
                                                  ModelBindingContext bindingContext,
                                                  PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder);

        protected virtual ICustomTypeDescriptor GetTypeDescriptor(ControllerContext controllerContext,
                                                                  ModelBindingContext bindingContext);

        protected static bool IsModelValid(ModelBindingContext bindingContext);
        protected virtual void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext);
        protected virtual bool OnModelUpdating(ControllerContext controllerContext, ModelBindingContext bindingContext);

        protected virtual void OnPropertyValidated(ControllerContext controllerContext,
                                                   ModelBindingContext bindingContext,
                                                   PropertyDescriptor propertyDescriptor, object value);

        protected virtual bool OnPropertyValidating(ControllerContext controllerContext,
                                                    ModelBindingContext bindingContext,
                                                    PropertyDescriptor propertyDescriptor, object value);

        protected virtual void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext,
                                           PropertyDescriptor propertyDescriptor, object value);
    }
}
