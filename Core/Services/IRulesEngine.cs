namespace Core.Services
{
    public interface IRulesEngine
    {
        ICanSucceed Process(object message);
    }
}