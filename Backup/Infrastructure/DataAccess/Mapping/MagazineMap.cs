using Core.Domain.Model;
using FluentNHibernate.Mapping;
using Core.Domain.Bases;
namespace Infrastructure.DataAccess.Mapping
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