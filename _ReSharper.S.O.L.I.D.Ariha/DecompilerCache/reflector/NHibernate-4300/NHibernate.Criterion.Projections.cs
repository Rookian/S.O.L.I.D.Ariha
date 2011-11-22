namespace NHibernate.Criterion
{
    using NHibernate.Dialect.Function;
    using NHibernate.Type;
    using System;

    /// <summary>
    /// The <tt>criterion</tt> package may be used by applications as a framework for building
    /// new kinds of <tt>Projection</tt>. However, it is intended that most applications will
    /// simply use the built-in projection types via the static factory methods of this class.<br />
    /// <br />
    /// The factory methods that take an alias allow the projected value to be referred to by 
    /// criterion and order instances.
    /// </summary>
    public class Projections
    {
        private Projections()
        {
        }

        /// <summary>
        /// Assign an alias to a projection, by wrapping it
        /// </summary>
        /// <param name="projection"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static IProjection Alias(IProjection projection, string alias)
        {
            return new AliasedProjection(projection, alias);
        }

        /// <summary>
        /// A property average value
        /// </summary>
        /// <param name="projection"></param>
        /// <returns></returns>
        public static AggregateProjection Avg(IProjection projection)
        {
            return new AvgProjection(projection);
        }

        /// <summary>
        /// A property average value
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static AggregateProjection Avg(string propertyName)
        {
            return new AvgProjection(propertyName);
        }

        /// <summary>
        /// Casts the projection result to the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="projection">The projection.</param>
        /// <returns></returns>
        public static IProjection Cast(IType type, IProjection projection)
        {
            return new CastProjection(type, projection);
        }

        /// <summary>
        /// Conditionally return the true or false part, dependention on the criterion
        /// </summary>
        /// <param name="criterion">The criterion.</param>
        /// <param name="whenTrue">The when true.</param>
        /// <param name="whenFalse">The when false.</param>
        /// <returns></returns>
        public static IProjection Conditional(ICriterion criterion, IProjection whenTrue, IProjection whenFalse)
        {
            return new ConditionalProjection(criterion, whenTrue, whenFalse);
        }

        /// <summary>
        /// Return a constant value
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static IProjection Constant(object obj)
        {
            return new ConstantProjection(obj);
        }

        /// <summary>
        /// A property value count
        /// </summary>
        /// <param name="projection"></param>
        /// <returns></returns>
        public static CountProjection Count(IProjection projection)
        {
            return new CountProjection(projection);
        }

        /// <summary>
        /// A property value count
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static CountProjection Count(string propertyName)
        {
            return new CountProjection(propertyName);
        }

        /// <summary>
        /// A distinct property value count
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static CountProjection CountDistinct(string propertyName)
        {
            return new CountProjection(propertyName).SetDistinct();
        }

        /// <summary>
        /// Create a distinct projection from a projection
        /// </summary>
        /// <param name="proj"></param>
        /// <returns></returns>
        public static IProjection Distinct(IProjection proj)
        {
            return new NHibernate.Criterion.Distinct(proj);
        }

        /// <summary>
        /// A grouping property value
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static PropertyProjection GroupProperty(string propertyName)
        {
            return new PropertyProjection(propertyName, true);
        }

        /// <summary>
        /// A projected identifier value
        /// </summary>
        /// <returns></returns>
        public static IdentifierProjection Id()
        {
            return new IdentifierProjection();
        }

        /// <summary>
        /// A projection maximum value
        /// </summary>
        /// <param name="projection"></param>
        /// <returns></returns>
        public static AggregateProjection Max(IProjection projection)
        {
            return new AggregateProjection("max", projection);
        }

        /// <summary>
        /// A property maximum value
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static AggregateProjection Max(string propertyName)
        {
            return new AggregateProjection("max", propertyName);
        }

        /// <summary>
        /// A projection minimum value
        /// </summary>
        /// <param name="projection"></param>
        /// <returns></returns>
        public static AggregateProjection Min(IProjection projection)
        {
            return new AggregateProjection("min", projection);
        }

        /// <summary>
        /// A property minimum value
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static AggregateProjection Min(string propertyName)
        {
            return new AggregateProjection("min", propertyName);
        }

        /// <summary>
        /// Create a new projection list
        /// </summary>
        /// <returns></returns>
        public static NHibernate.Criterion.ProjectionList ProjectionList()
        {
            return new NHibernate.Criterion.ProjectionList();
        }

        /// <summary>
        /// A projected property value
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static PropertyProjection Property(string propertyName)
        {
            return new PropertyProjection(propertyName);
        }

        /// <summary>
        /// The query row count, ie. <tt>count(*)</tt>
        /// </summary>
        /// <returns>The RowCount projection mapped to an <see cref="T:System.Int32" />.</returns>
        public static IProjection RowCount()
        {
            return new RowCountProjection();
        }

        /// <summary>
        /// The query row count, ie. <tt>count(*)</tt>
        /// </summary>
        /// <returns>The RowCount projection mapped to an <see cref="T:System.Int64" />.</returns>
        public static IProjection RowCountInt64()
        {
            return new RowCountInt64Projection();
        }

        /// <summary>
        /// Calls the specified <see cref="T:NHibernate.Dialect.Function.ISQLFunction" />
        /// </summary>
        /// <param name="function">the function.</param>
        /// <param name="type">The type.</param>
        /// <param name="projections">The projections.</param>
        /// <returns></returns>
        public static IProjection SqlFunction(ISQLFunction function, IType type, params IProjection[] projections)
        {
            return new SqlFunctionProjection(function, type, projections);
        }

        /// <summary>
        /// Calls the named <see cref="T:NHibernate.Dialect.Function.ISQLFunction" />
        /// </summary>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="type">The type.</param>
        /// <param name="projections">The projections.</param>
        /// <returns></returns>
        public static IProjection SqlFunction(string functionName, IType type, params IProjection[] projections)
        {
            return new SqlFunctionProjection(functionName, type, projections);
        }

        /// <summary>
        /// A grouping SQL projection, specifying both select clause and group by clause fragments
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="groupBy"></param>
        /// <param name="columnAliases"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IProjection SqlGroupProjection(string sql, string groupBy, string[] columnAliases, IType[] types)
        {
            return new SQLProjection(sql, groupBy, columnAliases, types);
        }

        /// <summary>
        /// A SQL projection, a typed select clause fragment
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="columnAliases"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IProjection SqlProjection(string sql, string[] columnAliases, IType[] types)
        {
            return new SQLProjection(sql, columnAliases, types);
        }

        public static IProjection SubQuery(DetachedCriteria detachedCriteria)
        {
            return new SubqueryProjection(new SelectSubqueryExpression(detachedCriteria));
        }

        /// <summary>
        /// A property value sum
        /// </summary>
        /// <param name="projection"></param>
        /// <returns></returns>
        public static AggregateProjection Sum(IProjection projection)
        {
            return new AggregateProjection("sum", projection);
        }

        /// <summary>
        /// A property value sum
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static AggregateProjection Sum(string propertyName)
        {
            return new AggregateProjection("sum", propertyName);
        }
    }
}