using System.ComponentModel;

namespace UserInterface.Models
{
    public class TeamEmployeeForm : IGridViewModel
    {
        public int EditAndDeleteId { get; set; }

        [DisplayName("First name")]
        public string EmployeeFirstName { get; set; }

        [DisplayName("Last name")]
        public string EmployeeLastName { get; set; }

        [DisplayName("E-Mail")]
        public string EmployeeEMail { get; set; }

        [DisplayName("Team")]
        public string TeamName { get; set; }
    }
}