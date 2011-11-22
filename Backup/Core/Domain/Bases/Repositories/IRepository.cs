using System.Linq;

namespace Core.Domain.Bases.Repositories
{

    public interface IRepository
    {

    }

    public interface IRepository<T> : IRepository where T : Entity
    {
        void Delete(T entity);
        IQueryable<T> GetAll();
        T GetById(int id);
        void SaveOrUpdate(T enity);
        void Merge(T entity);
    }
}
