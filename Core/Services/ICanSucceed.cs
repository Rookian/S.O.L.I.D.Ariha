using System.Collections.Generic;

namespace Core.Services
{
    public interface ICanSucceed
    {
        bool Successful { get; }
        IEnumerable<ErrorMessage> Errors { get; }
        T Result<T>();
    }
}