using System;
using Core.Domain.Bases.Repositories.ConsumerProtection;

namespace Core.Domain.Factories.ConsumerProtection
{
    public class SalesmanRepositoryFactory
    {
        public static Func<ISalesmanRepository> GetDefault;
    }
}