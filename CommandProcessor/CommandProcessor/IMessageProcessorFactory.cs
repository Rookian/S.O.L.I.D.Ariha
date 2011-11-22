using CommandProcessor.Configuration;
using CommandProcessor.Interfaces;

namespace CommandProcessor
{
	public interface IMessageProcessorFactory
	{
		IMessageProcessor Create(IUnitOfWork unitOfWork, IMessageMapper mappingEngine, CommandEngineConfiguration configuration);
	}
}