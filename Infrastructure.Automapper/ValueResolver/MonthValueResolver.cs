using System.Globalization;
using Core.Domain.Model.ConsumerProtection;
using AutoMapper;


namespace Infrastructure.Automapper.ValueResolver
{
    public class MonthValueResolver : ValueResolver<SalesmanArticleGroupedByMonthAndDescription, string >
    {
        protected override string ResolveCore(SalesmanArticleGroupedByMonthAndDescription source)
        {
            if (source == null || source.Month == 0)
                return string.Empty;

            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(source.Month);
        }
    }
}