using System;
using Core.Domain.Bases.Repositories.ConsumerProtection;

namespace Core.Domain.Factories.ConsumerProtection
{
    public class SalesmanArticleRepositoryFactory
    {
        public static Func<ISalesmanArticleRepository> GetDefault;
    }
}