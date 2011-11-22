// Type: AutoMapper.Mapper
// Assembly: AutoMapper, Version=1.0.0.155, Culture=neutral, PublicKeyToken=be96cd2c38ef1005
// Assembly location: E:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\AutoMapper\AutoMapper.dll

using System;

namespace AutoMapper
{
    public static class Mapper
    {
        public static bool AllowNullDestinationValues { get; set; }
        public static IMappingEngine Engine { get; }
        public static TDestination Map<TSource, TDestination>(TSource source);
        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
        public static object Map(object source, Type sourceType, Type destinationType);
        public static object Map(object source, object destination, Type sourceType, Type destinationType);
        public static TDestination DynamicMap<TSource, TDestination>(TSource source);
        public static void DynamicMap<TSource, TDestination>(TSource source, TDestination destination);
        public static TDestination DynamicMap<TDestination>(object source);
        public static object DynamicMap(object source, Type sourceType, Type destinationType);
        public static void DynamicMap(object source, object destination, Type sourceType, Type destinationType);
        public static void Initialize(Action<IConfiguration> action);

        public static IFormatterCtorExpression<TValueFormatter> AddFormatter<TValueFormatter>()
            where TValueFormatter : IValueFormatter;

        public static IFormatterCtorExpression AddFormatter(Type valueFormatterType);
        public static void AddFormatter(IValueFormatter formatter);
        public static void AddFormatExpression(Func<ResolutionContext, string> formatExpression);
        public static void SkipFormatter<TValueFormatter>() where TValueFormatter : IValueFormatter;
        public static IFormatterExpression ForSourceType<TSource>();
        public static IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>();
        public static IMappingExpression CreateMap(Type sourceType, Type destinationType);
        public static IProfileExpression CreateProfile(string profileName);
        public static void CreateProfile(string profileName, Action<IProfileExpression> profileConfiguration);
        public static void AddProfile(Profile profile);
        public static void AddProfile<TProfile>() where TProfile : new(), Profile;
        public static TypeMap FindTypeMapFor(Type sourceType, Type destinationType);
        public static TypeMap FindTypeMapFor<TSource, TDestination>();
        public static TypeMap[] GetAllTypeMaps();
        public static void AssertConfigurationIsValid();
        public static void AssertConfigurationIsValid(string profileName);
        public static void Reset();
    }
}
