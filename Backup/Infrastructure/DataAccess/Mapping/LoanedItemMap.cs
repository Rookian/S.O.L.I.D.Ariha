using Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace Infrastructure.DataAccess.Mapping
{
    public sealed class LoanedItemMap : ClassMap<LoanedItem>
    {
        public LoanedItemMap()
        {
            Id(p => p.Id)
                .Column("LoanedItemId")
                .GeneratedBy.Identity();

            // column mapping
            Map(p => p.DateOfIssue);
            Map(p => p.IncludesCDDVD);
            Map(p => p.IsLoaned);
            Map(p => p.Name);

            // component mapping
            // Publisher
            Component(p => p.Publisher, m =>
            {
                m.Map(x => x.PublisherName);
                m.Map(x => x.PublisherHomepage);
            });

            // Release
            Component(p => p.Release, m =>
            {
                m.Map(x => x.ReleaseDate);
                m.Map(x => x.ReleaseNumber);
            });

            //Table Per Class Hierarchy Inheritance

            DiscriminateSubClassesOnColumn("LoanedItemType");

            // reference/association
            References(p => p.LoanedBy)
                .Column("EmployeeID")
                .Cascade.SaveUpdate()
                .LazyLoad();
        }
    }
}
