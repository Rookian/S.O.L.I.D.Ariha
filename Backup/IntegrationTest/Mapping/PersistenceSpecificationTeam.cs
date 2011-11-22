using Core.Domain.Model;
using FluentNHibernate.Testing;
using NUnit.Framework;

namespace IntegrationTest.Mapping
{
    [TestFixture]
    public class PersistenceSpecificationTeam : PersistenceSpecificationBase
    {
        [Test]
        public void CanCorrectlyMapTeam()
        {
            new PersistenceSpecification<Team>(Session)
                .CheckProperty(p => p.Id, 1)
                .CheckProperty(p => p.Name, "Name")
                .VerifyTheMappings();
        }
    }
}
