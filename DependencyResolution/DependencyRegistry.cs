using AutoMapper;
using CommandProcessor;
using CommandProcessor.Commands;
using Core.Domain.Bases.Repositories;
using Core.Domain.Model;
using Core.Interfaces;
using Core.Services.BusinessRules;
using Infrastructure.Automapper;
using Infrastructure.CommandProcessor;
using Infrastructure.CommandProcessor.MessageConfiguration;
using Infrastructure.NHibernate.Repositories;
using Infrastructure.NHibernate.Session;
using Infrastructure.NHibernate.SessionFactory;
using Infrastructure.NHibernate.UnitOfWork;
using StructureMap.Configuration.DSL;
using IRulesEngine = Core.Services.IRulesEngine;
using RulesEngine = Infrastructure.CommandProcessor.RulesEngine;


namespace DependencyResolution
{
    public class DependencyRegistry : Registry
    {
        public DependencyRegistry()
        {
            // UnitOfWork
            For<IUnitOfWork>().Use<UnitOfWork>();

            // SessionBuilder
            For<ISessionBuilder>().Use<SessionBuilder>();

            // SessionFactoryBuilder
            For<ISessionFactoryBuilder>().Use<SessionFactoryBuilder>();

            // Automapper
            For<IMappingEngine>().Use(() => Mapper.Engine);
            
            // MappingService wrapping AutoMapper
            For<IMappingService>().Use<MappingService>();

            For<CommandProcessor.IRulesEngine>().Use<CommandProcessor.RulesEngine>();
            For<IRulesEngine>().Use<RulesEngine>();

            RulesEngineConfiguration.Configure(typeof(UpdateTeamEmployeeConfiguration));

            For<IMessageMapper>().Use<MessageMapper>();

            Scan(x =>
            {
                x.AssemblyContainingType<Employee>();
                x.ConnectImplementationsToTypesClosing(typeof(Command<>));
                x.ConnectImplementationsToTypesClosing(typeof(ICommandHandler<>));
            });

            Scan(x =>
            {
                x.WithDefaultConventions();
                x.AssemblyContainingType(typeof(TeamEmployeeRepository));
                x.AddAllTypesOf(typeof(Repository<>));
                x.ConnectImplementationsToTypesClosing(typeof(IRepository<>));
            });
            
            For<CommandProcessor.Interfaces.IUnitOfWork>().Use<CommandProcessorUnitOfWorkProxy>();
        }
    }
}
