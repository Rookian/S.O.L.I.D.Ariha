using CommandProcessor.Configuration;
using Core.Services.BusinessRules.CommandMessages;
using UserInterface.Models;

namespace Infrastructure.CommandProcessor.MessageConfiguration
{
    public class UpdateTeamEmployeeConfiguration : MessageDefinition<TeamEmployeeInput>
    {
        public UpdateTeamEmployeeConfiguration()
        {
            Execute<UpdateTeamEmployeeCommandMessage>();
        }
    }
}