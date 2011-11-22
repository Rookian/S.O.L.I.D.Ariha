using System;
using System.Collections.Generic;
using CommandProcessor.Configuration;
using CommandProcessor.Interfaces;

namespace Infrastructure.CommandProcessor
{
    public class ConventionMessageConfiguration : IMessageConfiguration
    {
        private readonly Type _messageType;

        public ConventionMessageConfiguration(Type messageType)
        {
            _messageType = messageType;
        }

        public IEnumerable<ICommandConfiguration> GetApplicableCommands(object message)
        {
            return new[] { new CommandDefinition(_messageType) };
        }
    }
}