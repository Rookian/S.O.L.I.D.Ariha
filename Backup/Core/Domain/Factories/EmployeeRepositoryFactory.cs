using System;
using Core.Domain.Bases.Repositories;

namespace Core.Domain.Factories
{
    public static class EmployeeRepositoryFactory
    {
        public static Func<IEmployeeRepository> GetDefault;
    }
}