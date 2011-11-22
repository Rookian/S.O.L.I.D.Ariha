using Core.Domain.Model.ConsumerProtection;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping.ConsumerProtection
{
    public class GoodsGroupMap : ClassMap<GoodsGroup>
    {
        public GoodsGroupMap()
        {
            Id(x => x.Id)
                .GeneratedBy.Identity()
                .Column("GoodsGroupId");

            Map(x => x.Description);
        }
    }
}