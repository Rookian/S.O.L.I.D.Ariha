// Type: MvcContrib.CommandProcessor.Configuration.MessageDefinition`1
// Assembly: MvcContrib.CommandProcessor, Version=2.0.96.0, Culture=neutral
// Assembly location: G:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\MVCContrib\MvcContrib.CommandProcessor.dll

using MvcContrib.CommandProcessor.Interfaces;
using System.Collections.Generic;

namespace MvcContrib.CommandProcessor.Configuration
{
    public abstract class MessageDefinition<TMessage> : IMessageConfiguration
    {
        public const int INDEX = 2147483647;
        protected MessageDefinition();
        protected IConditionExpression<TMessage> Conditions { get; }

        #region IMessageConfiguration Members

        IEnumerable<ICommandConfiguration> IMessageConfiguration.GetApplicableCommands(object message);

        #endregion

        public ICommandConfigurationExpression<TMessage, TCommand> Execute<TCommand>();
    }
}
