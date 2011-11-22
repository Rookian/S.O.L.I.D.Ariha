// Type: NHibernate.Connection.ConnectionProviderFactory
// Assembly: NHibernate, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4
// Assembly location: H:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\Nhibernate\NHibernate.dll

using System.Collections.Generic;

namespace NHibernate.Connection
{
    public sealed class ConnectionProviderFactory
    {
        public static IConnectionProvider NewConnectionProvider(IDictionary<string, string> settings);
    }
}
