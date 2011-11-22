using NHibernate;

namespace Infrastructure.NHibernate.SessionFactory
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory GetFactory();
    }
}