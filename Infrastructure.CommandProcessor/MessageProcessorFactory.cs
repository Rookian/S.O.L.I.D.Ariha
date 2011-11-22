using CommandProcessor;
using CommandProcessor.Commands;
using CommandProcessor.Configuration;
using CommandProcessor.Interfaces;
using CommandProcessor.Validation;

namespace Infrastructure.CommandProcessor
{
    public class MessageProcessorFactory : IMessageProcessorFactory
    {
        public IMessageProcessor Create(IUnitOfWork unitOfWork, IMessageMapper mappingEngine, CommandEngineConfiguration configuration)
        {
            return new MessageProcessor(mappingEngine, CreateInvoker(new CommandFactory()), unitOfWork, configuration);
        }

        private static CommandInvoker CreateInvoker(ICommandFactory commandFactory)
        {
            return new CommandInvoker(new ValidationEngine(new ValidationRuleFactory()), commandFactory);
        }
    }
}