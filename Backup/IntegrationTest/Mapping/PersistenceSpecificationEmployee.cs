using System.Collections.Generic;
using Core.Domain.Model;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace IntegrationTest.Mapping
{
    [TestFixture]
    public class PersistenceSpecificationEmployee : PersistenceSpecificationBase
    {
        [Test]
        public void CanCorrectlyMapEmployee()
        {
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

            new PersistenceSpecification<Employee>(Session)
                .CheckProperty(p => p.EMail, "Mail")
                .CheckProperty(p => p.FirstName, "Firstname")
                .CheckProperty(p => p.Id, 1)
                .CheckProperty(p => p.LastName, "Lastname")
                // does not work because of Inverse()
                .CheckList(p => p.GetLoanedItems(), items)
                .VerifyTheMappings();
        }
    }
}
