using System;
using Core.Domain.Bases;

namespace UserInterface.Models
{
    public class LoanedItemForm
    {
        public int Id { get; set; }
        public DateTime DateOfIssue { get; set; }
        public bool IsLoaned { get; set; }
        public string Name { get; set; }
        public string EmployeeName { get; set; }
        public bool IncludesCDDVD { get; set; }

        public DateTime ReleaseDate { get; set; }
        public int ReleaseNumber { get; set; }

        public string PublisherName { get; set; }
        public string PublisherHomepage { get; set; }
        public DiscriminatorValueLoanedItemEnum DiscriminatorValueLoanedItemEnum { get; set; }
    }
}