using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Common;

namespace Infrastructure.Automapper
{
    public class AutoMapperConfiguration
    {
        public static Func<Type, object> CreateDependencyCallback = type => Activator.CreateInstance(type);


        public static void Configure()
        {
            Mapper.Initialize(config =>
                                  {
                                      config.AllowNullDestinationValues = false;
                                      config.ConstructServicesUsing(type => CreateDependencyCallback(type));
                                      GetProfiles().ForEach(type => config.AddProfile((Profile)Activator.CreateInstance(type)));
                                  });

        }

        private static IEnumerable<Type> GetProfiles()
        {
            return typeof(AutoMapperConfiguration).Assembly.GetTypes()
                .Where(type => !type.IsAbstract && TypeIsOfTypeProfile(type));
        }

        private static bool TypeIsOfTypeProfile(Type type)
        {
            return typeof(Profile).IsAssignableFrom(type);
        }
    }
}