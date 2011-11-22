using AutoMapper;
using Core.Common;

namespace Infrastructure.Automapper.Formatter
{
    public class MoneyFormatter : IValueFormatter
    {
        public string FormatValue(ResolutionContext context)
        {
            if (context.SourceValue == null)
                return null;

            if (!(context.SourceValue is decimal))
                return context.SourceValue.ToNullSafeString();

            return ((decimal)context.SourceValue).ToString("c");
        }
    }
}