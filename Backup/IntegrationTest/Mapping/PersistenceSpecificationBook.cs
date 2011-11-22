using FluentNHibernate.Testing;
using Core.Domain.Model;
using NUnit.Framework;

namespace IntegrationTest.Mapping
{
    [TestFixture]
    public class PersistenceSpecificationBook : PersistenceSpecificationBase
    {
        [Test]
        public void CanCorrectlyMapBookWithComponentsAndReference()
        {
            new PersistenceSpecification<Book>(Session)
                .CheckProperty(p => p.Author, "Alex")
                .CheckProperty((p => p.DateOfIssue), DateTime)
                .CheckProperty(p => p.Id, 1)
                .CheckProperty(p => p.IncludesCDDVD, true)
                .CheckProperty(p => p.Isbn, "rder93q43949éwr")
                .CheckProperty(p => p.IsLoaned, false)
                .CheckReference(p => p.LoanedBy, new Employee { EMail = "", FirstName = "Alex", LastName = "Mueller" })
                .CheckProperty(p => p.Name, "My Book")
                .CheckProperty(p => p.Publisher, new Publisher { PublisherHomepage = "www.google.de", PublisherName = "google" })
                .CheckProperty(p => p.Release, new Release { ReleaseDate = DateTime, ReleaseNumber = 1 })
                .VerifyTheMappings();
        }
    }
}
