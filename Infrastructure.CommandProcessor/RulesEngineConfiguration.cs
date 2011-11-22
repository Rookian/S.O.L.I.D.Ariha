using System;
using System.Collections.Generic;
using System.Linq;
using CommandProcessor.Configuration;
using Core.Services.BusinessRules.CommandMessages;
using UserInterface.Models;

namespace Infrastructure.CommandProcessor
{
    public class RulesEngineConfiguration
    {
        private static readonly Type ConventionBasedViewModelInputType = typeof (TeamEmployeeInput);
        private static readonly Type ConventionBasedCommandMessageType = typeof (UpdateTeamEmployeeCommandMessage);

        public static void Configure(Type typeToLocatorConfigurationAssembly)
        {
            var rulesEngine = new global::CommandProcessor.RulesEngine();
            rulesEngine.Initialize(typeToLocatorConfigurationAssembly.Assembly, new MessageMapper());
            
            ConventionConfiguration(rulesEngine.Configuration);

            global::CommandProcessor.RulesEngine.MessageProcessorFactory = new MessageProcessorFactory();
        }

        private static void ConventionConfiguration(CommandEngineConfiguration configuration)
        {
            IEnumerable<Type> inputTypes = GetInputViewModelTypes(ConventionBasedViewModelInputType);

            IEnumerable<Type> messageTypes = GetCommandMessageTypes(ConventionBasedCommandMessageType);

            foreach (Type input in inputTypes)
            {
                if (!configuration.MessageConfigurations.ContainsKey(input))
                {
                    Type actualMessageType = messageTypes.FirstOrDefault(type => InputToCommandMessage(type, input));
                    if (actualMessageType != null)
                    {
                        configuration.MessageConfigurations.Add(input, new ConventionMessageConfiguration(actualMessageType));
                    }
                }
            }
        }

        private static IEnumerable<Type> GetCommandMessageTypes(Type messageType)
        {
            return messageType.Assembly.GetTypes().Where(t => TypeIsACommandMessage(t, messageType));
        }

        private static IEnumerable<Type> GetInputViewModelTypes(Type inputType)
        {
            return inputType.Assembly.GetTypes().Where(t => TypeIsAnInputModel(t, inputType)).ToArray();
        }

        private static bool TypeIsACommandMessage(Type t, Type messageType)
        {
            return !t.IsAbstract && t.Namespace.Equals(messageType.Namespace) && t.Name.EndsWith("CommandMessage");
        }

        private static bool TypeIsAnInputModel(Type t, Type inputType)
        {
            return t != null && !t.IsAbstract && t.Namespace != null && t.Namespace.Equals(inputType.Namespace) && t.Name != null && t.Name.EndsWith("Input");
        }

        private static bool InputToCommandMessage(Type message, Type input)
        {
            return message.Name.Replace("CommandMessage", "").Equals(input.Name.Replace("Input", ""));
        }
    }
}