using Core.Domain.Bases.Repositories;
using Core.Domain.Model;

namespace Infrastructure.NHibernate.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository { }
}