using Core.Domain.Model;
using System.Collections.Generic;

namespace Core.Domain.Bases.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        void Remove(Employee entity);
        List<Employee> GetAllView();
    }
}
