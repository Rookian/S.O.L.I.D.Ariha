// Type: FluentNHibernate.Cfg.FluentConfiguration
// Assembly: FluentNHibernate, Version=1.0.0.595, Culture=neutral, PublicKeyToken=8aa435e3cb308880
// Assembly location: H:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\FluentNHibernate.dll

using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using System;

namespace FluentNHibernate.Cfg
{
    public class FluentConfiguration
    {
        public FluentConfiguration Database(Func<IPersistenceConfigurer> config);
        public FluentConfiguration Database(IPersistenceConfigurer config);
        public FluentConfiguration Mappings(Action<MappingConfiguration> mappings);
        public FluentConfiguration ExposeConfiguration(Action<Configuration> config);
        public ISessionFactory BuildSessionFactory();
        public Configuration BuildConfiguration();
    }
}
