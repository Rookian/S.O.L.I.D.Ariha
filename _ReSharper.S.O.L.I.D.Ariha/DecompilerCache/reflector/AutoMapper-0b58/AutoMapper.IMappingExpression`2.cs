namespace AutoMapper
{
    using System;
    using System.Linq.Expressions;

    public interface IMappingExpression<TSource, TDestination>
    {
        IMappingExpression<TSource, TDestination> AfterMap<TMappingAction>()
            where TMappingAction : IMappingAction<TSource, TDestination>;

        IMappingExpression<TSource, TDestination> AfterMap(System.Action<TSource, TDestination> <
        TSource
    ,
        TDestination
    >
        afterFunction
    );

        IMappingExpression<TSource, TDestination> BeforeMap<TMappingAction>()
            where TMappingAction : IMappingAction<TSource, TDestination>;

        IMappingExpression<TSource, TDestination> BeforeMap(System.Action<TSource, TDestination> <
        TSource
    ,
        TDestination
    >
        beforeFunction
    );
        IMappingExpression<TSource, TDestination> ConstructUsing(System.Func<TSource, TDestination> <
        TSource
    ,
        TDestination
    >
        ctor
    );
        void ConvertUsing<TTypeConverter>() where TTypeConverter : ITypeConverter<TSource, TDestination>;
        void ConvertUsing(ITypeConverter<TSource, TDestination> converter);
        void ConvertUsing(System.Func<TSource, TDestination> <
        TSource
    ,
        TDestination
    >
        mappingFunction
    );
        void ForAllMembers(Action<IMemberConfigurationExpression<TSource>> memberOptions);
        IMappingExpression<TSource, TDestination> ForMember(Expression<System.Func<TDestination, Object>  <
        TDestination
    ,
        object
    >>
        destinationMember
    ,
        Action<IMemberConfigurationExpression<TSource>>
        memberOptions
    );

        IMappingExpression<TSource, TDestination> ForMember(string name,
                                                            Action<IMemberConfigurationExpression<TSource>>
                                                                memberOptions);

        IMappingExpression<TSource, TDestination> Include<TOtherSource, TOtherDestination>()
            where TOtherSource : TSource where TOtherDestination : TDestination;

        IMappingExpression<TSource, TDestination> WithProfile(string profileName);
    }
}