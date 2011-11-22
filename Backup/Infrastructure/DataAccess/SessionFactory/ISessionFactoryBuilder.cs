using NHibernate;

namespace Infrastructure.DataAccess.SessionFactory
{
    public interface ISessionFactoryBuilder
    {
        ISessionFactory GetFactory();
    }
}