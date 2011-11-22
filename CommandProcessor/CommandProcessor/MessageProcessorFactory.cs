using CommandProcessor.Commands;
using CommandProcessor.Configuration;
using CommandProcessor.Interfaces;
using CommandProcessor.Validation;

namespace CommandProcessor
{
	public class MessageProcessorFactory : IMessageProcessorFactory
	{
		public IMessageProcessor Create(IUnitOfWork unitOfWork, IMessageMapper mappingEngine,
		                                CommandEngineConfiguration configuration)
		{
			return new MessageProcessor(mappingEngine,
			                            new CommandInvoker(new ValidationEngine(new ValidationRuleFactory()),
			                                               new CommandFactory()), unitOfWork, configuration);
		}
	}
}