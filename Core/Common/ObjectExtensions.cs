using System;

namespace Core.Common
{
    public static class ObjectExtensions
    {
        public static string ToNullSafeString(this object value)
        {
            return value == null ? String.Empty : value.ToString();
        }
    }
}