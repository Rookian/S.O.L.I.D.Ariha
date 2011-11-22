using System;
using AutoMapper;
using CommandProcessor;

namespace Infrastructure.CommandProcessor
{
    public class MessageMapper : IMessageMapper
    {
        public object MapUiMessageToCommandMessage(object message, Type messageType, Type destinationType)
        {
            return Mapper.Map(message, message.GetType(), destinationType);
        }
    }
}