namespace Core.Common.Paging
{
    public class PagerInfo
    {
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage { get { return (PageIndex > 1); } }
        public bool HasNextPage { get { return (PageIndex * PageSize) <= TotalCount; } }
    }
}