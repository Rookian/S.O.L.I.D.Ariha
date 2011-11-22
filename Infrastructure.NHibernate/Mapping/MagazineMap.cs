using Core.Domain.Bases.Enumerations;
using Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping
{
    public sealed class MagazineMap : SubclassMap<Magazine>
    {
        public MagazineMap()
        {
            // identity mapping
            DiscriminatorValue(DiscriminatorValueLoanedItemEnum.Magazine);
        }
    }
}