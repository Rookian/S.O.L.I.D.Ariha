using System.Collections.Generic;
using Core.Common;
using Core.Domain.Bases.Repositories.ConsumerProtection;
using Core.Domain.Model.ConsumerProtection;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect.Function;
using NHibernate.SqlCommand;
using NHibernate.Transform;

namespace Infrastructure.NHibernate.Repositories.ConsumerProtection
{
    //SELECT 	SUM (sa.Amount) as 'SumAmount',
    //    SUM(sa.Cost) as 'SumCost', 
    //    gg.[Description] as 'Goodsgroup', Month(sa.[Date]) as 'Month' 
    //FROM SalesmanArticle sa
    //INNER JOIN Article a
    //    ON a.ArticleId = sa.ArticleId
    //INNER JOIN GoodsGroup gg
    //    ON gg.GoodsGroupId = a.GoodsGroupId
    //GROUP BY gg.[Description], Month(sa.[Date])
    //ORDER BY 'Month', gg.[Description]

    public class SalesmanArticleRepository : Repository<SalesmanArticle>, ISalesmanArticleRepository
    {
        #region ISalesmanArticleRepository Members

        public IList<SalesmanArticleGroupedByMonthAndDescription> GetSalesmanArticleGroupedByMonthAndDescription()
        {
            var multiply = new VarArgsSQLFunction("(", "*", ")");

            // typesafe properties
            string article = typeof (Article).Name;
            string goodsGroup = typeof (GoodsGroup).Name;
            string salesmanArticle = typeof (SalesmanArticle).Name;

            string amount = Reflector.GetPropertyName<SalesmanArticle>(x => x.Amount);
            string cost = Reflector.GetPropertyName<SalesmanArticle>(x => x.Cost);
            string description = Reflector.GetPropertyName<SalesmanArticle>(x => x.Article.GoodsGroup.Description);
            string descriptionformat = string.Format("{0}.{1}", goodsGroup, description);
            string date = Reflector.GetPropertyName<SalesmanArticle>(x => x.Date);
            string month = Reflector.GetPropertyName<SalesmanArticleGroupedByMonthAndDescription>(x => x.Month);

            string formatedDateSql = string.Format("month({{alias}}.[{0}]) as {1}", date, month);
            string formatedDateGroupBy = string.Format("month({{alias}}.[{0}])", date);

            return GetSession()
                // FROM
                .CreateCriteria(typeof (SalesmanArticle), salesmanArticle)
                // JOIN
                .CreateCriteria(article, article, JoinType.InnerJoin)
                .CreateCriteria(goodsGroup, goodsGroup, JoinType.InnerJoin)
                // SELECT
                .SetProjection(Projections.ProjectionList()
                                   .Add(Projections.Sum(amount), amount)
                                   .Add(
                                       Projections.Sum(Projections.SqlFunction(multiply, NHibernateUtil.Decimal,
                                                                               Projections.Property(amount),
                                                                               Projections.Property(cost))), cost)
                                   // GROUP BY
                                   .Add(Projections.GroupProperty(descriptionformat), description)
                                   .Add(Projections.SqlGroupProjection(formatedDateSql, formatedDateGroupBy,
                                                                       new[] {month}, new[] {NHibernateUtil.Int32})))
                .SetResultTransformer(Transformers.AliasToBean<SalesmanArticleGroupedByMonthAndDescription>())
                .List<SalesmanArticleGroupedByMonthAndDescription>();
        }

        #endregion
    }
}