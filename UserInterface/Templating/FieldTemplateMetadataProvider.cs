using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace UserInterface.Templating
{
    public class FieldTemplateMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            DataAnnotationsModelMetadata result = (DataAnnotationsModelMetadata) base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            string templateName = attributes.OfType<ITemplateField>()
                                            .Select(field => field.TemplateName)
                                            .LastOrDefault();

            return new FieldTemplateMetadata(this, containerType, modelAccessor, modelType, propertyName, attributes.OfType<DisplayColumnAttribute>().FirstOrDefault(), attributes)
                       {
                           TemplateHint = !string.IsNullOrEmpty(templateName) ? templateName : result.TemplateHint,
                           HideSurroundingHtml = result.HideSurroundingHtml,
                           DataTypeName = result.DataTypeName,
                           IsReadOnly = result.IsReadOnly,
                           NullDisplayText = result.NullDisplayText,
                           DisplayFormatString = result.DisplayFormatString,
                           ConvertEmptyStringToNull = result.ConvertEmptyStringToNull,
                           EditFormatString = result.EditFormatString,
                           ShowForDisplay = result.ShowForDisplay,
                           ShowForEdit = result.ShowForEdit,
                           DisplayName = result.DisplayName
                       };
        }
    }
}