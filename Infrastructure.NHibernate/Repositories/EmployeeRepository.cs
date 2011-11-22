using Core.Domain.Bases.Repositories;
using Core.Domain.Model;

namespace Infrastructure.NHibernate.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {

    }
}