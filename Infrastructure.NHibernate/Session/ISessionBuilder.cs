using NHibernate;

namespace Infrastructure.NHibernate.Session
{
    public interface ISessionBuilder
    {
        ISession GetSession();
    }
}