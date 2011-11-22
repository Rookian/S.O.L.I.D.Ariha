using Core.Domain.Bases.Repositories;
using Core.Services.BusinessRules.CommandMessages;

namespace Core.Services.BusinessRules.CommandHandler
{
    public class DeleteTeamEmployeeCommandHandler : ICommandHandler<DeleteTeamEmployeeCommandMessage>
    {
        private readonly ITeamEmployeeRepository _teamEmployeeRepository;
        
        public DeleteTeamEmployeeCommandHandler(ITeamEmployeeRepository teamEmployeeRepository)
        {
            _teamEmployeeRepository = teamEmployeeRepository;
        }

        public object Execute(DeleteTeamEmployeeCommandMessage commandMessage)
        {
            _teamEmployeeRepository.Delete(commandMessage.TeamEmployee);
            return commandMessage.TeamEmployee;
        }
    }
}