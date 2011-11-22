using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.DataAccess.Mapping;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace IntegrationTest.Mapping
{
    [TestFixture]
    public class PersistenceSpecificationBase
    {
        public ISession Session;

        // only DateTime.Today is suitable for testing
        public readonly DateTime DateTime = DateTime.Today;

        [SetUp]
        public void PersistenceSpecificationTest()
        {
            var cfg = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.InMemory().UseReflectionOptimizer())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<EmployeeMap>())
                .BuildConfiguration();

            Session = cfg.BuildSessionFactory().OpenSession();
            new SchemaExport(cfg).Execute(false, true, false, Session.Connection, null);
        }       
    }
}