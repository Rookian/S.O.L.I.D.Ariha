using System;
using System.Collections.Generic;
using CommandProcessor.Validation;

namespace CommandProcessor.Interfaces
{
	public interface ICommandConfiguration
	{
		Type CommandMessageType { get; }
		Delegate Condition { get; }
		void Initialize(object commandMessage, ExecutionResult result);
		IEnumerable<ValidationRuleInstance> GetValidationRules();
	}
}