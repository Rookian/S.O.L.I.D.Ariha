// Type: MvcContrib.CommandProcessor.Configuration.CommandEngineConfiguration
// Assembly: MvcContrib.CommandProcessor, Version=2.0.96.0, Culture=neutral
// Assembly location: C:\Users\Administrator\Desktop\bck\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\MVCContrib\MvcContrib.CommandProcessor.dll

using System;
using System.Collections.Generic;
using System.Reflection;

namespace MvcContrib.CommandProcessor.Configuration
{
    public class CommandEngineConfiguration
    {
        public IDictionary<Type, IMessageConfiguration> MessageConfigurations { get; }
        public IMessageConfiguration GetMessageConfiguration(Type messageType);
        public void Initialize(Assembly assembly);
    }
}
