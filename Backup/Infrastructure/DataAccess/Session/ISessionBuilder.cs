using NHibernate;

namespace Infrastructure.DataAccess.Session
{
    public interface ISessionBuilder
    {
        ISession GetSession();
    }
}