using System;
using AutoMapper;
using Core.Domain.Bases.Repositories;
using Core.Domain.Model;
using Core.Services.BusinessRules.CommandMessages;
using UserInterface.Models;

namespace Infrastructure.Automapper.ConfigurationProfiles
{
    public class EmployeeMapperProfile : Profile
    {
        public static Func<Type, object> CreateDependencyCallback = type => Activator.CreateInstance(type);
        
        public T CreateDependency<T>()
        {
            return (T)CreateDependencyCallback(typeof(T));
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Employee, EmployeeInput>();
            Mapper.CreateMap<EmployeeInput, Employee>().ConstructUsing(input => CreateDependency<IEmployeeRepository>().GetById(input.Id) ?? new Employee());
            Mapper.CreateMap<EmployeeInput, UpdateTeamEmployeeCommandMessage>();

        }
    }
}