using System;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.NHibernate.Mapping;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Environment = System.Environment;

namespace SchemaCreator
{
    public class Helper
    {
        private const string Database = "Ariha";
        private const string Server = @".";

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
                .ExposeConfiguration(UpdateSchema)
                .BuildSessionFactory();
        }

        private static void UpdateSchema(Configuration config)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SchemaPath);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //// 2nd create schema
            //var schema = new SchemaExport(config).SetOutputFile(Path.Combine(path, SchemaFileName));
            
            //schema.Execute(true, true, false);
            

            // when export is true, the schema will be recreated
            var update = new SchemaUpdate(config);
            update.Execute(true, true);
        }
    }
}