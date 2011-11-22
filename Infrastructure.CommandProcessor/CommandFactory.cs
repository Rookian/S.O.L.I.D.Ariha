using System;
using System.Collections.Generic;
using System.Linq;
using CommandProcessor.Commands;
using CommandProcessor.Interfaces;
using Core.Services.BusinessRules;

namespace Infrastructure.CommandProcessor
{
    public class CommandFactory : ICommandFactory
    {

        public static Func<Type, ICommandHandler[]> CommandLocator = t => { throw new NotImplementedException(); };

        public IEnumerable<ICommandMessageHandler> GetCommands(ICommandConfiguration definition)
        {
            Type concreteCommandType = typeof(ICommandHandler<>).MakeGenericType(definition.CommandMessageType);
            var commands = CommandLocator(concreteCommandType);
            return commands.Select(command => new CommandMessageHandlerProxy(command)).ToArray();
        }
    }
}