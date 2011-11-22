namespace NHibernate.Criterion
{
    using NHibernate;
    using NHibernate.Engine;
    using NHibernate.SqlCommand;
    using NHibernate.Type;
    using NHibernate.Util;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A SQL fragment. The string {alias} will be replaced by the alias of the root entity.
    /// </summary>
    [Serializable]
    public sealed class SQLProjection : IProjection
    {
        private readonly string[] aliases;
        private readonly string[] columnAliases;
        private readonly string groupBy;
        private readonly bool grouped;
        private readonly string sql;
        private readonly IType[] types;

        internal SQLProjection(string sql, string[] columnAliases, IType[] types)
            : this(sql, null, columnAliases, types)
        {
        }

        internal SQLProjection(string sql, string groupBy, string[] columnAliases, IType[] types)
        {
            this.sql = sql;
            this.types = types;
            this.aliases = columnAliases;
            this.columnAliases = columnAliases;
            this.grouped = groupBy != null;
            this.groupBy = groupBy;
        }

        #region IProjection Members

        public string[] GetColumnAliases(int loc)
        {
            return this.columnAliases;
        }

        public string[] GetColumnAliases(string alias, int loc)
        {
            return null;
        }

        /// <summary>
        /// Gets the typed values for parameters in this projection
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="criteriaQuery">The criteria query.</param>
        /// <returns></returns>
        public TypedValue[] GetTypedValues(ICriteria criteria, ICriteriaQuery criteriaQuery)
        {
            return new TypedValue[0];
        }

        public IType[] GetTypes(ICriteria crit, ICriteriaQuery criteriaQuery)
        {
            return this.types;
        }

        public IType[] GetTypes(string alias, ICriteria crit, ICriteriaQuery criteriaQuery)
        {
            return null;
        }

        public SqlString ToGroupSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery,
                                          IDictionary<string, IFilter> enabledFilters)
        {
            return new SqlString(StringHelper.Replace(this.groupBy, "{alias}", criteriaQuery.GetSQLAlias(criteria)));
        }

        public SqlString ToSqlString(ICriteria criteria, int loc, ICriteriaQuery criteriaQuery,
                                     IDictionary<string, IFilter> enabledFilters)
        {
            return new SqlString(StringHelper.Replace(this.sql, "{alias}", criteriaQuery.GetSQLAlias(criteria)));
        }

        public string[] Aliases
        {
            get { return this.aliases; }
        }

        public bool IsAggregate
        {
            get { return false; }
        }

        public bool IsGrouped
        {
            get { return this.grouped; }
        }

        #endregion

        public override string ToString()
        {
            return this.sql;
        }
    }
}