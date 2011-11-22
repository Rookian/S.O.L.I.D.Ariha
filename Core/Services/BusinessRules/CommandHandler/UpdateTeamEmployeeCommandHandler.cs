using Core.Domain.Bases.Repositories;
using Core.Domain.Model;
using Core.Services.BusinessRules.CommandMessages;

namespace Core.Services.BusinessRules.CommandHandler
{
    public class UpdateTeamEmployeeCommandHandler : ICommandHandler<UpdateTeamEmployeeCommandMessage>
    {
        private readonly ITeamEmployeeRepository _teamEmployeeRepository;
        private readonly ITeamRepository _teamRepository;

        public UpdateTeamEmployeeCommandHandler(ITeamEmployeeRepository teamEmployeeRepository, ITeamRepository teamRepository)
        {
            _teamEmployeeRepository = teamEmployeeRepository;
            _teamRepository = teamRepository;
        }

        public object Execute(UpdateTeamEmployeeCommandMessage commandMessage)
        {
            Team team = _teamRepository.GetById(commandMessage.TeamId);
            TeamEmployee teamEmployee = _teamEmployeeRepository.GetById(commandMessage.Id) ?? new TeamEmployee();

            teamEmployee.Employee = teamEmployee.Employee ?? new Employee();
            teamEmployee.Employee.FirstName = commandMessage.EmployeeFirstName;
            teamEmployee.Employee.LastName = commandMessage.EmployeeLastName;
            teamEmployee.Employee.EMail = commandMessage.EmployeeEmail;
            teamEmployee.Team = team;

            _teamEmployeeRepository.SaveOrUpdate(teamEmployee);

            return teamEmployee;
        }
    }
}