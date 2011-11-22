using System.Collections.Generic;
using Core.Domain.Bases.Repositories;
using Core.Domain.Helper;
using Core.Domain.Model;
using Core.ReflectorExtension;
using NHibernate;

namespace Infrastructure.DataAccess.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public void Remove(Employee entity)
        {
            IEnumerable<Team> teams = entity.GetTeams();

            foreach (var team in teams)
            {
                entity.RemoveTeam(team);
            }
        }

        public List<Employee> GetAllView()
        {
            return (List<Employee>) GetSession()
                .CreateCriteria(typeof(Employee))
                .SetFetchMode(DomainModelHelper.GetAssociationEntityNameAsPlural(typeof(Team)), FetchMode.Eager)
                .List<Employee>();
        }
    }
}
