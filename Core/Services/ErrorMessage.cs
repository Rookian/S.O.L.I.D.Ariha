using System.Linq.Expressions;

namespace Core.Services
{
    public class ErrorMessage
    {
        public LambdaExpression InvalidProperty { get; set; }
        public string Message { get; set; }
    }
}