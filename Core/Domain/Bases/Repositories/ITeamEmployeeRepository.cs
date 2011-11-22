using Core.Common.Paging;
using Core.Domain.Model;

namespace Core.Domain.Bases.Repositories
{
    public interface ITeamEmployeeRepository : IRepository<TeamEmployee>
    {
        PagedList<TeamEmployee> GetPagedTeamEmployees(int pageIndex, int pageSize);
    }
}