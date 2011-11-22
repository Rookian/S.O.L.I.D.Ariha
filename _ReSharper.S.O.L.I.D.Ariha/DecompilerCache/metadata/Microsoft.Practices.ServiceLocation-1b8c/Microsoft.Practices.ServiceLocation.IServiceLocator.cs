// Type: Microsoft.Practices.ServiceLocation.IServiceLocator
// Assembly: Microsoft.Practices.ServiceLocation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: G:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\Microsoft.Practices.ServiceLocation\Microsoft.Practices.ServiceLocation.dll

using System;
using System.Collections.Generic;

namespace Microsoft.Practices.ServiceLocation
{
    public interface IServiceLocator : IServiceProvider
    {
        object GetInstance(Type serviceType);
        object GetInstance(Type serviceType, string key);
        IEnumerable<object> GetAllInstances(Type serviceType);
        TService GetInstance<TService>();
        TService GetInstance<TService>(string key);
        IEnumerable<TService> GetAllInstances<TService>();
    }
}
