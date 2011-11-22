using System.Collections.Generic;
using Core.Domain.Model.ConsumerProtection;

namespace Core.Domain.Bases.Repositories.ConsumerProtection
{
    public interface ISalesmanArticleRepository : IRepository<SalesmanArticle>
    {
        IList<SalesmanArticleGroupedByMonthAndDescription> GetSalesmanArticleGroupedByMonthAndDescription();
    }
}