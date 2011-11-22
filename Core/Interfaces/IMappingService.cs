using System;
using Core.Common.Paging;

namespace Core.Interfaces
{
    public interface IMappingService
    {
        PagedList<TDestinationElement> MapToViewModelPagedList<TSourceElement, TDestinationElement>(PagedList<TSourceElement> model);
        object Map<TSource, TDestination>(TSource model);
        object Map(object model, Type sourceType, Type destinationType);
    }
}