using System;
using Core.Domain.Bases.Repositories;

namespace Core.Domain.Factories
{
    public static class TeamRepositoryFactory
    {
        public static Func<ITeamRepository> GetDefault;
    }
}