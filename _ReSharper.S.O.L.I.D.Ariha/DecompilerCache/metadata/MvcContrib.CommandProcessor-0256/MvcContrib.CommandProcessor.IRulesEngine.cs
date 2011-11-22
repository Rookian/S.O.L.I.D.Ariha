// Type: MvcContrib.CommandProcessor.IRulesEngine
// Assembly: MvcContrib.CommandProcessor, Version=2.0.96.0, Culture=neutral
// Assembly location: G:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\MVCContrib\MvcContrib.CommandProcessor.dll

using System;

namespace MvcContrib.CommandProcessor
{
    public interface IRulesEngine
    {
        ExecutionResult Process(object message, Type messageType);
        ExecutionResult Process(object message);
    }
}
