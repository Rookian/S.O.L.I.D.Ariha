using System.Collections.Generic;
using Core.Common.Paging;
using Core.Domain.Bases;
using NHibernate;

namespace Infrastructure.NHibernate
{
    public static class QueryOverExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryOver<T, T> queryOver, int pageIndex, int pageSize) where T : Entity
        {
            var rowCountQuery = queryOver.ToRowCountQuery();
            IEnumerable<T> list = queryOver.Take(pageSize).Skip((pageIndex - 1) * pageSize).Future();
            int totalCount = rowCountQuery.FutureValue<int>().Value;

            return new PagedList<T>(list, pageIndex, pageSize, totalCount);
        }
    }
}