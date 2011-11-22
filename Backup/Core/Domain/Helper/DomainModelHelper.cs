using System;

namespace Core.Domain.Helper
{
    public class DomainModelHelper
    {
        public static string GetAssociationEntityNameAsPlural(Type type)
        {
            return String.Format("{0}s", type.Name);
        }
    }
}