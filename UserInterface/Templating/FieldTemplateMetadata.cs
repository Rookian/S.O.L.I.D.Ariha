using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace UserInterface.Templating
{
    public class FieldTemplateMetadata : DataAnnotationsModelMetadata
    {
        public FieldTemplateMetadata(DataAnnotationsModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, DisplayColumnAttribute displayColumnAttribute, IEnumerable<Attribute> attributes)
            : base(provider, containerType, modelAccessor, modelType, propertyName, displayColumnAttribute)
        {
            Attributes = new List<Attribute>(attributes);
        }

        public IList<Attribute> Attributes { get; private set; }
    }
}