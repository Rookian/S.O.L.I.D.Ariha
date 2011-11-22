using Infrastructure.NHibernate.InstanceScoper;
using Infrastructure.NHibernate.SessionFactory;
using NHibernate;

namespace Infrastructure.NHibernate.Session
{
    public class SessionBuilder : ISessionBuilder
    {
        private const string NHibernateSessionKey = "NHibernate.ISession";
        private readonly ISessionFactoryBuilder _builder;
        private readonly HybridInstanceScoper<ISession> _hybridInstanceScoper;

        public SessionBuilder()
        {
            _hybridInstanceScoper = new HybridInstanceScoper<ISession>();
            _builder = new SessionFactoryBuilder();
        }

        public ISession GetSession()
        {
            ISession instance = GetScopedInstance();
            if (!instance.IsOpen)
            {
                _hybridInstanceScoper.ClearScopedInstance(NHibernateSessionKey);
                return GetScopedInstance();
            }
            return instance;
        }

        private ISession GetScopedInstance()
        {
            return _hybridInstanceScoper.GetScopedInstance(NHibernateSessionKey, BuildSession);
        }

        private ISession BuildSession()
        {
            ISessionFactory factory = _builder.GetFactory();
            ISession session = factory.OpenSession();
              
            session.FlushMode = FlushMode.Commit;
            return session;
        }
    }
}