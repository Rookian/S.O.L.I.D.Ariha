namespace Core.Services.BusinessRules.CommandMessages
{
    public class UpdateTeamEmployeeCommandMessage
    {
        public string EmployeeFirstName { get; set; }

        public string EmployeeLastName { get; set; }

        public int Id { get; set; }

        public string EmployeeEmail { get; set; }

        public int TeamId { get; set; }
 
    }
}