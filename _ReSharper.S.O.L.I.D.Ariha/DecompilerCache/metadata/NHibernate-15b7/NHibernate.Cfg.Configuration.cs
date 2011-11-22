// Type: NHibernate.Cfg.Configuration
// Assembly: NHibernate, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4
// Assembly location: H:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\Nhibernate\NHibernate.dll

using Iesi.Collections;
using Iesi.Collections.Generic;
using log4net;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;
using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Id;
using NHibernate.Mapping;
using NHibernate.Proxy;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Type;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace NHibernate.Cfg
{
    [Serializable]
    public class Configuration : ISerializable
    {
        public const string MappingSchemaXMLNS = "urn:nhibernate-mapping-2.2";
        public const string DefaultHibernateCfgFileName = "hibernate.cfg.xml";
        private static readonly ILog log;
        private static readonly IInterceptor emptyInterceptor;
        protected IList<IAuxiliaryDatabaseObject> auxiliaryDatabaseObjects;
        protected IDictionary<string, PersistentClass> classes;
        protected IDictionary<string, Collection> collections;
        protected IDictionary<Table, Mappings.ColumnNames> columnNameBindingPerTable;
        private string currentDocumentName;
        private string defaultAssembly;
        private string defaultNamespace;
        private EventListeners eventListeners;
        protected Iesi.Collections.Generic.ISet<ExtendsQueueEntry> extendsQueue;
        protected Queue<FilterSecondPassArgs> filtersSecondPasses;
        private IInterceptor interceptor;
        private IMapping mapping;
        private MappingsQueue mappingsQueue;
        private INamingStrategy namingStrategy;
        private bool preMappingBuildProcessed;
        private IDictionary<string, string> properties;
        protected IList<Mappings.PropertyReference> propertyReferences;
        private XmlSchemas schemas;
        protected IList<SecondPassCommand> secondPasses;
        protected internal SettingsFactory settingsFactory;
        protected IDictionary<string, Mappings.TableDescription> tableNameBinding;
        protected IDictionary<string, Table> tables;
        protected IDictionary<string, TypeDef> typeDefs;
        static Configuration();
        public Configuration(SerializationInfo info, StreamingContext context);
        protected Configuration(SettingsFactory settingsFactory);
        public Configuration();
        public ICollection<PersistentClass> ClassMappings { get; }
        public ICollection<Collection> CollectionMappings { get; }
        private ICollection<Table> TableMappings { get; }
        public IDictionary<string, NamedQueryDefinition> NamedQueries { get; protected set; }
        public IEntityNotFoundDelegate EntityNotFoundDelegate { get; set; }
        public EventListeners EventListeners { get; }
        public IInterceptor Interceptor { get; set; }
        public IDictionary<string, string> Properties { get; set; }
        public IDictionary<string, string> Imports { get; protected set; }
        public IDictionary<string, NamedSQLQueryDefinition> NamedSQLQueries { get; protected set; }
        public INamingStrategy NamingStrategy { get; }
        public IDictionary<string, ResultSetMappingDefinition> SqlResultSetMappings { get; protected set; }
        public IDictionary<string, FilterDefinition> FilterDefinitions { get; protected set; }
        public IDictionary<string, ISQLFunction> SqlFunctions { get; protected set; }
        private XmlSchemas Schemas { get; set; }

        #region ISerializable Members

        public void GetObjectData(SerializationInfo info, StreamingContext context);

        #endregion

        private T GetSerialedObject<T>(SerializationInfo info, string name);
        protected void Reset();
        private void InitBlock();
        public virtual IMapping BuildMapping();
        public PersistentClass GetClassMapping(Type persistentClass);
        public PersistentClass GetClassMapping(string entityName);
        public Collection GetCollectionMapping(string role);
        public Configuration AddFile(string xmlFile);
        public Configuration AddFile(FileInfo xmlFile);
        private static void LogAndThrow(Exception exception);
        public Configuration AddXmlFile(string xmlFile);
        public Configuration AddXml(string xml);
        public Configuration AddXml(string xml, string name);
        public Configuration AddXmlString(string xml);
        public Configuration AddUrl(string url);
        public Configuration AddUrl(Uri url);
        public Configuration AddDocument(XmlDocument doc);
        public Configuration AddDocument(XmlDocument doc, string name);
        private void AddValidatedDocument(NamedXmlDocument doc);
        public Mappings CreateMappings(Dialect dialect);
        private void ProcessPreMappingBuildProperties();
        private void ConfigureCollectionTypeFactory();
        public Configuration AddInputStream(Stream xmlInputStream);
        public Configuration AddInputStream(Stream xmlInputStream, string name);
        public Configuration AddResource(string path, Assembly assembly);
        public Configuration AddResources(IEnumerable<string> paths, Assembly assembly);
        public Configuration AddClass(Type persistentClass);
        public Configuration AddAssembly(string assemblyName);
        public Configuration AddAssembly(Assembly assembly);
        private static IList<string> GetAllHbmXmlResourceNames(Assembly assembly);
        public Configuration AddDirectory(DirectoryInfo dir);
        public string[] GenerateDropSchemaScript(Dialect dialect);
        public static bool IncludeAction(SchemaAction actionsSource, SchemaAction includedAction);
        public string[] GenerateSchemaCreationScript(Dialect dialect);
        private void Validate();
        private void ValidateFilterDefs();
        private void ValidateCollections();
        private void ValidateEntities();

        private static ICollection<string> ValidateProxyInterface(PersistentClass persistentClass,
                                                                  IProxyValidator validator);

        public virtual void BuildMappings();
        private void SecondPassCompile();
        private void SecondPassCompileForeignKeys(Table table, ISet done);
        private EventListeners GetInitializedEventListeners();
        protected virtual void ConfigureProxyFactoryFactory();
        public ISessionFactory BuildSessionFactory();
        public Configuration SetDefaultAssembly(string newDefaultAssembly);
        public Configuration SetDefaultNamespace(string newDefaultNamespace);
        public Configuration SetInterceptor(IInterceptor newInterceptor);
        public Configuration SetProperties(IDictionary<string, string> newProperties);
        public Configuration AddProperties(IDictionary<string, string> additionalProperties);
        public Configuration SetProperty(string name, string value);
        public string GetProperty(string name);
        private void AddProperties(IHibernateConfiguration hc);
        public Configuration Configure();
        public Configuration Configure(string fileName);
        private Configuration Configure(string fileName, bool ignoreSessionFactoryConfig);
        public Configuration Configure(Assembly assembly, string resourceName);
        public Configuration Configure(XmlReader textReader);
        protected Configuration DoConfigure(IHibernateConfiguration hc);
        internal RootClass GetRootClassMapping(string clazz);
        internal RootClass GetRootClassMapping(Type clazz);
        public Configuration SetCacheConcurrencyStrategy(string clazz, string concurrencyStrategy);
        public void SetCacheConcurrencyStrategy(string clazz, string concurrencyStrategy, string region);

        internal void SetCacheConcurrencyStrategy(string clazz, string concurrencyStrategy, string region,
                                                  bool includeLazy);

        public Configuration SetCollectionCacheConcurrencyStrategy(string collectionRole, string concurrencyStrategy);

        internal void SetCollectionCacheConcurrencyStrategy(string collectionRole, string concurrencyStrategy,
                                                            string region);

        private Settings BuildSettings();
        public Configuration SetNamingStrategy(INamingStrategy newNamingStrategy);
        public void AddFilterDefinition(FilterDefinition definition);
        public void AddAuxiliaryDatabaseObject(IAuxiliaryDatabaseObject obj);
        public void AddSqlFunction(string functionName, ISQLFunction sqlFunction);
        public NamedXmlDocument LoadMappingDocument(XmlReader hbmReader, string name);
        public Configuration AddXmlReader(XmlReader hbmReader);
        public Configuration AddXmlReader(XmlReader hbmReader, string name);
        private void AddDocumentThroughQueue(NamedXmlDocument document);
        private void ProcessMappingsQueue();
        private void ValidationHandler(object o, ValidationEventArgs args);
        private static string GetDefaultConfigurationFilePath();
        public void SetListeners(ListenerType type, string[] listenerClasses);
        public void SetListener(ListenerType type, object listener);
        private void ClearListeners(ListenerType type);
        public void SetListeners(ListenerType type, object[] listeners);
        public string[] GenerateSchemaUpdateScript(Dialect dialect, DatabaseMetadata databaseMetadata);
        public void ValidateSchema(Dialect dialect, DatabaseMetadata databaseMetadata);
        private IEnumerable<IPersistentIdentifierGenerator> IterateGenerators(Dialect dialect);

        #region Nested type: Mapping

        [Serializable]
        private class Mapping : IMapping
        {
            private readonly Configuration configuration;
            public Mapping(Configuration configuration);

            #region IMapping Members

            public IType GetIdentifierType(string className);
            public string GetIdentifierPropertyName(string className);
            public IType GetReferencedPropertyType(string className, string propertyName);
            public bool HasNonIdentifierPropertyNamedId(string className);

            #endregion

            private PersistentClass GetPersistentClass(string className);
        }

        #endregion
    }
}
