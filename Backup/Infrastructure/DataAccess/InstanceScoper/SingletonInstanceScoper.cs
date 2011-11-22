using System.Collections;
using System.Collections.Generic;

namespace Infrastructure.DataAccess.InstanceScoper
{
    public class SingletonInstanceScoper<T> : InstanceScoperBase<T>
    {
        private static readonly IDictionary Dictionary = new Dictionary<string, T>();

        protected override IDictionary GetDictionary()
        {
            return Dictionary;
        }
    }
}