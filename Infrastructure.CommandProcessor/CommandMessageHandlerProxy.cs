using System;
using System.Reflection;
using CommandProcessor;
using CommandProcessor.Commands;
using Core.Services.BusinessRules;

namespace Infrastructure.CommandProcessor
{
    public class CommandMessageHandlerProxy : ICommandMessageHandler
    {
        public static Func<object, object, object> ExecuteHandler = (message, commandHandler) =>
                                            {
                                                MethodInfo method = commandHandler.GetType().GetMethod("Execute");
                                                object value = method.Invoke(commandHandler, new[] { message });
                                                return value;
                                            };

        private readonly ICommandHandler _command;

        public CommandMessageHandlerProxy(ICommandHandler command)
        {
            _command = command;
        }

        public ReturnValue Execute(object commandMessage)
        {
            object value = ExecuteHandler(commandMessage, _command);
            return new ReturnValue { Type = value.GetType(), Value = value };
        }
    }
}