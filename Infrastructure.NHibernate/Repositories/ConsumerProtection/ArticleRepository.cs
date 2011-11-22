using Core.Domain.Bases.Repositories.ConsumerProtection;
using Core.Domain.Model.ConsumerProtection;

namespace Infrastructure.NHibernate.Repositories.ConsumerProtection
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
    }
}