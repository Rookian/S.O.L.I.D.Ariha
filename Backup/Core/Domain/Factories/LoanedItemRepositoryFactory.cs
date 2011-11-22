using System;
using Core.Domain.Bases.Repositories;

namespace Core.Domain.Factories
{
    public static class LoanedItemRepositoryFactory
    {
        public static Func<ILoanedItemRepository> GetDefault;
    }
}