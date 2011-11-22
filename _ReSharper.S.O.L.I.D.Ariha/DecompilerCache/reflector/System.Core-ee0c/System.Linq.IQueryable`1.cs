namespace System.Linq
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>Provides functionality to evaluate queries against a specific data source wherein the type of the data is known.</summary>
    /// <typeparam name="T">The type of the data in the data source.</typeparam>
    public interface IQueryable<T> : IEnumerable<T>, IQueryable, IEnumerable
    {
    }
}