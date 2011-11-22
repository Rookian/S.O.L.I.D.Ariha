using System.Linq;
using AutoMapper;
using Core.Factories;
using Core.Interfaces;
using Core.Services.BusinessRules;
using Infrastructure.Automapper;
using Infrastructure.Automapper.ConfigurationProfiles;
using Infrastructure.CommandProcessor;
using Microsoft.Practices.ServiceLocation;
using MvcContrib.Services;
using StructureMap;
using UserInterface;
using UserInterface.ActionResults;

namespace DependencyResolution
{
    public class InitiailizeDefaultFactories
    {
        public void Configure()
        {
            UnitOfWorkFactory.GetDefault = ObjectFactory.GetInstance<IUnitOfWork>;

            StructureMapControllerFactory.CreateDependencyCallback = type => ObjectFactory.GetInstance(type);
            
            MappingService.AutoMap = Mapper.Map;

            Profiles();
            AutoMapper();

            BaseHybridViewResult.CreateDependencyCallback = type => ObjectFactory.GetInstance(type);
            AutoMappedViewResult.CreateDepencyCallBack = type => ObjectFactory.GetInstance(type);

            // Rulesengine + CommandProcessor
            DependencyResolver.InitializeWith(new StructureMapServiceLocator());
            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator());
            CommandFactory.CommandLocator = t => ObjectFactory.GetAllInstances(t).Cast<ICommandHandler>().ToArray();
        }

        private static void AutoMapper()
        {
            AutoMapperConfiguration.CreateDependencyCallback = ObjectFactory.GetInstance;
            AutoMapperConfiguration.Configure();
        }

        private static void Profiles()
        {
            EmployeeMapperProfile.CreateDependencyCallback = type => ObjectFactory.GetInstance(type);
            TeamEmployeeMapperProfile.CreateDependencyCallback = type => ObjectFactory.GetInstance(type);
        }
    }
}