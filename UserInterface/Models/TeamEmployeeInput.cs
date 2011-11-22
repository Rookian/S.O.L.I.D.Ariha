using System.Collections.Generic;

namespace UserInterface.Models
{
    public class TeamEmployeeInput
    {
        public int? Id { get; set; }
        public string EmployeeLastName { get; set; }
        public string EmployeeEMail { get; set; }
        public string EmployeeFirstName { get; set; }

        public int SelectedTeam { get; set; }
        
        public IList<TeamDropDownInput> Teams { get; set; }
    }
}