using Core.Domain.Model.ConsumerProtection;
using FluentNHibernate.Mapping;

namespace Infrastructure.DataAccess.Mapping.ConsumerProtection
{
    public class ArticleMap : ClassMap<Article>
    {
        public ArticleMap()
        {
            Id(x => x.Id)
                .Column("ArticleId")
                .GeneratedBy.Identity();

            Map(x => x.Description);

            References(x => x.GoodsGroup)
                .Column("GoodsGroupId")
                .Cascade.SaveUpdate();

        }
    }
}