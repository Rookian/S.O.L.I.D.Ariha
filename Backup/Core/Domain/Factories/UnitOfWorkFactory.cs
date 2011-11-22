using System;
using Core.Domain.Bases;

namespace Core.Domain.Factories
{
    public static class UnitOfWorkFactory
    {
        public static Func<IUnitOfWork> GetDefault;
    }
}