namespace Core.Services.BusinessRules
{

    public interface ICommandHandler
    {

    }

    public interface ICommandHandler<in TCommand> : ICommandHandler
    {
        object Execute(TCommand commandMessage);
    }
}