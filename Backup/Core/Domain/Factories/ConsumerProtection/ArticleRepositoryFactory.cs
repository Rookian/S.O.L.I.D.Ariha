using System;
using Core.Domain.Bases.Repositories.ConsumerProtection;

namespace Core.Domain.Factories.ConsumerProtection
{
    public class ArticleRepositoryFactory
    {
        public static Func<IArticleRepository> GetDefault;
    }
}