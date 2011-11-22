using System;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.DataAccess.Mapping;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Environment = System.Environment;

namespace SchemaCreator
{
    public class Helper
    {
        private const string Database = "Ariha";
        private const string Server = "localhost";

        private const string MappingsPath = "Mappings";
        private const string SchemaPath = "Schema";
        private const string SchemaFileName = "Schema.SQL";

        public static ISessionFactory CreateSessionFactory()
        {
            string mappingFilePath = Path.Combine(Environment.CurrentDirectory, MappingsPath);

            if (!Directory.Exists(mappingFilePath))
                Directory.CreateDirectory(mappingFilePath);

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(c =>
                                                c.Database(Database)
                                                    .TrustedConnection()
                                                    .Server(Server)
                              ))
                .Mappings(m => m.FluentMappings
                                   .AddFromAssemblyOf<TeamMap>()
                                   .ExportTo(mappingFilePath))
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        public static void BuildSchema(Configuration config)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SchemaPath);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            // when export is true, the schema will be recreate
            var schema = new SchemaExport(config).SetOutputFile(Path.Combine(path, SchemaFileName));
            
            // 2nd create schema
            schema.Create(true, true);
        }
    }
}