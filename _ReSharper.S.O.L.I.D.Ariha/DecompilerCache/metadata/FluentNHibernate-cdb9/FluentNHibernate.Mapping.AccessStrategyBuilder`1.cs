// Type: FluentNHibernate.Mapping.AccessStrategyBuilder`1
// Assembly: FluentNHibernate, Version=1.0.0.595, Culture=neutral, PublicKeyToken=8aa435e3cb308880
// Assembly location: H:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\FluentNHibernate.dll

using NHibernate.Properties;
using System;

namespace FluentNHibernate.Mapping
{
    public class AccessStrategyBuilder<T> : AccessStrategyBuilder
    {
        public AccessStrategyBuilder(T parent, Action<string> setter);
        public new T Property();
        public new T Field();
        public new T BackingField();
        public new T CamelCaseField();
        public new T CamelCaseField(Prefix prefix);
        public new T LowerCaseField();
        public new T LowerCaseField(Prefix prefix);
        public new T PascalCaseField(Prefix prefix);
        public new T ReadOnlyPropertyThroughCamelCaseField();
        public new T ReadOnlyPropertyThroughCamelCaseField(Prefix prefix);
        public new T ReadOnlyPropertyThroughLowerCaseField();
        public new T ReadOnlyPropertyThroughLowerCaseField(Prefix prefix);
        public new T ReadOnlyPropertyThroughPascalCaseField(Prefix prefix);
        public new T Using(string propertyAccessorAssemblyQualifiedClassName);
        public new T Using(Type propertyAccessorClassType);
        public new T Using<TPropertyAccessorClass>() where TPropertyAccessorClass : IPropertyAccessor;
        public new T NoOp();
        public new T None();
    }
}
