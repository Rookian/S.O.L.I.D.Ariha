using System;
using AutoMapper;

namespace Infrastructure.Automapper.ConfigurationProfiles
{
    public class StringToDateTimeConverter: ITypeConverter<string, DateTime>
    {
        public DateTime Convert(ResolutionContext context)
        {
            object obDateTime = context.SourceValue;
            DateTime dateTime;

            if (obDateTime == null)
            {
                return default(DateTime);
            }
            
            if (DateTime.TryParse(obDateTime.ToString(), out dateTime))
            {
                return dateTime;
            }

            return default(DateTime);
        }
    }
}