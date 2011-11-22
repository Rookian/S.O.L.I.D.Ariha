using System;
using Core.Domain.Bases.Repositories.ConsumerProtection;

namespace Core.Domain.Factories.ConsumerProtection
{
    public class GoodsGroupRepositoryFactory
    {
        public static Func<IGoodsGroupRepository> GetDefault;
    }
}