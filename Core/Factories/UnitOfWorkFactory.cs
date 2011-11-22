using System;
using Core.Interfaces;

namespace Core.Factories
{
    public  class UnitOfWorkFactory : AbstractFactoryBase<IUnitOfWork>
    {
        public static Func<IUnitOfWork> GetDefault = GetDefaultUnconfiguredState;
    }
}