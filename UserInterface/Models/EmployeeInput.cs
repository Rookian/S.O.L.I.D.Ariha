using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserInterface.Models
{
    public class EmployeeInput
    {
        [Required(ErrorMessage="Pflichtfeld")]
        public string LastName { get; set; }

        [DisplayName("Vorname")]
        public string FirstName { get; set; }

        [DisplayName("E-Mail")]
        public string EMail { get; set; }

        [Required(AllowEmptyStrings = true)]
        public int Id { get; set; }
    }
}