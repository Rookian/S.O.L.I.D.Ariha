using Core.Domain.Model;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace IntegrationTest.Mapping
{
    [TestFixture]
    public class PersistenceSpecificationMagazine : PersistenceSpecificationBase
    {
        [Test]
        public void CanCorrectlyMapMagazineWithComponentsAndReference()
        {
            new PersistenceSpecification<Magazine>(Session)
                .CheckProperty((p => p.DateOfIssue), DateTime)
                .CheckProperty(p => p.Id, 1)
                .CheckProperty(p => p.IncludesCDDVD, true)
                .CheckProperty(p => p.IsLoaned, false)
                .CheckReference(p => p.LoanedBy, new Employee { EMail = "", FirstName = "Alex", LastName = "Mueller" })
                .CheckProperty(p => p.Name, "My Book")
                .CheckProperty(p => p.Publisher, new Publisher { PublisherHomepage = "www.google.de", PublisherName = "google" })
                .CheckProperty(p => p.Release, new Release { ReleaseDate = DateTime, ReleaseNumber = 1 })
                .VerifyTheMappings();
        }
    }
}
