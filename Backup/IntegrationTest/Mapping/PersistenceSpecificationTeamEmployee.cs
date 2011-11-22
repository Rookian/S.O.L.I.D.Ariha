using System.Collections.Generic;
using Core.Domain.Model;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace IntegrationTest.Mapping
{
    [TestFixture]
    public class PersistenceSpecificationTeamEmployee : PersistenceSpecificationBase
    {
        [Test]
        public void CanCorrectlyMapRelationShipBetweenTeamAndEmployee()
        {
            var teams = new List<Team>();

            var employee1 = new Employee { EMail = "Mail", FirstName = "Firstname", LastName = "Lastname" };

            var team1 = new Team { Name = "Team1" };
            var team2 = new Team { Name = "Team2" };

            employee1.AddTeam(team1);
            employee1.AddTeam(team2);

            teams.Add(team1);
            teams.Add(team2);

            var items = new List<LoanedItem>
            {
                new Book
                    {
                        Author = "Alex",
                        DateOfIssue = DateTime,
                        IncludesCDDVD = true,
                        Isbn = "ISBN",
                        IsLoaned = true,
                        Name = "Name"
                    },
                new Magazine
                    {
                        DateOfIssue = DateTime,
                        IncludesCDDVD = false,
                        IsLoaned = true,
                        Name = "Name"
                    }
            };

            // only Employee can be tested correctly, because it is responsible for saving
            new PersistenceSpecification<Employee>(Session)
                .CheckProperty(p => p.EMail, "E-Mail")
                .CheckProperty(p => p.FirstName, "Firstname")
                .CheckProperty(p => p.LastName, "Lastname")
                .CheckList(p => p.GetLoanedItems(), items)
                .CheckList(p => p.GetTeams(), teams)
                .VerifyTheMappings();
        }
    }
}
