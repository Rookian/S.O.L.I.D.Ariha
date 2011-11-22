using System;

namespace Infrastructure.DataAccess.InstanceScoper
{
    public interface IInstanceScoper<T>
    {
        T GetScopedInstance(string key, Func<T> builder);
        void ClearScopedInstance(string key);
    }
}