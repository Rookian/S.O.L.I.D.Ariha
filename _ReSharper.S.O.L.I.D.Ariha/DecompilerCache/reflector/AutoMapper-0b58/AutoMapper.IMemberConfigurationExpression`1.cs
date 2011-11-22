namespace AutoMapper
{
    using System;

    public interface IMemberConfigurationExpression<TSource>
    {
        IFormatterCtorExpression<TValueFormatter> AddFormatter<TValueFormatter>()
            where TValueFormatter : IValueFormatter;

        void AddFormatter(IValueFormatter formatter);
        IFormatterCtorExpression AddFormatter(Type valueFormatterType);
        void Ignore();
        void MapFrom<TMember>(System.Func<TSource, TMember> <
        TSource
    ,
        TMember
    >
        sourceMember
    );
        void NullSubstitute(object nullSubstitute);

        IResolverConfigurationExpression<TSource, TValueResolver> ResolveUsing<TValueResolver>()
            where TValueResolver : IValueResolver;

        IResolutionExpression<TSource> ResolveUsing(IValueResolver valueResolver);
        IResolverConfigurationExpression<TSource> ResolveUsing(Type valueResolverType);
        void SetMappingOrder(int mappingOrder);
        void SkipFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter;
        void UseDestinationValue();
        void UseValue<TValue>(TValue value);
    }
}