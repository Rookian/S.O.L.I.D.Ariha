// Type: NHibernate.ICriteria
// Assembly: NHibernate, Version=2.1.2.4000, Culture=neutral, PublicKeyToken=aa95f207798dfdb4
// Assembly location: G:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\Nhibernate\NHibernate.dll

using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NHibernate
{
    public interface ICriteria : ICloneable
    {
        string Alias { get; }
        ICriteria SetProjection(params IProjection[] projection);
        ICriteria Add(ICriterion expression);
        ICriteria AddOrder(Order order);
        ICriteria SetFetchMode(string associationPath, FetchMode mode);
        ICriteria SetLockMode(LockMode lockMode);
        ICriteria SetLockMode(string alias, LockMode lockMode);
        ICriteria CreateAlias(string associationPath, string alias);
        ICriteria CreateAlias(string associationPath, string alias, JoinType joinType);
        ICriteria CreateCriteria(string associationPath);
        ICriteria CreateCriteria(string associationPath, JoinType joinType);
        ICriteria CreateCriteria(string associationPath, string alias);
        ICriteria CreateCriteria(string associationPath, string alias, JoinType joinType);
        ICriteria SetResultTransformer(IResultTransformer resultTransformer);
        ICriteria SetMaxResults(int maxResults);
        ICriteria SetFirstResult(int firstResult);
        ICriteria SetFetchSize(int fetchSize);
        ICriteria SetTimeout(int timeout);
        ICriteria SetCacheable(bool cacheable);
        ICriteria SetCacheRegion(string cacheRegion);
        ICriteria SetComment(string comment);
        ICriteria SetFlushMode(FlushMode flushMode);
        ICriteria SetCacheMode(CacheMode cacheMode);
        IList List();
        object UniqueResult();
        IEnumerable<T> Future<T>();
        IFutureValue<T> FutureValue<T>();
        void List(IList results);
        IList<T> List<T>();
        T UniqueResult<T>();
        void ClearOrders();
        ICriteria GetCriteriaByPath(string path);
        ICriteria GetCriteriaByAlias(string alias);
        Type GetRootEntityTypeIfAvailable();
    }
}
