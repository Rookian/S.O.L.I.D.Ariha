// Type: FluentNHibernate.Mapping.SubclassMap`1
// Assembly: FluentNHibernate, Version=1.0.0.595, Culture=neutral, PublicKeyToken=8aa435e3cb308880
// Assembly location: C:\Nhibernate\FluentNhibernate\FluentNHibernate.dll

using System;
using System.Diagnostics;
using FluentNHibernate.Mapping.Providers;
using FluentNHibernate.MappingModel.ClassBased;

namespace FluentNHibernate.Mapping
{
    public class SubclassMap<T> : ClasslikeMapBase<T>, IIndeterminateSubclassMappingProvider
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public SubclassMap<T> Not { get; }

        #region IIndeterminateSubclassMappingProvider Members

        ISubclassMapping IIndeterminateSubclassMappingProvider.GetSubclassMapping(ISubclassMapping mapping);

        #endregion

        public void Abstract();
        public void DynamicInsert();
        public void DynamicUpdate();
        public void LazyLoad();
        public void Proxy<TProxy>();
        public void Proxy(Type proxyType);
        public void SelectBeforeUpdate();
        public void Subclass<TSubclass>(Action<SubclassMap<TSubclass>> subclassDefinition);
        public void DiscriminatorValue(object discriminatorValue);
        public void Table(string table);
        public void Schema(string schema);
        public void Check(string constraint);
        public void KeyColumn(string column);
        public void Subselect(string subselect);
        public void Persister<TPersister>();
        public void Persister(Type type);
        public void Persister(string type);
        public void BatchSize(int batchSize);
        public void Join(string tableName, Action<JoinPart<T>> action);
    }
}