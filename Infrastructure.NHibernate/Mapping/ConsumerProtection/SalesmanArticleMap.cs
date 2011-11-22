using Core.Domain.Model.ConsumerProtection;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping.ConsumerProtection
{
    public class SalesmanArticleMap : ClassMap<SalesmanArticle>
    {
        public SalesmanArticleMap()
        {
            Id(x => x.Id)
                .Column("SalesmanArticleId")
                .GeneratedBy.Identity();

            Map(x => x.Amount);
            Map(x => x.Cost);
            Map(x => x.Date);

            References(x => x.Article)
                .Cascade.SaveUpdate()
                .Column("ArticleId");

            References(x => x.Salesman)
                .Cascade.SaveUpdate()
                .Column("SalesmanId");
        }
    }
}