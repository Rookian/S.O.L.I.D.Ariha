using Core.Common.Paging;
using Core.Interfaces;

namespace UserInterface.ActionResults
{
    public class AutoMappedHybridViewResult<TSourceElement, TDestinationElement> : BaseHybridViewResult
    {
        public AutoMappedHybridViewResult(PagedList<TSourceElement> pagedList)
        {
            ViewModel = CreateDependency<IMappingService>().MapToViewModelPagedList<TSourceElement, TDestinationElement>(pagedList);
        }

        public AutoMappedHybridViewResult(PagedList<TSourceElement> pagedList, string viewNameForAjaxRequest)
        {
            ViewNameForAjaxRequest = viewNameForAjaxRequest;
            ViewModel = CreateDependency<IMappingService>().MapToViewModelPagedList<TSourceElement, TDestinationElement>(pagedList);
        }

        public AutoMappedHybridViewResult(TSourceElement model)
        {
            ViewModel = CreateDependency<IMappingService>().Map<TSourceElement, TDestinationElement>(model);
        }

        public AutoMappedHybridViewResult(TSourceElement model, string viewNameForAjaxRequest)
        {
            ViewNameForAjaxRequest = viewNameForAjaxRequest;
            ViewModel = CreateDependency<IMappingService>().Map<TSourceElement, TDestinationElement>(model);
        }
    }
}