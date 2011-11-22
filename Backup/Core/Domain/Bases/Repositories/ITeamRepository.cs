using Core.Domain.Model;

namespace Core.Domain.Bases.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        void Remove(Team entity);
    }
}
