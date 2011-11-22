using System.Linq;
using Core.Domain.Bases;
using Core.Domain.Bases.Repositories;
using Infrastructure.DataAccess.Session;
using NHibernate;
using NHibernate.Linq;

namespace Infrastructure.DataAccess.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly ISession _session;

        protected Repository()
        {
            _session = GetSession();
        }

        protected ISession GetSession()
        {
            return new SessionBuilder().GetSession();
        }

        public virtual void Delete(T entity)
        {
            _session.Delete(entity);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _session.Linq<T>();
        }

        public virtual T GetById(int id)
        {
            return _session.Get<T>(id);
        }

        public virtual void SaveOrUpdate(T enity)
        {
            _session.SaveOrUpdate(enity);
        }

        public void Merge(T entity)
        {
            _session.Merge(entity);
        }
    }
}


