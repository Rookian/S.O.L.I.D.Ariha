using Core.Domain.Model;

namespace Core.Services.BusinessRules.CommandMessages
{
    public class DeleteTeamEmployeeCommandMessage
    {
        public TeamEmployee TeamEmployee { get; set; }
    }
}