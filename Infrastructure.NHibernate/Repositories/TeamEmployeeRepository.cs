using Core.Common.Paging;
using Core.Domain.Bases.Repositories;
using Core.Domain.Model;

namespace Infrastructure.NHibernate.Repositories
{
    public class TeamEmployeeRepository : Repository<TeamEmployee>, ITeamEmployeeRepository
    {
        public PagedList<TeamEmployee> GetPagedTeamEmployees(int pageIndex, int pageSize)
        {
            return GetSession().QueryOver<TeamEmployee>()
                .Fetch(x => x.Employee).Eager
                .Fetch(x => x.Team).Eager
                .ToPagedList(pageIndex, pageSize);
        }
    }
}