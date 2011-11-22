using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.DataAccess.Mapping;
using NHibernate.Cfg;

namespace Infrastructure.DataAccess.SessionFactory
{
    public class ConfigurationFactory
    {
        private const string Database = "Ariha";
        private const string Server = "localhost";

        public Configuration Build()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(c => c
                                                         .Database(Database)
                                                         .TrustedConnection()
                                                         .Server(Server)
                              ))
                .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "web"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TeamMap>()).BuildConfiguration();
        }
    }
}