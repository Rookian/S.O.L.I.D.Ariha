using System;

namespace Infrastructure.NHibernate.InstanceScoper
{
    public interface IInstanceScoper<T>
    {
        T GetScopedInstance(string key, Func<T> builder);
        void ClearScopedInstance(string key);
    }
}