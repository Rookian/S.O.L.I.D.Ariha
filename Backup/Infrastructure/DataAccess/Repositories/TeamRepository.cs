using Core.Domain.Bases.Repositories;
using Core.Domain.Model;
using NHibernate;

namespace Infrastructure.DataAccess.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public void Remove(Team entity)
        {
            Employee[] employees = entity.GetEmployees();

            foreach (var employee in employees)
            {
                entity.RemoveEmployee(employee);
            }
        }

        public override void Delete(Team entity)
        {
            ISession session = GetSession();

            Remove(entity);

            
            session.Delete(entity);
        }
    }
}
