// Type: AutoMapper.IMappingExpression`2
// Assembly: AutoMapper, Version=1.1.0.188, Culture=neutral, PublicKeyToken=be96cd2c38ef1005
// Assembly location: G:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\AutoMapper\AutoMapper.dll

using System;
using System.Linq.Expressions;

namespace AutoMapper
{
    public interface IMappingExpression<TSource, TDestination>
    {
        IMappingExpression<TSource, TDestination> ForMember(Expression<Func<TDestination, object>> destinationMember,
                                                            Action<IMemberConfigurationExpression<TSource>>
                                                                memberOptions);

        IMappingExpression<TSource, TDestination> ForMember(string name,
                                                            Action<IMemberConfigurationExpression<TSource>>
                                                                memberOptions);

        void ForAllMembers(Action<IMemberConfigurationExpression<TSource>> memberOptions);

        IMappingExpression<TSource, TDestination> Include<TOtherSource, TOtherDestination>()
            where TOtherSource : TSource where TOtherDestination : TDestination;

        IMappingExpression<TSource, TDestination> WithProfile(string profileName);
        void ConvertUsing(Func<TSource, TDestination> mappingFunction);
        void ConvertUsing(ITypeConverter<TSource, TDestination> converter);
        void ConvertUsing<TTypeConverter>() where TTypeConverter : ITypeConverter<TSource, TDestination>;
        IMappingExpression<TSource, TDestination> BeforeMap(Action<TSource, TDestination> beforeFunction);

        IMappingExpression<TSource, TDestination> BeforeMap<TMappingAction>()
            where TMappingAction : IMappingAction<TSource, TDestination>;

        IMappingExpression<TSource, TDestination> AfterMap(Action<TSource, TDestination> afterFunction);

        IMappingExpression<TSource, TDestination> AfterMap<TMappingAction>()
            where TMappingAction : IMappingAction<TSource, TDestination>;

        IMappingExpression<TSource, TDestination> ConstructUsing(Func<TSource, TDestination> ctor);
    }
}
