// Type: MvcContrib.CommandProcessor.ReturnValue
// Assembly: MvcContrib.CommandProcessor, Version=2.0.96.0, Culture=neutral
// Assembly location: G:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\MVCContrib\MvcContrib.CommandProcessor.dll

using System;

namespace MvcContrib.CommandProcessor
{
    public class ReturnValue
    {
        public Type Type { get; set; }
        public object Value { get; set; }
        public ReturnValue SetValue<T>(T input);
    }
}
