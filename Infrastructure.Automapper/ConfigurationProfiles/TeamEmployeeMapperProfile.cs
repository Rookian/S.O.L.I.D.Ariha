using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Core.Domain.Bases.Repositories;
using Core.Domain.Model;
using Core.Services.BusinessRules.CommandMessages;
using UserInterface.Controllers;
using UserInterface.Models;

namespace Infrastructure.Automapper.ConfigurationProfiles
{
    public class TeamEmployeeMapperProfile : Profile
    {
        public static Func<Type, object> CreateDependencyCallback = type => Activator.CreateInstance(type);

        public T CreateDependency<T>()
        {
            return (T)CreateDependencyCallback(typeof(T));
        }

        protected override void Configure()
        {
            CreateMap<Team, TeamDropDownInput>()
                .ForMember(d => d.Id, x => x.MapFrom(s => s.Id))
                .ForMember(d => d.Text, x => x.MapFrom(s => s.Name));

            CreateMap<TeamEmployee, TeamEmployeeForm>()
                .ForMember(d => d.EditAndDeleteId, s => s.MapFrom(x => x.Id));

            CreateMap<TeamEmployee, TeamEmployeeInput>()
                .ForMember(x => x.Teams, opt => opt.MapFrom(x => GetTeamEmployeeInputs()))
                .ForMember(d => d.SelectedTeam, s => s.MapFrom(x => x.Team == null ? 0 : x.Team.Id));

            CreateMap<TeamEmployeeInput, UpdateTeamEmployeeCommandMessage>()
                .ForMember(x => x.TeamId, y => y.MapFrom(x => x.SelectedTeam));

            CreateMap<DeleteTeamEmployeeInput, DeleteTeamEmployeeCommandMessage>()
                .ForMember(dest => dest.TeamEmployee, opt => opt.MapFrom(x => CreateDependency<ITeamEmployeeRepository>().GetById(x.EditAndDeleteId)));

            CreateMap<ReminderType, SelectListItem>()
                .ForMember(d => d.Text, o => o.MapFrom(x => x))
                .ForMember(d => d.Value, o => o.MapFrom(x => (int) x));

            CreateMap<ReminderType, RemindersViewModel>()
                .ForMember(d => d.Reminder, opt => opt.MapFrom(Mapper.Map<ReminderType, SelectListItem[]>));

            //CreateMap<string, DateTime>().ForMember(d => d, opt => opt.MapFrom(x => DateTime.Parse(x)));
            CreateMap<string, DateTime>().ConvertUsing<StringToDateTimeConverter>();
        }

        private IEnumerable<TeamDropDownInput> GetTeamEmployeeInputs()
        {
            Team[] teams = CreateDependency<ITeamRepository>().GetAll();
            return Mapper.Map<Team[], TeamDropDownInput[]>(teams);
        }
    }
}