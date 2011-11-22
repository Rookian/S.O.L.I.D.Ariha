// Type: AutoMapper.IMemberConfigurationExpression`1
// Assembly: AutoMapper, Version=1.1.0.188, Culture=neutral, PublicKeyToken=be96cd2c38ef1005
// Assembly location: G:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\AutoMapper\AutoMapper.dll

using System;

namespace AutoMapper
{
    public interface IMemberConfigurationExpression<TSource>
    {
        void SkipFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter;

        IFormatterCtorExpression<TValueFormatter> AddFormatter<TValueFormatter>()
            where TValueFormatter : IValueFormatter;

        IFormatterCtorExpression AddFormatter(Type valueFormatterType);
        void AddFormatter(IValueFormatter formatter);
        void NullSubstitute(object nullSubstitute);

        IResolverConfigurationExpression<TSource, TValueResolver> ResolveUsing<TValueResolver>()
            where TValueResolver : IValueResolver;

        IResolverConfigurationExpression<TSource> ResolveUsing(Type valueResolverType);
        IResolutionExpression<TSource> ResolveUsing(IValueResolver valueResolver);
        void MapFrom<TMember>(Func<TSource, TMember> sourceMember);
        void Ignore();
        void SetMappingOrder(int mappingOrder);
        void UseDestinationValue();
        void UseValue<TValue>(TValue value);
        void UseValue(object value);
        void Condition(Func<TSource, bool> condition);
        void Condition(Func<ResolutionContext, bool> condition);
    }
}
