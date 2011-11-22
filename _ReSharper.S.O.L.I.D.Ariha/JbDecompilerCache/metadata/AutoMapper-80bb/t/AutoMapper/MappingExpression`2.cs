// Type: AutoMapper.MappingExpression`2
// Assembly: AutoMapper, Version=1.1.0.188, Culture=neutral, PublicKeyToken=be96cd2c38ef1005
// Assembly location: G:\S.O.L.I.D.Ariha\Libraries\AutoMapper\AutoMapper.dll

using AutoMapper.Internal;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper
{
  internal class MappingExpression<TSource, TDestination> : IMappingExpression<TSource, TDestination>, IMemberConfigurationExpression<TSource>, IFormatterCtorConfigurator
  {
    private readonly TypeMap _typeMap;
    private readonly Func<Type, object> _serviceCtor;
    private PropertyMap _propertyMap;

    public MappingExpression(TypeMap typeMap, Func<Type, object> serviceCtor)
    {
      this._typeMap = typeMap;
      this._serviceCtor = serviceCtor;
    }

    public IMappingExpression<TSource, TDestination> ForMember(Expression<Func<TDestination, object>> destinationMember, Action<IMemberConfigurationExpression<TSource>> memberOptions)
    {
      this.ForDestinationMember(ReflectionHelper.ToMemberAccessor(ReflectionHelper.FindProperty((LambdaExpression) destinationMember)), memberOptions);
      return (IMappingExpression<TSource, TDestination>) new MappingExpression<TSource, TDestination>(this._typeMap, this._serviceCtor);
    }

    public IMappingExpression<TSource, TDestination> ForMember(string name, Action<IMemberConfigurationExpression<TSource>> memberOptions)
    {
      this.ForDestinationMember((IMemberAccessor) new PropertyAccessor(typeof (TDestination).GetProperty(name)), memberOptions);
      return (IMappingExpression<TSource, TDestination>) new MappingExpression<TSource, TDestination>(this._typeMap, this._serviceCtor);
    }

    public void ForAllMembers(Action<IMemberConfigurationExpression<TSource>> memberOptions)
    {
      EnumerableExtensions.Each<MemberInfo>(new TypeInfo(this._typeMap.DestinationType).GetPublicWriteAccessors(), (Action<MemberInfo>) (acc => this.ForDestinationMember(ReflectionHelper.ToMemberAccessor(acc), memberOptions)));
    }

    public IMappingExpression<TSource, TDestination> Include<TOtherSource, TOtherDestination>() where TOtherSource : TSource where TOtherDestination : TDestination
    {
      this._typeMap.IncludeDerivedTypes(typeof (TOtherSource), typeof (TOtherDestination));
      return (IMappingExpression<TSource, TDestination>) this;
    }

    public IMappingExpression<TSource, TDestination> WithProfile(string profileName)
    {
      this._typeMap.Profile = profileName;
      return (IMappingExpression<TSource, TDestination>) this;
    }

    public void SkipFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter
    {
      this._propertyMap.AddFormatterToSkip<TValueFormatter>();
    }

    public IFormatterCtorExpression<TValueFormatter> AddFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter
    {
      this.AddFormatter((IValueFormatter) new DeferredInstantiatedFormatter((Func<IValueFormatter>) (() => (IValueFormatter) this._serviceCtor(typeof (TValueFormatter)))));
      return (IFormatterCtorExpression<TValueFormatter>) new FormatterCtorExpression<TValueFormatter>((IFormatterCtorConfigurator) this);
    }

    public IFormatterCtorExpression AddFormatter(Type valueFormatterType)
    {
      this.AddFormatter((IValueFormatter) new DeferredInstantiatedFormatter((Func<IValueFormatter>) (() => (IValueFormatter) this._serviceCtor(valueFormatterType))));
      return (IFormatterCtorExpression) new FormatterCtorExpression(valueFormatterType, (IFormatterCtorConfigurator) this);
    }

    public void AddFormatter(IValueFormatter formatter)
    {
      this._propertyMap.AddFormatter(formatter);
    }

    public void NullSubstitute(object nullSubstitute)
    {
      this._propertyMap.SetNullSubstitute(nullSubstitute);
    }

    public IResolverConfigurationExpression<TSource, TValueResolver> ResolveUsing<TValueResolver>() where TValueResolver : IValueResolver
    {
      this.ResolveUsing((IValueResolver) new DeferredInstantiatedResolver((Func<IValueResolver>) (() => (IValueResolver) this._serviceCtor(typeof (TValueResolver)))));
      return (IResolverConfigurationExpression<TSource, TValueResolver>) new ResolutionExpression<TSource, TValueResolver>(this._propertyMap);
    }

    public IResolverConfigurationExpression<TSource> ResolveUsing(Type valueResolverType)
    {
      this.ResolveUsing((IValueResolver) new DeferredInstantiatedResolver((Func<IValueResolver>) (() => (IValueResolver) this._serviceCtor(valueResolverType))));
      return (IResolverConfigurationExpression<TSource>) new ResolutionExpression<TSource>(this._propertyMap);
    }

    public IResolutionExpression<TSource> ResolveUsing(IValueResolver valueResolver)
    {
      this._propertyMap.AssignCustomValueResolver(valueResolver);
      return (IResolutionExpression<TSource>) new ResolutionExpression<TSource>(this._propertyMap);
    }

    public void MapFrom<TMember>(Func<TSource, TMember> sourceMember)
    {
      this._propertyMap.AssignCustomValueResolver((IValueResolver) new DelegateBasedResolver<TSource, TMember>(sourceMember));
    }

    public void UseValue<TValue>(TValue value)
    {
      this.MapFrom<TValue>((Func<TSource, TValue>) (src => value));
    }

    public void UseValue(object value)
    {
      this._propertyMap.AssignCustomValueResolver((IValueResolver) new DelegateBasedResolver<TSource>((Func<TSource, object>) (src => value)));
    }

    public void Condition(Func<TSource, bool> condition)
    {
      this.Condition((Func<ResolutionContext, bool>) (context => condition((TSource) context.Parent.SourceValue)));
    }

    public void Condition(Func<ResolutionContext, bool> condition)
    {
      this._propertyMap.ApplyCondition(condition);
    }

    public void Ignore()
    {
      this._propertyMap.Ignore();
    }

    public void UseDestinationValue()
    {
      this._propertyMap.UseDestinationValue = true;
    }

    public void SetMappingOrder(int mappingOrder)
    {
      this._propertyMap.SetMappingOrder(mappingOrder);
    }

    public void ConstructFormatterBy(Type formatterType, Func<IValueFormatter> instantiator)
    {
      this._propertyMap.RemoveLastFormatter();
      this._propertyMap.AddFormatter((IValueFormatter) new DeferredInstantiatedFormatter(instantiator));
    }

    public void ConvertUsing(Func<TSource, TDestination> mappingFunction)
    {
      this._typeMap.UseCustomMapper((Func<ResolutionContext, object>) (source => (object) mappingFunction((TSource) source.SourceValue)));
    }

    public void ConvertUsing(Func<ResolutionContext, TDestination> mappingFunction)
    {
      this._typeMap.UseCustomMapper((Func<ResolutionContext, object>) (context => (object) mappingFunction(context)));
    }

    public void ConvertUsing(Func<ResolutionContext, TSource, TDestination> mappingFunction)
    {
      this._typeMap.UseCustomMapper((Func<ResolutionContext, object>) (source => (object) mappingFunction(source, (TSource) source.SourceValue)));
    }

    public void ConvertUsing(ITypeConverter<TSource, TDestination> converter)
    {
      this.ConvertUsing(new Func<ResolutionContext, TDestination>(converter.Convert));
    }

    public void ConvertUsing<TTypeConverter>() where TTypeConverter : ITypeConverter<TSource, TDestination>
    {
      this.ConvertUsing(new Func<ResolutionContext, TDestination>(new DeferredInstantiatedConverter<TSource, TDestination>((Func<ITypeConverter<TSource, TDestination>>) (() => (ITypeConverter<TSource, TDestination>) this._serviceCtor(typeof (TTypeConverter)))).Convert));
    }

    public IMappingExpression<TSource, TDestination> BeforeMap(Action<TSource, TDestination> beforeFunction)
    {
      this._typeMap.AddBeforeMapAction((Action<object, object>) ((src, dest) => beforeFunction((TSource) src, (TDestination) dest)));
      return (IMappingExpression<TSource, TDestination>) this;
    }

    public IMappingExpression<TSource, TDestination> BeforeMap<TMappingAction>() where TMappingAction : IMappingAction<TSource, TDestination>
    {
      return this.BeforeMap((Action<TSource, TDestination>) ((src, dest) => ((TMappingAction) this._serviceCtor(typeof (TMappingAction))).Process(src, dest)));
    }

    public IMappingExpression<TSource, TDestination> AfterMap(Action<TSource, TDestination> afterFunction)
    {
      this._typeMap.AddAfterMapAction((Action<object, object>) ((src, dest) => afterFunction((TSource) src, (TDestination) dest)));
      return (IMappingExpression<TSource, TDestination>) this;
    }

    public IMappingExpression<TSource, TDestination> AfterMap<TMappingAction>() where TMappingAction : IMappingAction<TSource, TDestination>
    {
      return this.AfterMap((Action<TSource, TDestination>) ((src, dest) => ((TMappingAction) this._serviceCtor(typeof (TMappingAction))).Process(src, dest)));
    }

    public IMappingExpression<TSource, TDestination> ConstructUsing(Func<TSource, TDestination> ctor)
    {
      this._typeMap.DestinationCtor = (Func<object, object>) (src => (object) ctor((TSource) src));
      return (IMappingExpression<TSource, TDestination>) this;
    }

    private void ForDestinationMember(IMemberAccessor destinationProperty, Action<IMemberConfigurationExpression<TSource>> memberOptions)
    {
      this._propertyMap = this._typeMap.FindOrCreatePropertyMapFor(destinationProperty);
      memberOptions((IMemberConfigurationExpression<TSource>) this);
    }
  }
}
