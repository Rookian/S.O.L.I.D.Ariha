namespace System.Linq
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;

    /// <summary>Provides a set of static (Shared in Visual Basic) methods for querying objects that implement <see cref="T:System.Collections.Generic.IEnumerable`1" />.</summary>
    public static class Enumerable
    {
        /// <summary>Applies an accumulator function over a sequence.</summary>
        /// <returns>The final accumulator value.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to aggregate over.</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="func" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (func == null)
            {
                throw Error.ArgumentNull("func");
            }
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    throw Error.NoElements();
                }
                TSource current = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    current = func(current, enumerator.Current);
                }
                return current;
            }
        }

        /// <summary>Applies an accumulator function over a sequence. The specified seed value is used as the initial accumulator value.</summary>
        /// <returns>The final accumulator value.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to aggregate over.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="func" /> is null.</exception>
        public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed,
                                                                  Func<TAccumulate, TSource, TAccumulate> func)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (func == null)
            {
                throw Error.ArgumentNull("func");
            }
            TAccumulate local = seed;
            foreach (TSource local2 in source)
            {
                local = func(local, local2);
            }
            return local;
        }

        /// <summary>Applies an accumulator function over a sequence. The specified seed value is used as the initial accumulator value, and the specified function is used to select the result value.</summary>
        /// <returns>The transformed final accumulator value.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to aggregate over.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">A function to transform the final accumulator value into the result value.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="func" /> or <paramref name="resultSelector" /> is null.</exception>
        public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source,
                                                                       TAccumulate seed,
                                                                       Func<TAccumulate, TSource, TAccumulate> func,
                                                                       Func<TAccumulate, TResult> resultSelector)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (func == null)
            {
                throw Error.ArgumentNull("func");
            }
            if (resultSelector == null)
            {
                throw Error.ArgumentNull("resultSelector");
            }
            TAccumulate local = seed;
            foreach (TSource local2 in source)
            {
                local = func(local, local2);
            }
            return resultSelector(local);
        }

        /// <summary>Determines whether all elements of a sequence satisfy a condition.</summary>
        /// <returns>true if every element of the source sequence passes the test in the specified predicate, or if the sequence is empty; otherwise, false.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements to apply the predicate to.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            foreach (TSource local in source)
            {
                if (!predicate(local))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>Determines whether a sequence contains any elements.</summary>
        /// <returns>true if the source sequence contains any elements; otherwise, false.</returns>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to check for emptiness.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static bool Any<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Determines whether any element of a sequence satisfies a condition.</summary>
        /// <returns>true if any elements in the source sequence pass the test in the specified predicate; otherwise, false.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to apply the predicate to.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            foreach (TSource local in source)
            {
                if (predicate(local))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Returns the input typed as <see cref="T:System.Collections.Generic.IEnumerable`1" />.</summary>
        /// <returns>The input sequence typed as <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
        /// <param name="source">The sequence to type as <see cref="T:System.Collections.Generic.IEnumerable`1" />.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            return source;
        }

        /// <summary>Computes the average of a sequence of nullable <see cref="T:System.Decimal" /> values.</summary>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Decimal" /> values to calculate the average of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
        public static decimal? Average(this IEnumerable<decimal?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            decimal num = 0M;
            long num2 = 0L;
            foreach (decimal? nullable in source)
            {
                if (nullable.HasValue)
                {
                    num += nullable.GetValueOrDefault();
                    num2 += 1L;
                }
            }
            if (num2 > 0L)
            {
                return new decimal?(num/num2);
            }
            return null;
        }

        /// <summary>Computes the average of a sequence of <see cref="T:System.Decimal" /> values.</summary>
        /// <returns>The average of the sequence of values.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Decimal" /> values to calculate the average of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        /// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
        public static decimal Average(this IEnumerable<decimal> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            decimal num = 0M;
            long num2 = 0L;
            foreach (decimal num3 in source)
            {
                num += num3;
                num2 += 1L;
            }
            if (num2 <= 0L)
            {
                throw Error.NoElements();
            }
            return (num/num2);
        }

        /// <summary>Computes the average of a sequence of <see cref="T:System.Double" /> values.</summary>
        /// <returns>The average of the sequence of values.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Double" /> values to calculate the average of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static double Average(this IEnumerable<double> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            double num = 0.0;
            long num2 = 0L;
            foreach (double num3 in source)
            {
                num += num3;
                num2 += 1L;
            }
            if (num2 <= 0L)
            {
                throw Error.NoElements();
            }
            return (num/((double) num2));
        }

        /// <summary>Computes the average of a sequence of <see cref="T:System.Int32" /> values.</summary>
        /// <returns>The average of the sequence of values.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Int32" /> values to calculate the average of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        /// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        public static double Average(this IEnumerable<int> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            long num = 0L;
            long num2 = 0L;
            foreach (int num3 in source)
            {
                num += num3;
                num2 += 1L;
            }
            if (num2 <= 0L)
            {
                throw Error.NoElements();
            }
            return (((double) num)/((double) num2));
        }

        /// <summary>Computes the average of a sequence of nullable <see cref="T:System.Double" /> values.</summary>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Double" /> values to calculate the average of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static double? Average(this IEnumerable<double?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            double num = 0.0;
            long num2 = 0L;
            foreach (double? nullable in source)
            {
                if (nullable.HasValue)
                {
                    num += nullable.GetValueOrDefault();
                    num2 += 1L;
                }
            }
            if (num2 > 0L)
            {
                return new double?(num/((double) num2));
            }
            return null;
        }

        /// <summary>Computes the average of a sequence of nullable <see cref="T:System.Int32" /> values.</summary>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Int32" />values to calculate the average of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        public static double? Average(this IEnumerable<int?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            long num = 0L;
            long num2 = 0L;
            foreach (int? nullable in source)
            {
                if (nullable.HasValue)
                {
                    num += (long) nullable.GetValueOrDefault();
                    num2 += 1L;
                }
            }
            if (num2 > 0L)
            {
                return new double?(((double) num)/((double) num2));
            }
            return null;
        }

        /// <summary>Computes the average of a sequence of nullable <see cref="T:System.Int64" /> values.</summary>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Int64" /> values to calculate the average of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        public static double? Average(this IEnumerable<long?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            long num = 0L;
            long num2 = 0L;
            foreach (long? nullable in source)
            {
                if (nullable.HasValue)
                {
                    num += nullable.GetValueOrDefault();
                    num2 += 1L;
                }
            }
            if (num2 > 0L)
            {
                return new double?(((double) num)/((double) num2));
            }
            return null;
        }

        /// <summary>Computes the average of a sequence of <see cref="T:System.Int64" /> values.</summary>
        /// <returns>The average of the sequence of values.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Int64" /> values to calculate the average of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        /// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        public static double Average(this IEnumerable<long> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            long num = 0L;
            long num2 = 0L;
            foreach (long num3 in source)
            {
                num += num3;
                num2 += 1L;
            }
            if (num2 <= 0L)
            {
                throw Error.NoElements();
            }
            return (((double) num)/((double) num2));
        }

        /// <summary>Computes the average of a sequence of nullable <see cref="T:System.Single" /> values.</summary>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Single" /> values to calculate the average of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static float? Average(this IEnumerable<float?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            double num = 0.0;
            long num2 = 0L;
            foreach (float? nullable in source)
            {
                if (nullable.HasValue)
                {
                    num += (double) nullable.GetValueOrDefault();
                    num2 += 1L;
                }
            }
            if (num2 > 0L)
            {
                return new float?((float) (num/((double) num2)));
            }
            return null;
        }

        /// <summary>Computes the average of a sequence of <see cref="T:System.Single" /> values.</summary>
        /// <returns>The average of the sequence of values.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Single" /> values to calculate the average of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static float Average(this IEnumerable<float> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            double num = 0.0;
            long num2 = 0L;
            foreach (float num3 in source)
            {
                num += num3;
                num2 += 1L;
            }
            if (num2 <= 0L)
            {
                throw Error.NoElements();
            }
            return (float) (num/((double) num2));
        }

        /// <summary>Computes the average of a sequence of nullable <see cref="T:System.Decimal" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
        public static decimal? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            return source.Select<TSource, decimal?>(selector).Average();
        }

        /// <summary>Computes the average of a sequence of <see cref="T:System.Decimal" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The average of the sequence of values.</returns>
        /// <param name="source">A sequence of values that are used to calculate an average.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        /// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
        public static decimal Average<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            return source.Select<TSource, decimal>(selector).Average();
        }

        /// <summary>Computes the average of a sequence of <see cref="T:System.Double" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The average of the sequence of values.</returns>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            return source.Select<TSource, double>(selector).Average();
        }

        /// <summary>Computes the average of a sequence of <see cref="T:System.Int32" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The average of the sequence of values.</returns>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        /// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return source.Select<TSource, int>(selector).Average();
        }

        /// <summary>Computes the average of a sequence of <see cref="T:System.Int64" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The average of the sequence of values.</returns>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="Tsource">The type of the elements of source.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        /// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            return source.Select<TSource, long>(selector).Average();
        }

        /// <summary>Computes the average of a sequence of nullable <see cref="T:System.Double" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        {
            return source.Select<TSource, double?>(selector).Average();
        }

        /// <summary>Computes the average of a sequence of nullable <see cref="T:System.Int32" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum of the elements in the sequence is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        {
            return source.Select<TSource, int?>(selector).Average();
        }

        /// <summary>Computes the average of a sequence of nullable <see cref="T:System.Int64" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        {
            return source.Select<TSource, long?>(selector).Average();
        }

        /// <summary>Computes the average of a sequence of nullable <see cref="T:System.Single" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static float? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        {
            return source.Select<TSource, float?>(selector).Average();
        }

        /// <summary>Computes the average of a sequence of <see cref="T:System.Single" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The average of the sequence of values.</returns>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static float Average<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            return source.Select<TSource, float>(selector).Average();
        }

        /// <summary>Converts the elements of an <see cref="T:System.Collections.IEnumerable" /> to the specified type.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains each element of the source sequence converted to the specified type.</returns>
        /// <param name="source">The <see cref="T:System.Collections.IEnumerable" /> that contains the elements to be converted.</param>
        /// <typeparam name="TResult">The type to convert the elements of <paramref name="source" /> to.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidCastException">An element in the sequence cannot be cast to type <paramref name="TResult" />.</exception>
        public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
        {
            IEnumerable<TResult> enumerable = source as IEnumerable<TResult>;
            if (enumerable != null)
            {
                return enumerable;
            }
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            return CastIterator<TResult>(source);
        }

        private static IEnumerable<TResult> CastIterator<TResult>(IEnumerable source)
        {
        <
            CastIterator > d__aa < TResult > _aa = new < CastIterator > d__aa<TResult>(-2);
            _aa.<>
            3
            __source = source;
            return _aa;
        }

        private static Func<TSource, bool> CombinePredicates<TSource>(Func<TSource, bool> predicate1,
                                                                      Func<TSource, bool> predicate2)
        {
            return delegate(TSource x)
                       {
                           if (base.predicate1(x))
                           {
                               return base.predicate2(x);
                           }
                           return false;
                       };
        }

        private static Func<TSource, TResult> CombineSelectors<TSource, TMiddle, TResult>(
            Func<TSource, TMiddle> selector1, Func<TMiddle, TResult> selector2)
        {
            return delegate(TSource x) { return base.selector2(base.selector1(x)); };
        }

        /// <summary>Concatenates two sequences.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the concatenated elements of the two input sequences.</returns>
        /// <param name="first">The first sequence to concatenate.</param>
        /// <param name="second">The sequence to concatenate to the first sequence.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="first" /> or <paramref name="second" /> is null.</exception>
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
            {
                throw Error.ArgumentNull("first");
            }
            if (second == null)
            {
                throw Error.ArgumentNull("second");
            }
            return ConcatIterator<TSource>(first, second);
        }

        private static IEnumerable<TSource> ConcatIterator<TSource>(IEnumerable<TSource> first,
                                                                    IEnumerable<TSource> second)
        {
        <
            ConcatIterator > d__71 < TSource > d__ = new < ConcatIterator > d__71<TSource>(-2);
            d__.<>
            3
            __first = first;
            d__.<>
            3
            __second = second;
            return d__;
        }

        /// <summary>Determines whether a sequence contains a specified element by using the default equality comparer.</summary>
        /// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
        /// <param name="source">A sequence in which to locate a value.</param>
        /// <param name="value">The value to locate in the sequence.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            ICollection<TSource> is2 = source as ICollection<TSource>;
            if (is2 != null)
            {
                return is2.Contains(value);
            }
            return source.Contains<TSource>(value, null);
        }

        /// <summary>Determines whether a sequence contains a specified element by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
        /// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
        /// <param name="source">A sequence in which to locate a value.</param>
        /// <param name="value">The value to locate in the sequence.</param>
        /// <param name="comparer">An equality comparer to compare values.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value,
                                             IEqualityComparer<TSource> comparer)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TSource>.Default;
            }
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            foreach (TSource local in source)
            {
                if (comparer.Equals(local, value))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>Returns the number of elements in a sequence.</summary>
        /// <returns>The number of elements in the input sequence.</returns>
        /// <param name="source">A sequence that contains elements to be counted.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
        public static int Count<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            ICollection<TSource> is2 = source as ICollection<TSource>;
            if (is2 != null)
            {
                return is2.Count;
            }
            int num = 0;
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    num++;
                }
            }
            return num;
        }

        /// <summary>Returns a number that represents how many elements in the specified sequence satisfy a condition.</summary>
        /// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function.</returns>
        /// <param name="source">A sequence that contains elements to be tested and counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
        public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            int num = 0;
            foreach (TSource local in source)
            {
                if (predicate(local))
                {
                    num++;
                }
            }
            return num;
        }

        /// <summary>Returns the elements of the specified sequence or the type parameter's default value in a singleton collection if the sequence is empty.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains default(<paramref name="TSource" />) if <paramref name="source" /> is empty; otherwise, <paramref name="source" />.</returns>
        /// <param name="source">The sequence to return a default value for if it is empty.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source.DefaultIfEmpty<TSource>(default(TSource));
        }

        /// <summary>Returns the elements of the specified sequence or the specified value in a singleton collection if the sequence is empty.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains <paramref name="defaultValue" /> if <paramref name="source" /> is empty; otherwise, <paramref name="source" />.</returns>
        /// <param name="source">The sequence to return the specified value for if it is empty.</param>
        /// <param name="defaultValue">The value to return if the sequence is empty.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source,
                                                                   TSource defaultValue)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            return DefaultIfEmptyIterator<TSource>(source, defaultValue);
        }

        private static IEnumerable<TSource> DefaultIfEmptyIterator<TSource>(IEnumerable<TSource> source,
                                                                            TSource defaultValue)
        {
        <
            DefaultIfEmptyIterator > d__9e < TSource > d__e = new < DefaultIfEmptyIterator > d__9e<TSource>(-2);
            d__e.<>
            3
            __source = source;
            d__e.<>
            3
            __defaultValue = defaultValue;
            return d__e;
        }

        /// <summary>Returns distinct elements from a sequence by using the default equality comparer to compare values.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains distinct elements from the source sequence.</returns>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            return DistinctIterator<TSource>(source, null);
        }

        /// <summary>Returns distinct elements from a sequence by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains distinct elements from the source sequence.</returns>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source,
                                                             IEqualityComparer<TSource> comparer)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            return DistinctIterator<TSource>(source, comparer);
        }

        private static IEnumerable<TSource> DistinctIterator<TSource>(IEnumerable<TSource> source,
                                                                      IEqualityComparer<TSource> comparer)
        {
        <
            DistinctIterator > d__7a < TSource > d__a = new < DistinctIterator > d__7a<TSource>(-2);
            d__a.<>
            3
            __source = source;
            d__a.<>
            3
            __comparer = comparer;
            return d__a;
        }

        /// <summary>Returns the element at a specified index in a sequence.</summary>
        /// <returns>The element at the specified position in the source sequence.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index" /> is less than 0 or greater than or equal to the number of elements in <paramref name="source" />.</exception>
        public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index)
        {
            TSource current;
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                return list[index];
            }
            if (index < 0)
            {
                throw Error.ArgumentOutOfRange("index");
            }
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                Label_0036:
                if (!enumerator.MoveNext())
                {
                    throw Error.ArgumentOutOfRange("index");
                }
                if (index == 0)
                {
                    current = enumerator.Current;
                }
                else
                {
                    index--;
                    goto Label_0036;
                }
            }
            return current;
        }

        /// <summary>Returns the element at a specified index in a sequence or a default value if the index is out of range.</summary>
        /// <returns>default(<paramref name="TSource" />) if the index is outside the bounds of the source sequence; otherwise, the element at the specified position in the source sequence.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (index >= 0)
            {
                IList<TSource> list = source as IList<TSource>;
                if (list != null)
                {
                    if (index < list.Count)
                    {
                        return list[index];
                    }
                }
                else
                {
                    using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            if (index == 0)
                            {
                                return enumerator.Current;
                            }
                            index--;
                        }
                    }
                }
            }
            return default(TSource);
        }

        /// <summary>Returns an empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> that has the specified type argument.</summary>
        /// <returns>An empty <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose type argument is <paramref name="TResult" />.</returns>
        /// <typeparam name="TResult">The type to assign to the type parameter of the returned generic <see cref="T:System.Collections.Generic.IEnumerable`1" />.</typeparam>
        public static IEnumerable<TResult> Empty<TResult>()
        {
            return EmptyEnumerable<TResult>.Instance;
        }

        /// <summary>Produces the set difference of two sequences by using the default equality comparer to compare values.</summary>
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        /// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that are not also in <paramref name="second" /> will be returned.</param>
        /// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="first" /> or <paramref name="second" /> is null.</exception>
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
            {
                throw Error.ArgumentNull("first");
            }
            if (second == null)
            {
                throw Error.ArgumentNull("second");
            }
            return ExceptIterator<TSource>(first, second, null);
        }

        /// <summary>Produces the set difference of two sequences by using the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        /// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that are not also in <paramref name="second" /> will be returned.</param>
        /// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="first" /> or <paramref name="second" /> is null.</exception>
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second,
                                                           IEqualityComparer<TSource> comparer)
        {
            if (first == null)
            {
                throw Error.ArgumentNull("first");
            }
            if (second == null)
            {
                throw Error.ArgumentNull("second");
            }
            return ExceptIterator<TSource>(first, second, comparer);
        }

        private static IEnumerable<TSource> ExceptIterator<TSource>(IEnumerable<TSource> first,
                                                                    IEnumerable<TSource> second,
                                                                    IEqualityComparer<TSource> comparer)
        {
        <
            ExceptIterator > d__92 < TSource > d__ = new < ExceptIterator > d__92<TSource>(-2);
            d__.<>
            3
            __first = first;
            d__.<>
            3
            __second = second;
            d__.<>
            3
            __comparer = comparer;
            return d__;
        }

        /// <summary>Returns the first element of a sequence.</summary>
        /// <returns>The first element in the specified sequence.</returns>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the first element of.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
        public static TSource First<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        return enumerator.Current;
                    }
                }
            }
            throw Error.NoElements();
        }

        /// <summary>Returns the first element in a sequence that satisfies a specified condition.</summary>
        /// <returns>The first element in the sequence that passes the test in the specified predicate function.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            foreach (TSource local in source)
            {
                if (predicate(local))
                {
                    return local;
                }
            }
            throw Error.NoMatch();
        }

        /// <summary>Returns the first element of a sequence, or a default value if the sequence contains no elements.</summary>
        /// <returns>default(<paramref name="TSource" />) if <paramref name="source" /> is empty; otherwise, the first element in <paramref name="source" />.</returns>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the first element of.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        return enumerator.Current;
                    }
                }
            }
            return default(TSource);
        }

        /// <summary>Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.</summary>
        /// <returns>default(<paramref name="TSource" />) if <paramref name="source" /> is empty or if no element passes the test specified by <paramref name="predicate" />; otherwise, the first element in <paramref name="source" /> that passes the test specified by <paramref name="predicate" />.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            foreach (TSource local in source)
            {
                if (predicate(local))
                {
                    return local;
                }
            }
            return default(TSource);
        }

        /// <summary>Groups the elements of a sequence according to a specified key selector function.</summary>
        /// <returns>An IEnumerable&lt;IGrouping&lt;TKey, TSource&gt;&gt; in C# or IEnumerable(Of IGrouping(Of TKey, TSource)) in Visual Basic where each <see cref="T:System.Linq.IGrouping`2" /> object contains a sequence of objects and a key.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
        public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source,
                                                                                   Func<TSource, TKey> keySelector)
        {
            return new GroupedEnumerable<TSource, TKey, TSource>(source, keySelector, IdentityFunction<TSource>.Instance,
                                                                 null);
        }

        /// <summary>Groups the elements of a sequence according to a specified key selector function and compares the keys by using a specified comparer.</summary>
        /// <returns>An IEnumerable&lt;IGrouping&lt;TKey, TSource&gt;&gt; in C# or IEnumerable(Of IGrouping(Of TKey, TSource)) in Visual Basic where each <see cref="T:System.Linq.IGrouping`2" /> object contains a collection of objects and a key.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
        public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source,
                                                                                   Func<TSource, TKey> keySelector,
                                                                                   IEqualityComparer<TKey> comparer)
        {
            return new GroupedEnumerable<TSource, TKey, TSource>(source, keySelector, IdentityFunction<TSource>.Instance,
                                                                 comparer);
        }

        /// <summary>Groups the elements of a sequence according to a specified key selector function and projects the elements for each group by using a specified function.</summary>
        /// <returns>An IEnumerable&lt;IGrouping&lt;TKey, TElement&gt;&gt; in C# or IEnumerable(Of IGrouping(Of TKey, TElement)) in Visual Basic where each <see cref="T:System.Linq.IGrouping`2" /> object contains a collection of objects of type <paramref name="TElement" /> and a key.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="elementSelector">A function to map each source element to an element in the <see cref="T:System.Linq.IGrouping`2" />.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <typeparam name="TElement">The type of the elements in the <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.</exception>
        public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            return new GroupedEnumerable<TSource, TKey, TElement>(source, keySelector, elementSelector, null);
        }

        /// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key.</summary>
        /// <returns>A collection of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="resultSelector">A function to create a result value from each group.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source,
                                                                           Func<TSource, TKey> keySelector,
                                                                           Func<TKey, IEnumerable<TSource>, TResult>
                                                                               resultSelector)
        {
            return new GroupedEnumerable<TSource, TKey, TSource, TResult>(source, keySelector,
                                                                          IdentityFunction<TSource>.Instance,
                                                                          resultSelector, null);
        }

        /// <summary>Groups the elements of a sequence according to a key selector function. The keys are compared by using a comparer and each group's elements are projected by using a specified function.</summary>
        /// <returns>An IEnumerable&lt;IGrouping&lt;TKey, TElement&gt;&gt; in C# or IEnumerable(Of IGrouping(Of TKey, TElement)) in Visual Basic where each <see cref="T:System.Linq.IGrouping`2" /> object contains a collection of objects of type <paramref name="TElement" /> and a key.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="elementSelector">A function to map each source element to an element in an <see cref="T:System.Linq.IGrouping`2" />.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <typeparam name="TElement">The type of the elements in the <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.</exception>
        public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey> comparer)
        {
            return new GroupedEnumerable<TSource, TKey, TElement>(source, keySelector, elementSelector, comparer);
        }

        /// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. The elements of each group are projected by using a specified function.</summary>
        /// <returns>A collection of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="elementSelector">A function to map each source element to an element in an <see cref="T:System.Linq.IGrouping`2" />.</param>
        /// <param name="resultSelector">A function to create a result value from each group.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <typeparam name="TElement">The type of the elements in each <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
        /// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source,
                                                                                     Func<TSource, TKey> keySelector,
                                                                                     Func<TSource, TElement>
                                                                                         elementSelector,
                                                                                     Func
                                                                                         <TKey, IEnumerable<TElement>,
                                                                                         TResult> resultSelector)
        {
            return new GroupedEnumerable<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector,
                                                                           resultSelector, null);
        }

        /// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. The keys are compared by using a specified comparer.</summary>
        /// <returns>A collection of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="resultSelector">A function to create a result value from each group.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys with.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source,
                                                                           Func<TSource, TKey> keySelector,
                                                                           Func<TKey, IEnumerable<TSource>, TResult>
                                                                               resultSelector,
                                                                           IEqualityComparer<TKey> comparer)
        {
            return new GroupedEnumerable<TSource, TKey, TSource, TResult>(source, keySelector,
                                                                          IdentityFunction<TSource>.Instance,
                                                                          resultSelector, comparer);
        }

        /// <summary>Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. Key values are compared by using a specified comparer, and the elements of each group are projected by using a specified function.</summary>
        /// <returns>A collection of elements of type <paramref name="TResult" /> where each element represents a projection over a group and its key.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="elementSelector">A function to map each source element to an element in an <see cref="T:System.Linq.IGrouping`2" />.</param>
        /// <param name="resultSelector">A function to create a result value from each group.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys with.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <typeparam name="TElement">The type of the elements in each <see cref="T:System.Linq.IGrouping`2" />.</typeparam>
        /// <typeparam name="TResult">The type of the result value returned by <paramref name="resultSelector" />.</typeparam>
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source,
                                                                                     Func<TSource, TKey> keySelector,
                                                                                     Func<TSource, TElement>
                                                                                         elementSelector,
                                                                                     Func
                                                                                         <TKey, IEnumerable<TElement>,
                                                                                         TResult> resultSelector,
                                                                                     IEqualityComparer<TKey> comparer)
        {
            return new GroupedEnumerable<TSource, TKey, TElement, TResult>(source, keySelector, elementSelector,
                                                                           resultSelector, comparer);
        }

        /// <summary>Correlates the elements of two sequences based on equality of keys and groups the results. The default equality comparer is used to compare keys.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements of type <paramref name="TResult" /> that are obtained by performing a grouped join on two sequences.</returns>
        /// <param name="outer">The first sequence to join.</param>
        /// <param name="inner">The sequence to join to the first sequence.</param>
        /// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
        /// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="outer" /> or <paramref name="inner" /> or <paramref name="outerKeySelector" /> or <paramref name="innerKeySelector" /> or <paramref name="resultSelector" /> is null.</exception>
        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,
                                                                                    IEnumerable<TInner> inner,
                                                                                    Func<TOuter, TKey> outerKeySelector,
                                                                                    Func<TInner, TKey> innerKeySelector,
                                                                                    Func
                                                                                        <TOuter, IEnumerable<TInner>,
                                                                                        TResult> resultSelector)
        {
            if (outer == null)
            {
                throw Error.ArgumentNull("outer");
            }
            if (inner == null)
            {
                throw Error.ArgumentNull("inner");
            }
            if (outerKeySelector == null)
            {
                throw Error.ArgumentNull("outerKeySelector");
            }
            if (innerKeySelector == null)
            {
                throw Error.ArgumentNull("innerKeySelector");
            }
            if (resultSelector == null)
            {
                throw Error.ArgumentNull("resultSelector");
            }
            return GroupJoinIterator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector,
                                                                    resultSelector, null);
        }

        /// <summary>Correlates the elements of two sequences based on key equality and groups the results. A specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> is used to compare keys.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements of type <paramref name="TResult" /> that are obtained by performing a grouped join on two sequences.</returns>
        /// <param name="outer">The first sequence to join.</param>
        /// <param name="inner">The sequence to join to the first sequence.</param>
        /// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to hash and compare keys.</param>
        /// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="outer" /> or <paramref name="inner" /> or <paramref name="outerKeySelector" /> or <paramref name="innerKeySelector" /> or <paramref name="resultSelector" /> is null.</exception>
        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,
                                                                                    IEnumerable<TInner> inner,
                                                                                    Func<TOuter, TKey> outerKeySelector,
                                                                                    Func<TInner, TKey> innerKeySelector,
                                                                                    Func
                                                                                        <TOuter, IEnumerable<TInner>,
                                                                                        TResult> resultSelector,
                                                                                    IEqualityComparer<TKey> comparer)
        {
            if (outer == null)
            {
                throw Error.ArgumentNull("outer");
            }
            if (inner == null)
            {
                throw Error.ArgumentNull("inner");
            }
            if (outerKeySelector == null)
            {
                throw Error.ArgumentNull("outerKeySelector");
            }
            if (innerKeySelector == null)
            {
                throw Error.ArgumentNull("innerKeySelector");
            }
            if (resultSelector == null)
            {
                throw Error.ArgumentNull("resultSelector");
            }
            return GroupJoinIterator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector,
                                                                    resultSelector, comparer);
        }

        private static IEnumerable<TResult> GroupJoinIterator<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer,
                                                                                             IEnumerable<TInner> inner,
                                                                                             Func<TOuter, TKey>
                                                                                                 outerKeySelector,
                                                                                             Func<TInner, TKey>
                                                                                                 innerKeySelector,
                                                                                             Func
                                                                                                 <TOuter,
                                                                                                 IEnumerable<TInner>,
                                                                                                 TResult> resultSelector,
                                                                                             IEqualityComparer<TKey>
                                                                                                 comparer)
        {
        <
            GroupJoinIterator > d__6a < TOuter,
            TInner,
            TKey,
            TResult > d__a = new < GroupJoinIterator > d__6a<TOuter, TInner, TKey, TResult>(-2);
            d__a.<>
            3
            __outer = outer;
            d__a.<>
            3
            __inner = inner;
            d__a.<>
            3
            __outerKeySelector = outerKeySelector;
            d__a.<>
            3
            __innerKeySelector = innerKeySelector;
            d__a.<>
            3
            __resultSelector = resultSelector;
            d__a.<>
            3
            __comparer = comparer;
            return d__a;
        }

        /// <summary>Produces the set intersection of two sequences by using the default equality comparer to compare values.</summary>
        /// <returns>A sequence that contains the elements that form the set intersection of two sequences.</returns>
        /// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements that also appear in <paramref name="second" /> will be returned.</param>
        /// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements that also appear in the first sequence will be returned.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="first" /> or <paramref name="second" /> is null.</exception>
        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first,
                                                              IEnumerable<TSource> second)
        {
            if (first == null)
            {
                throw Error.ArgumentNull("first");
            }
            if (second == null)
            {
                throw Error.ArgumentNull("second");
            }
            return IntersectIterator<TSource>(first, second, null);
        }

        /// <summary>Produces the set intersection of two sequences by using the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</summary>
        /// <returns>A sequence that contains the elements that form the set intersection of two sequences.</returns>
        /// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements that also appear in <paramref name="second" /> will be returned.</param>
        /// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements that also appear in the first sequence will be returned.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="first" /> or <paramref name="second" /> is null.</exception>
        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first,
                                                              IEnumerable<TSource> second,
                                                              IEqualityComparer<TSource> comparer)
        {
            if (first == null)
            {
                throw Error.ArgumentNull("first");
            }
            if (second == null)
            {
                throw Error.ArgumentNull("second");
            }
            return IntersectIterator<TSource>(first, second, comparer);
        }

        private static IEnumerable<TSource> IntersectIterator<TSource>(IEnumerable<TSource> first,
                                                                       IEnumerable<TSource> second,
                                                                       IEqualityComparer<TSource> comparer)
        {
        <
            IntersectIterator > d__8b < TSource > d__b = new < IntersectIterator > d__8b<TSource>(-2);
            d__b.<>
            3
            __first = first;
            d__b.<>
            3
            __second = second;
            d__b.<>
            3
            __comparer = comparer;
            return d__b;
        }

        /// <summary>Correlates the elements of two sequences based on matching keys. The default equality comparer is used to compare keys.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that has elements of type <paramref name="TResult" /> that are obtained by performing an inner join on two sequences.</returns>
        /// <param name="outer">The first sequence to join.</param>
        /// <param name="inner">The sequence to join to the first sequence.</param>
        /// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">A function to create a result element from two matching elements.</param>
        /// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="outer" /> or <paramref name="inner" /> or <paramref name="outerKeySelector" /> or <paramref name="innerKeySelector" /> or <paramref name="resultSelector" /> is null.</exception>
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,
                                                                               IEnumerable<TInner> inner,
                                                                               Func<TOuter, TKey> outerKeySelector,
                                                                               Func<TInner, TKey> innerKeySelector,
                                                                               Func<TOuter, TInner, TResult>
                                                                                   resultSelector)
        {
            if (outer == null)
            {
                throw Error.ArgumentNull("outer");
            }
            if (inner == null)
            {
                throw Error.ArgumentNull("inner");
            }
            if (outerKeySelector == null)
            {
                throw Error.ArgumentNull("outerKeySelector");
            }
            if (innerKeySelector == null)
            {
                throw Error.ArgumentNull("innerKeySelector");
            }
            if (resultSelector == null)
            {
                throw Error.ArgumentNull("resultSelector");
            }
            return JoinIterator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector,
                                                               resultSelector, null);
        }

        /// <summary>Correlates the elements of two sequences based on matching keys. A specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> is used to compare keys.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that has elements of type <paramref name="TResult" /> that are obtained by performing an inner join on two sequences.</returns>
        /// <param name="outer">The first sequence to join.</param>
        /// <param name="inner">The sequence to join to the first sequence.</param>
        /// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">A function to create a result element from two matching elements.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to hash and compare keys.</param>
        /// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="outer" /> or <paramref name="inner" /> or <paramref name="outerKeySelector" /> or <paramref name="innerKeySelector" /> or <paramref name="resultSelector" /> is null.</exception>
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer,
                                                                               IEnumerable<TInner> inner,
                                                                               Func<TOuter, TKey> outerKeySelector,
                                                                               Func<TInner, TKey> innerKeySelector,
                                                                               Func<TOuter, TInner, TResult>
                                                                                   resultSelector,
                                                                               IEqualityComparer<TKey> comparer)
        {
            if (outer == null)
            {
                throw Error.ArgumentNull("outer");
            }
            if (inner == null)
            {
                throw Error.ArgumentNull("inner");
            }
            if (outerKeySelector == null)
            {
                throw Error.ArgumentNull("outerKeySelector");
            }
            if (innerKeySelector == null)
            {
                throw Error.ArgumentNull("innerKeySelector");
            }
            if (resultSelector == null)
            {
                throw Error.ArgumentNull("resultSelector");
            }
            return JoinIterator<TOuter, TInner, TKey, TResult>(outer, inner, outerKeySelector, innerKeySelector,
                                                               resultSelector, comparer);
        }

        private static IEnumerable<TResult> JoinIterator<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer,
                                                                                        IEnumerable<TInner> inner,
                                                                                        Func<TOuter, TKey>
                                                                                            outerKeySelector,
                                                                                        Func<TInner, TKey>
                                                                                            innerKeySelector,
                                                                                        Func<TOuter, TInner, TResult>
                                                                                            resultSelector,
                                                                                        IEqualityComparer<TKey> comparer)
        {
        <
            JoinIterator > d__61 < TOuter,
            TInner,
            TKey,
            TResult > d__ = new < JoinIterator > d__61<TOuter, TInner, TKey, TResult>(-2);
            d__.<>
            3
            __outer = outer;
            d__.<>
            3
            __inner = inner;
            d__.<>
            3
            __outerKeySelector = outerKeySelector;
            d__.<>
            3
            __innerKeySelector = innerKeySelector;
            d__.<>
            3
            __resultSelector = resultSelector;
            d__.<>
            3
            __comparer = comparer;
            return d__;
        }

        /// <summary>Returns the last element of a sequence.</summary>
        /// <returns>The value at the last position in the source sequence.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the last element of.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
        public static TSource Last<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                int count = list.Count;
                if (count > 0)
                {
                    return list[count - 1];
                }
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        TSource current;
                        do
                        {
                            current = enumerator.Current;
                        } while (enumerator.MoveNext());
                        return current;
                    }
                }
            }
            throw Error.NoElements();
        }

        /// <summary>Returns the last element of a sequence that satisfies a specified condition.</summary>
        /// <returns>The last element in the sequence that passes the test in the specified predicate function.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
        public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            TSource local = default(TSource);
            bool flag = false;
            foreach (TSource local2 in source)
            {
                if (predicate(local2))
                {
                    local = local2;
                    flag = true;
                }
            }
            if (!flag)
            {
                throw Error.NoMatch();
            }
            return local;
        }

        /// <summary>Returns the last element of a sequence, or a default value if the sequence contains no elements.</summary>
        /// <returns>default(<paramref name="TSource" />) if the source sequence is empty; otherwise, the last element in the <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the last element of.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                int count = list.Count;
                if (count > 0)
                {
                    return list[count - 1];
                }
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        TSource current;
                        do
                        {
                            current = enumerator.Current;
                        } while (enumerator.MoveNext());
                        return current;
                    }
                }
            }
            return default(TSource);
        }

        /// <summary>Returns the last element of a sequence that satisfies a condition or a default value if no such element is found.</summary>
        /// <returns>default(<paramref name="TSource" />) if the sequence is empty or if no elements pass the test in the predicate function; otherwise, the last element that passes the test in the predicate function.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            TSource local = default(TSource);
            foreach (TSource local2 in source)
            {
                if (predicate(local2))
                {
                    local = local2;
                }
            }
            return local;
        }

        /// <summary>Returns an <see cref="T:System.Int64" /> that represents the total number of elements in a sequence.</summary>
        /// <returns>The number of elements in the source sequence.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements to be counted.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The number of elements exceeds <see cref="F:System.Int64.MaxValue" />.</exception>
        public static long LongCount<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            long num = 0L;
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    num += 1L;
                }
            }
            return num;
        }

        /// <summary>Returns an <see cref="T:System.Int64" /> that represents  how many elements in a sequence satisfy a condition.</summary>
        /// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements to be counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The number of matching elements exceeds <see cref="F:System.Int64.MaxValue" />.</exception>
        public static long LongCount<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            long num = 0L;
            foreach (TSource local in source)
            {
                if (predicate(local))
                {
                    num += 1L;
                }
            }
            return num;
        }

        /// <summary>Returns the maximum value in a sequence of <see cref="T:System.Decimal" /> values.</summary>
        /// <returns>The maximum value in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Decimal" /> values to determine the maximum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static decimal Max(this IEnumerable<decimal> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            decimal num = 0M;
            bool flag = false;
            foreach (decimal num2 in source)
            {
                if (flag)
                {
                    if (num2 > num)
                    {
                        num = num2;
                    }
                }
                else
                {
                    num = num2;
                    flag = true;
                }
            }
            if (!flag)
            {
                throw Error.NoElements();
            }
            return num;
        }

        /// <summary>Returns the maximum value in a sequence of nullable <see cref="T:System.Decimal" /> values.</summary>
        /// <returns>A value of type Nullable&lt;Decimal&gt; in C# or Nullable(Of Decimal) in Visual Basic that corresponds to the maximum value in the sequence. </returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Decimal" /> values to determine the maximum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static decimal? Max(this IEnumerable<decimal?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            decimal? nullable = null;
            foreach (decimal? nullable2 in source)
            {
                if (nullable.HasValue)
                {
                    decimal? nullable3 = nullable2;
                    decimal? nullable4 = nullable;
                    if ((nullable3.GetValueOrDefault() <= nullable4.GetValueOrDefault()) ||
                        !(nullable3.HasValue & nullable4.HasValue))
                    {
                        continue;
                    }
                }
                nullable = nullable2;
            }
            return nullable;
        }

        /// <summary>Returns the maximum value in a sequence of <see cref="T:System.Int32" /> values.</summary>
        /// <returns>The maximum value in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Int32" /> values to determine the maximum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static int Max(this IEnumerable<int> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            int num = 0;
            bool flag = false;
            foreach (int num2 in source)
            {
                if (flag)
                {
                    if (num2 > num)
                    {
                        num = num2;
                    }
                }
                else
                {
                    num = num2;
                    flag = true;
                }
            }
            if (!flag)
            {
                throw Error.NoElements();
            }
            return num;
        }

        /// <summary>Returns the maximum value in a sequence of nullable <see cref="T:System.Double" /> values.</summary>
        /// <returns>A value of type Nullable&lt;Double&gt; in C# or Nullable(Of Double) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Double" /> values to determine the maximum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static double? Max(this IEnumerable<double?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            double? nullable = null;
            foreach (double? nullable2 in source)
            {
                if (nullable2.HasValue)
                {
                    if (nullable.HasValue)
                    {
                        double? nullable3 = nullable2;
                        double? nullable4 = nullable;
                        if (((nullable3.GetValueOrDefault() <= nullable4.GetValueOrDefault()) ||
                             !(nullable3.HasValue & nullable4.HasValue)) && !double.IsNaN(nullable.Value))
                        {
                            continue;
                        }
                    }
                    nullable = nullable2;
                }
            }
            return nullable;
        }

        /// <summary>Returns the maximum value in a sequence of nullable <see cref="T:System.Int64" /> values.</summary>
        /// <returns>A value of type Nullable&lt;Int64&gt; in C# or Nullable(Of Int64) in Visual Basic that corresponds to the maximum value in the sequence. </returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Int64" /> values to determine the maximum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static long? Max(this IEnumerable<long?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            long? nullable = null;
            foreach (long? nullable2 in source)
            {
                if (nullable.HasValue)
                {
                    long? nullable3 = nullable2;
                    long? nullable4 = nullable;
                    if ((nullable3.GetValueOrDefault() <= nullable4.GetValueOrDefault()) ||
                        !(nullable3.HasValue & nullable4.HasValue))
                    {
                        continue;
                    }
                }
                nullable = nullable2;
            }
            return nullable;
        }

        /// <summary>Returns the maximum value in a sequence of <see cref="T:System.Double" /> values.</summary>
        /// <returns>The maximum value in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Double" /> values to determine the maximum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static double Max(this IEnumerable<double> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            double d = 0.0;
            bool flag = false;
            foreach (double num2 in source)
            {
                if (flag)
                {
                    if ((num2 > d) || double.IsNaN(d))
                    {
                        d = num2;
                    }
                    continue;
                }
                d = num2;
                flag = true;
            }
            if (!flag)
            {
                throw Error.NoElements();
            }
            return d;
        }

        /// <summary>Returns the maximum value in a sequence of nullable <see cref="T:System.Int32" /> values.</summary>
        /// <returns>A value of type Nullable&lt;Int32&gt; in C# or Nullable(Of Int32) in Visual Basic that corresponds to the maximum value in the sequence. </returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Int32" /> values to determine the maximum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static int? Max(this IEnumerable<int?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            int? nullable = null;
            foreach (int? nullable2 in source)
            {
                if (nullable.HasValue)
                {
                    int? nullable3 = nullable2;
                    int? nullable4 = nullable;
                    if ((nullable3.GetValueOrDefault() <= nullable4.GetValueOrDefault()) ||
                        !(nullable3.HasValue & nullable4.HasValue))
                    {
                        continue;
                    }
                }
                nullable = nullable2;
            }
            return nullable;
        }

        /// <summary>Returns the maximum value in a sequence of <see cref="T:System.Int64" /> values.</summary>
        /// <returns>The maximum value in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Int64" /> values to determine the maximum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static long Max(this IEnumerable<long> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            long num = 0L;
            bool flag = false;
            foreach (long num2 in source)
            {
                if (flag)
                {
                    if (num2 > num)
                    {
                        num = num2;
                    }
                }
                else
                {
                    num = num2;
                    flag = true;
                }
            }
            if (!flag)
            {
                throw Error.NoElements();
            }
            return num;
        }

        /// <summary>Returns the maximum value in a sequence of nullable <see cref="T:System.Single" /> values.</summary>
        /// <returns>A value of type Nullable&lt;Single&gt; in C# or Nullable(Of Single) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Single" /> values to determine the maximum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static float? Max(this IEnumerable<float?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            float? nullable = null;
            foreach (float? nullable2 in source)
            {
                if (nullable2.HasValue)
                {
                    if (nullable.HasValue)
                    {
                        float? nullable3 = nullable2;
                        float? nullable4 = nullable;
                        if (((nullable3.GetValueOrDefault() <= nullable4.GetValueOrDefault()) ||
                             !(nullable3.HasValue & nullable4.HasValue)) && !float.IsNaN(nullable.Value))
                        {
                            continue;
                        }
                    }
                    nullable = nullable2;
                }
            }
            return nullable;
        }

        /// <summary>Returns the maximum value in a sequence of <see cref="T:System.Single" /> values.</summary>
        /// <returns>The maximum value in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Single" /> values to determine the maximum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static float Max(this IEnumerable<float> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            float num = 0f;
            bool flag = false;
            foreach (float num2 in source)
            {
                if (flag)
                {
                    if ((num2 > num) || double.IsNaN((double) num))
                    {
                        num = num2;
                    }
                    continue;
                }
                num = num2;
                flag = true;
            }
            if (!flag)
            {
                throw Error.NoElements();
            }
            return num;
        }

        /// <summary>Returns the maximum value in a generic sequence.</summary>
        /// <returns>The maximum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static TSource Max<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            Comparer<TSource> comparer = Comparer<TSource>.Default;
            TSource y = default(TSource);
            if (y == null)
            {
                foreach (TSource local2 in source)
                {
                    if ((local2 != null) && ((y == null) || (comparer.Compare(local2, y) > 0)))
                    {
                        y = local2;
                    }
                }
                return y;
            }
            bool flag = false;
            foreach (TSource local3 in source)
            {
                if (flag)
                {
                    if (comparer.Compare(local3, y) > 0)
                    {
                        y = local3;
                    }
                }
                else
                {
                    y = local3;
                    flag = true;
                }
            }
            if (!flag)
            {
                throw Error.NoElements();
            }
            return y;
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the maximum <see cref="T:System.Decimal" /> value.</summary>
        /// <returns>The maximum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static decimal Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            return source.Select<TSource, decimal>(selector).Max();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the maximum <see cref="T:System.Double" /> value.</summary>
        /// <returns>The maximum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static double Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            return source.Select<TSource, double>(selector).Max();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the maximum <see cref="T:System.Int32" /> value.</summary>
        /// <returns>The maximum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static int Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return source.Select<TSource, int>(selector).Max();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the maximum <see cref="T:System.Int64" /> value.</summary>
        /// <returns>The maximum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static long Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            return source.Select<TSource, long>(selector).Max();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the maximum nullable <see cref="T:System.Int64" /> value.</summary>
        /// <returns>The value of type Nullable&lt;Int64&gt; in C# or Nullable(Of Int64) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static long? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        {
            return source.Select<TSource, long?>(selector).Max();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the maximum nullable <see cref="T:System.Decimal" /> value.</summary>
        /// <returns>The value of type Nullable&lt;Decimal&gt; in C# or Nullable(Of Decimal) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static decimal? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            return source.Select<TSource, decimal?>(selector).Max();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the maximum nullable <see cref="T:System.Double" /> value.</summary>
        /// <returns>The value of type Nullable&lt;Double&gt; in C# or Nullable(Of Double) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static double? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        {
            return source.Select<TSource, double?>(selector).Max();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the maximum nullable <see cref="T:System.Int32" /> value.</summary>
        /// <returns>The value of type Nullable&lt;Int32&gt; in C# or Nullable(Of Int32) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static int? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        {
            return source.Select<TSource, int?>(selector).Max();
        }

        /// <summary>Invokes a transform function on each element of a generic sequence and returns the maximum resulting value.</summary>
        /// <returns>The maximum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static TResult Max<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select<TSource, TResult>(selector).Max<TResult>();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the maximum nullable <see cref="T:System.Single" /> value.</summary>
        /// <returns>The value of type Nullable&lt;Single&gt; in C# or Nullable(Of Single) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static float? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        {
            return source.Select<TSource, float?>(selector).Max();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the maximum <see cref="T:System.Single" /> value.</summary>
        /// <returns>The maximum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static float Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            return source.Select<TSource, float>(selector).Max();
        }

        /// <summary>Returns the minimum value in a sequence of <see cref="T:System.Decimal" /> values.</summary>
        /// <returns>The minimum value in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Decimal" /> values to determine the minimum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static decimal Min(this IEnumerable<decimal> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            decimal num = 0M;
            bool flag = false;
            foreach (decimal num2 in source)
            {
                if (flag)
                {
                    if (num2 < num)
                    {
                        num = num2;
                    }
                }
                else
                {
                    num = num2;
                    flag = true;
                }
            }
            if (!flag)
            {
                throw Error.NoElements();
            }
            return num;
        }

        /// <summary>Returns the minimum value in a sequence of <see cref="T:System.Double" /> values.</summary>
        /// <returns>The minimum value in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Double" /> values to determine the minimum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static double Min(this IEnumerable<double> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            double num = 0.0;
            bool flag = false;
            foreach (double num2 in source)
            {
                if (flag)
                {
                    if ((num2 < num) || double.IsNaN(num2))
                    {
                        num = num2;
                    }
                    continue;
                }
                num = num2;
                flag = true;
            }
            if (!flag)
            {
                throw Error.NoElements();
            }
            return num;
        }

        /// <summary>Returns the minimum value in a sequence of <see cref="T:System.Int32" /> values.</summary>
        /// <returns>The minimum value in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Int32" /> values to determine the minimum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static int Min(this IEnumerable<int> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            int num = 0;
            bool flag = false;
            foreach (int num2 in source)
            {
                if (flag)
                {
                    if (num2 < num)
                    {
                        num = num2;
                    }
                }
                else
                {
                    num = num2;
                    flag = true;
                }
            }
            if (!flag)
            {
                throw Error.NoElements();
            }
            return num;
        }

        /// <summary>Returns the minimum value in a sequence of nullable <see cref="T:System.Decimal" /> values.</summary>
        /// <returns>A value of type Nullable&lt;Decimal&gt; in C# or Nullable(Of Decimal) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Decimal" /> values to determine the minimum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static decimal? Min(this IEnumerable<decimal?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            decimal? nullable = null;
            foreach (decimal? nullable2 in source)
            {
                if (nullable.HasValue)
                {
                    decimal? nullable3 = nullable2;
                    decimal? nullable4 = nullable;
                    if ((nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault()) ||
                        !(nullable3.HasValue & nullable4.HasValue))
                    {
                        continue;
                    }
                }
                nullable = nullable2;
            }
            return nullable;
        }

        /// <summary>Returns the minimum value in a generic sequence.</summary>
        /// <returns>The minimum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static TSource Min<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            Comparer<TSource> comparer = Comparer<TSource>.Default;
            TSource y = default(TSource);
            if (y == null)
            {
                foreach (TSource local2 in source)
                {
                    if ((local2 != null) && ((y == null) || (comparer.Compare(local2, y) < 0)))
                    {
                        y = local2;
                    }
                }
                return y;
            }
            bool flag = false;
            foreach (TSource local3 in source)
            {
                if (flag)
                {
                    if (comparer.Compare(local3, y) < 0)
                    {
                        y = local3;
                    }
                }
                else
                {
                    y = local3;
                    flag = true;
                }
            }
            if (!flag)
            {
                throw Error.NoElements();
            }
            return y;
        }

        /// <summary>Returns the minimum value in a sequence of nullable <see cref="T:System.Double" /> values.</summary>
        /// <returns>A value of type Nullable&lt;Double&gt; in C# or Nullable(Of Double) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Double" /> values to determine the minimum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static double? Min(this IEnumerable<double?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            double? nullable = null;
            foreach (double? nullable2 in source)
            {
                if (nullable2.HasValue)
                {
                    if (nullable.HasValue)
                    {
                        double? nullable3 = nullable2;
                        double? nullable4 = nullable;
                        if (((nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault()) ||
                             !(nullable3.HasValue & nullable4.HasValue)) && !double.IsNaN(nullable2.Value))
                        {
                            continue;
                        }
                    }
                    nullable = nullable2;
                }
            }
            return nullable;
        }

        /// <summary>Returns the minimum value in a sequence of nullable <see cref="T:System.Int32" /> values.</summary>
        /// <returns>A value of type Nullable&lt;Int32&gt; in C# or Nullable(Of Int32) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Int32" /> values to determine the minimum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static int? Min(this IEnumerable<int?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            int? nullable = null;
            foreach (int? nullable2 in source)
            {
                if (nullable.HasValue)
                {
                    int? nullable3 = nullable2;
                    int? nullable4 = nullable;
                    if ((nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault()) ||
                        !(nullable3.HasValue & nullable4.HasValue))
                    {
                        continue;
                    }
                }
                nullable = nullable2;
            }
            return nullable;
        }

        /// <summary>Returns the minimum value in a sequence of <see cref="T:System.Int64" /> values.</summary>
        /// <returns>The minimum value in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Int64" /> values to determine the minimum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static long Min(this IEnumerable<long> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            long num = 0L;
            bool flag = false;
            foreach (long num2 in source)
            {
                if (flag)
                {
                    if (num2 < num)
                    {
                        num = num2;
                    }
                }
                else
                {
                    num = num2;
                    flag = true;
                }
            }
            if (!flag)
            {
                throw Error.NoElements();
            }
            return num;
        }

        /// <summary>Returns the minimum value in a sequence of nullable <see cref="T:System.Int64" /> values.</summary>
        /// <returns>A value of type Nullable&lt;Int64&gt; in C# or Nullable(Of Int64) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Int64" /> values to determine the minimum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static long? Min(this IEnumerable<long?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            long? nullable = null;
            foreach (long? nullable2 in source)
            {
                if (nullable.HasValue)
                {
                    long? nullable3 = nullable2;
                    long? nullable4 = nullable;
                    if ((nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault()) ||
                        !(nullable3.HasValue & nullable4.HasValue))
                    {
                        continue;
                    }
                }
                nullable = nullable2;
            }
            return nullable;
        }

        /// <summary>Returns the minimum value in a sequence of nullable <see cref="T:System.Single" /> values.</summary>
        /// <returns>A value of type Nullable&lt;Single&gt; in C# or Nullable(Of Single) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Single" /> values to determine the minimum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static float? Min(this IEnumerable<float?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            float? nullable = null;
            foreach (float? nullable2 in source)
            {
                if (nullable2.HasValue)
                {
                    if (nullable.HasValue)
                    {
                        float? nullable3 = nullable2;
                        float? nullable4 = nullable;
                        if (((nullable3.GetValueOrDefault() >= nullable4.GetValueOrDefault()) ||
                             !(nullable3.HasValue & nullable4.HasValue)) && !float.IsNaN(nullable2.Value))
                        {
                            continue;
                        }
                    }
                    nullable = nullable2;
                }
            }
            return nullable;
        }

        /// <summary>Returns the minimum value in a sequence of <see cref="T:System.Single" /> values.</summary>
        /// <returns>The minimum value in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Single" /> values to determine the minimum value of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static float Min(this IEnumerable<float> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            float num = 0f;
            bool flag = false;
            foreach (float num2 in source)
            {
                if (flag)
                {
                    if ((num2 < num) || float.IsNaN(num2))
                    {
                        num = num2;
                    }
                    continue;
                }
                num = num2;
                flag = true;
            }
            if (!flag)
            {
                throw Error.NoElements();
            }
            return num;
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the minimum nullable <see cref="T:System.Double" /> value.</summary>
        /// <returns>The value of type Nullable&lt;Double&gt; in C# or Nullable(Of Double) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static double? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        {
            return source.Select<TSource, double?>(selector).Min();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the minimum nullable <see cref="T:System.Int32" /> value.</summary>
        /// <returns>The value of type Nullable&lt;Int32&gt; in C# or Nullable(Of Int32) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static int? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        {
            return source.Select<TSource, int?>(selector).Min();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the minimum <see cref="T:System.Decimal" /> value.</summary>
        /// <returns>The minimum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static decimal Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            return source.Select<TSource, decimal>(selector).Min();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the minimum nullable <see cref="T:System.Decimal" /> value.</summary>
        /// <returns>The value of type Nullable&lt;Decimal&gt; in C# or Nullable(Of Decimal) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static decimal? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            return source.Select<TSource, decimal?>(selector).Min();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the minimum <see cref="T:System.Double" /> value.</summary>
        /// <returns>The minimum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static double Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            return source.Select<TSource, double>(selector).Min();
        }

        /// <summary>Invokes a transform function on each element of a generic sequence and returns the minimum resulting value.</summary>
        /// <returns>The minimum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static TResult Min<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select<TSource, TResult>(selector).Min<TResult>();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the minimum nullable <see cref="T:System.Int64" /> value.</summary>
        /// <returns>The value of type Nullable&lt;Int64&gt; in C# or Nullable(Of Int64) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static long? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        {
            return source.Select<TSource, long?>(selector).Min();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the minimum <see cref="T:System.Int32" /> value.</summary>
        /// <returns>The minimum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static int Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return source.Select<TSource, int>(selector).Min();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the minimum nullable <see cref="T:System.Single" /> value.</summary>
        /// <returns>The value of type Nullable&lt;Single&gt; in C# or Nullable(Of Single) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static float? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        {
            return source.Select<TSource, float?>(selector).Min();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the minimum <see cref="T:System.Int64" /> value.</summary>
        /// <returns>The minimum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static long Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            return source.Select<TSource, long>(selector).Min();
        }

        /// <summary>Invokes a transform function on each element of a sequence and returns the minimum <see cref="T:System.Single" /> value.</summary>
        /// <returns>The minimum value in the sequence.</returns>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// <paramref name="source" /> contains no elements.</exception>
        public static float Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            return source.Select<TSource, float>(selector).Min();
        }

        /// <summary>Filters the elements of an <see cref="T:System.Collections.IEnumerable" /> based on a specified type.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements from the input sequence of type <paramref name="TResult" />.</returns>
        /// <param name="source">The <see cref="T:System.Collections.IEnumerable" /> whose elements to filter.</param>
        /// <typeparam name="TResult">The type to filter the elements of the sequence on.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            return OfTypeIterator<TResult>(source);
        }

        private static IEnumerable<TResult> OfTypeIterator<TResult>(IEnumerable source)
        {
        <
            OfTypeIterator > d__a3 < TResult > _a = new < OfTypeIterator > d__a3<TResult>(-2);
            _a.<>
            3
            __source = source;
            return _a;
        }

        /// <summary>Sorts the elements of a sequence in ascending order according to a key.</summary>
        /// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted according to a key.</returns>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source,
                                                                         Func<TSource, TKey> keySelector)
        {
            return new OrderedEnumerable<TSource, TKey>(source, keySelector, null, false);
        }

        /// <summary>Sorts the elements of a sequence in ascending order by using a specified comparer.</summary>
        /// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted according to a key.</returns>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source,
                                                                         Func<TSource, TKey> keySelector,
                                                                         IComparer<TKey> comparer)
        {
            return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, false);
        }

        /// <summary>Sorts the elements of a sequence in descending order according to a key.</summary>
        /// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted in descending order according to a key.</returns>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source,
                                                                                   Func<TSource, TKey> keySelector)
        {
            return new OrderedEnumerable<TSource, TKey>(source, keySelector, null, true);
        }

        /// <summary>Sorts the elements of a sequence in descending order by using a specified comparer.</summary>
        /// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted in descending order according to a key.</returns>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source,
                                                                                   Func<TSource, TKey> keySelector,
                                                                                   IComparer<TKey> comparer)
        {
            return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, true);
        }

        /// <summary>Generates a sequence of integral numbers within a specified range.</summary>
        /// <returns>An IEnumerable&lt;Int32&gt; in C# or IEnumerable(Of Int32) in Visual Basic that contains a range of sequential integral numbers.</returns>
        /// <param name="start">The value of the first integer in the sequence.</param>
        /// <param name="count">The number of sequential integers to generate.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="count" /> is less than 0.-or-<paramref name="start" /> + <paramref name="count" /> -1 is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
        public static IEnumerable<int> Range(int start, int count)
        {
            long num = (start + count) - 1L;
            if ((count < 0) || (num > 0x7fffffffL))
            {
                throw Error.ArgumentOutOfRange("count");
            }
            return RangeIterator(start, count);
        }

        private static IEnumerable<int> RangeIterator(int start, int count)
        {
        <
            RangeIterator > d__b1
            _b = new < RangeIterator > d__b1(-2);
            _b.<>
            3
            __start = start;
            _b.<>
            3
            __count = count;
            return _b;
        }

        /// <summary>Generates a sequence that contains one repeated value.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains a repeated value.</returns>
        /// <param name="element">The value to be repeated.</param>
        /// <param name="count">The number of times to repeat the value in the generated sequence.</param>
        /// <typeparam name="TResult">The type of the value to be repeated in the result sequence.</typeparam>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="count" /> is less than 0.</exception>
        public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
        {
            if (count < 0)
            {
                throw Error.ArgumentOutOfRange("count");
            }
            return RepeatIterator<TResult>(element, count);
        }

        private static IEnumerable<TResult> RepeatIterator<TResult>(TResult element, int count)
        {
        <
            RepeatIterator > d__b5 < TResult > _b = new < RepeatIterator > d__b5<TResult>(-2);
            _b.<>
            3
            __element = element;
            _b.<>
            3
            __count = count;
            return _b;
        }

        /// <summary>Inverts the order of the elements in a sequence.</summary>
        /// <returns>A sequence whose elements correspond to those of the input sequence in reverse order.</returns>
        /// <param name="source">A sequence of values to reverse.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            return ReverseIterator<TSource>(source);
        }

        private static IEnumerable<TSource> ReverseIterator<TSource>(IEnumerable<TSource> source)
        {
        <
            ReverseIterator > d__99 < TSource > d__ = new < ReverseIterator > d__99<TSource>(-2);
            d__.<>
            3
            __source = source;
            return d__;
        }

        /// <summary>Projects each element of a sequence into a new form.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the transform function on each element of <paramref name="source" />.</returns>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source,
                                                                    Func<TSource, TResult> selector)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (selector == null)
            {
                throw Error.ArgumentNull("selector");
            }
            if (source is Iterator<TSource>)
            {
                return ((Iterator<TSource>) source).Select<TResult>(selector);
            }
            if (source is TSource[])
            {
                return new WhereSelectArrayIterator<TSource, TResult>((TSource[]) source, null, selector);
            }
            if (source is List<TSource>)
            {
                return new WhereSelectListIterator<TSource, TResult>((List<TSource>) source, null, selector);
            }
            return new WhereSelectEnumerableIterator<TSource, TResult>(source, null, selector);
        }

        /// <summary>Projects each element of a sequence into a new form by incorporating the element's index.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the transform function on each element of <paramref name="source" />.</returns>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="selector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source,
                                                                    Func<TSource, int, TResult> selector)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (selector == null)
            {
                throw Error.ArgumentNull("selector");
            }
            return SelectIterator<TSource, TResult>(source, selector);
        }

        private static IEnumerable<TResult> SelectIterator<TSource, TResult>(IEnumerable<TSource> source,
                                                                             Func<TSource, int, TResult> selector)
        {
        <
            SelectIterator > d__7 < TSource,
            TResult > d__ = new < SelectIterator > d__7<TSource, TResult>(-2);
            d__.<>
            3
            __source = source;
            d__.<>
            3
            __selector = selector;
            return d__;
        }

        /// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" /> and flattens the resulting sequences into one sequence.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the sequence returned by <paramref name="selector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source,
                                                                        Func<TSource, IEnumerable<TResult>> selector)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (selector == null)
            {
                throw Error.ArgumentNull("selector");
            }
            return SelectManyIterator<TSource, TResult>(source, selector);
        }

        /// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />, and flattens the resulting sequences into one sequence. The index of each source element is used in the projected form of that element.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the one-to-many transform function on each element of an input sequence.</returns>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the sequence returned by <paramref name="selector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source,
                                                                        Func<TSource, int, IEnumerable<TResult>>
                                                                            selector)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (selector == null)
            {
                throw Error.ArgumentNull("selector");
            }
            return SelectManyIterator<TSource, TResult>(source, selector);
        }

        /// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />, flattens the resulting sequences into one sequence, and invokes a result selector function on each element therein.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the one-to-many transform function <paramref name="collectionSelector" /> on each element of <paramref name="source" /> and then mapping each of those sequence elements and their corresponding source element to a result element.</returns>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="collectionSelector">A transform function to apply to each element of the input sequence.</param>
        /// <param name="resultSelector">A transform function to apply to each element of the intermediate sequence.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TCollection">The type of the intermediate elements collected by <paramref name="collectionSelector" />.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the resulting sequence.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="collectionSelector" /> or <paramref name="resultSelector" /> is null.</exception>
        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source,
                                                                                     Func
                                                                                         <TSource,
                                                                                         IEnumerable<TCollection>>
                                                                                         collectionSelector,
                                                                                     Func<TSource, TCollection, TResult>
                                                                                         resultSelector)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (collectionSelector == null)
            {
                throw Error.ArgumentNull("collectionSelector");
            }
            if (resultSelector == null)
            {
                throw Error.ArgumentNull("resultSelector");
            }
            return SelectManyIterator<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }

        /// <summary>Projects each element of a sequence to an <see cref="T:System.Collections.Generic.IEnumerable`1" />, flattens the resulting sequences into one sequence, and invokes a result selector function on each element therein. The index of each source element is used in the intermediate projected form of that element.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose elements are the result of invoking the one-to-many transform function <paramref name="collectionSelector" /> on each element of <paramref name="source" /> and then mapping each of those sequence elements and their corresponding source element to a result element.</returns>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="collectionSelector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
        /// <param name="resultSelector">A transform function to apply to each element of the intermediate sequence.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TCollection">The type of the intermediate elements collected by <paramref name="collectionSelector" />.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the resulting sequence.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="collectionSelector" /> or <paramref name="resultSelector" /> is null.</exception>
        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source,
                                                                                     Func
                                                                                         <TSource, int,
                                                                                         IEnumerable<TCollection>>
                                                                                         collectionSelector,
                                                                                     Func<TSource, TCollection, TResult>
                                                                                         resultSelector)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (collectionSelector == null)
            {
                throw Error.ArgumentNull("collectionSelector");
            }
            if (resultSelector == null)
            {
                throw Error.ArgumentNull("resultSelector");
            }
            return SelectManyIterator<TSource, TCollection, TResult>(source, collectionSelector, resultSelector);
        }

        private static IEnumerable<TResult> SelectManyIterator<TSource, TResult>(IEnumerable<TSource> source,
                                                                                 Func<TSource, IEnumerable<TResult>>
                                                                                     selector)
        {
        <
            SelectManyIterator > d__14 < TSource,
            TResult > d__ = new < SelectManyIterator > d__14<TSource, TResult>(-2);
            d__.<>
            3
            __source = source;
            d__.<>
            3
            __selector = selector;
            return d__;
        }

        private static IEnumerable<TResult> SelectManyIterator<TSource, TResult>(IEnumerable<TSource> source,
                                                                                 Func
                                                                                     <TSource, int, IEnumerable<TResult>
                                                                                     > selector)
        {
        <
            SelectManyIterator > d__1d < TSource,
            TResult > d__d = new < SelectManyIterator > d__1d<TSource, TResult>(-2);
            d__d.<>
            3
            __source = source;
            d__d.<>
            3
            __selector = selector;
            return d__d;
        }

        private static IEnumerable<TResult> SelectManyIterator<TSource, TCollection, TResult>(
            IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
        <
            SelectManyIterator > d__31 < TSource,
            TCollection,
            TResult > d__ = new < SelectManyIterator > d__31<TSource, TCollection, TResult>(-2);
            d__.<>
            3
            __source = source;
            d__.<>
            3
            __collectionSelector = collectionSelector;
            d__.<>
            3
            __resultSelector = resultSelector;
            return d__;
        }

        private static IEnumerable<TResult> SelectManyIterator<TSource, TCollection, TResult>(
            IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
        <
            SelectManyIterator > d__27 < TSource,
            TCollection,
            TResult > d__ = new < SelectManyIterator > d__27<TSource, TCollection, TResult>(-2);
            d__.<>
            3
            __source = source;
            d__.<>
            3
            __collectionSelector = collectionSelector;
            d__.<>
            3
            __resultSelector = resultSelector;
            return d__;
        }

        /// <summary>Determines whether two sequences are equal by comparing the elements by using the default equality comparer for their type.</summary>
        /// <returns>true if the two source sequences are of equal length and their corresponding elements are equal according to the default equality comparer for their type; otherwise, false.</returns>
        /// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to <paramref name="second" />.</param>
        /// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to the first sequence.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="first" /> or <paramref name="second" /> is null.</exception>
        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return first.SequenceEqual<TSource>(second, null);
        }

        /// <summary>Determines whether two sequences are equal by comparing their elements by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
        /// <returns>true if the two source sequences are of equal length and their corresponding elements compare equal according to <paramref name="comparer" />; otherwise, false.</returns>
        /// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to <paramref name="second" />.</param>
        /// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to compare to the first sequence.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to use to compare elements.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="first" /> or <paramref name="second" /> is null.</exception>
        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second,
                                                  IEqualityComparer<TSource> comparer)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TSource>.Default;
            }
            if (first == null)
            {
                throw Error.ArgumentNull("first");
            }
            if (second == null)
            {
                throw Error.ArgumentNull("second");
            }
            using (IEnumerator<TSource> enumerator = first.GetEnumerator())
            {
                using (IEnumerator<TSource> enumerator2 = second.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        if (!enumerator2.MoveNext() || !comparer.Equals(enumerator.Current, enumerator2.Current))
                        {
                            return false;
                        }
                    }
                    if (enumerator2.MoveNext())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.</summary>
        /// <returns>The single element of the input sequence.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the single element of.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">The input sequence contains more than one element.-or-The input sequence is empty.</exception>
        public static TSource Single<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                switch (list.Count)
                {
                    case 0:
                        throw Error.NoElements();

                    case 1:
                        return list[0];
                }
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    if (!enumerator.MoveNext())
                    {
                        throw Error.NoElements();
                    }
                    TSource current = enumerator.Current;
                    if (!enumerator.MoveNext())
                    {
                        return current;
                    }
                }
            }
            throw Error.MoreThanOneElement();
        }

        /// <summary>Returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.</summary>
        /// <returns>The single element of the input sequence that satisfies a condition.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return a single element from.</param>
        /// <param name="predicate">A function to test an element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate" />.-or-More than one element satisfies the condition in <paramref name="predicate" />.-or-The source sequence is empty.</exception>
        public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            TSource local = default(TSource);
            long num = 0L;
            foreach (TSource local2 in source)
            {
                if (predicate(local2))
                {
                    local = local2;
                    num += 1L;
                }
            }
            long num2 = num;
            if ((num2 <= 1L) && (num2 >= 0L))
            {
                switch (((int) num2))
                {
                    case 0:
                        throw Error.NoMatch();

                    case 1:
                        return local;
                }
            }
            throw Error.MoreThanOneMatch();
        }

        /// <summary>Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.</summary>
        /// <returns>The single element of the input sequence, or default(<paramref name="TSource" />) if the sequence contains no elements.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return the single element of.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">The input sequence contains more than one element.</exception>
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                switch (list.Count)
                {
                    case 0:
                        return default(TSource);

                    case 1:
                        return list[0];
                }
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    if (!enumerator.MoveNext())
                    {
                        return default(TSource);
                    }
                    TSource current = enumerator.Current;
                    if (!enumerator.MoveNext())
                    {
                        return current;
                    }
                }
            }
            throw Error.MoreThanOneElement();
        }

        /// <summary>Returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists; this method throws an exception if more than one element satisfies the condition.</summary>
        /// <returns>The single element of the input sequence that satisfies the condition, or default(<paramref name="TSource" />) if no such element is found.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return a single element from.</param>
        /// <param name="predicate">A function to test an element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        /// <exception cref="T:System.InvalidOperationException">More than one element satisfies the condition in <paramref name="predicate" />.</exception>
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            TSource local = default(TSource);
            long num = 0L;
            foreach (TSource local2 in source)
            {
                if (predicate(local2))
                {
                    local = local2;
                    num += 1L;
                }
            }
            long num2 = num;
            if ((num2 <= 1L) && (num2 >= 0L))
            {
                switch (((int) num2))
                {
                    case 0:
                        return default(TSource);

                    case 1:
                        return local;
                }
            }
            throw Error.MoreThanOneMatch();
        }

        /// <summary>Bypasses a specified number of elements in a sequence and then returns the remaining elements.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements that occur after the specified index in the input sequence.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            return SkipIterator<TSource>(source, count);
        }

        private static IEnumerable<TSource> SkipIterator<TSource>(IEnumerable<TSource> source, int count)
        {
        <
            SkipIterator > d__4d < TSource > d__d = new < SkipIterator > d__4d<TSource>(-2);
            d__d.<>
            3
            __source = source;
            d__d.<>
            3
            __count = count;
            return d__d;
        }

        /// <summary>Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by <paramref name="predicate" />.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source,
                                                              Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            return SkipWhileIterator<TSource>(source, predicate);
        }

        /// <summary>Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements. The element's index is used in the logic of the predicate function.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by <paramref name="predicate" />.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to return elements from.</param>
        /// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source,
                                                              Func<TSource, int, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            return SkipWhileIterator<TSource>(source, predicate);
        }

        private static IEnumerable<TSource> SkipWhileIterator<TSource>(IEnumerable<TSource> source,
                                                                       Func<TSource, bool> predicate)
        {
        <
            SkipWhileIterator > d__52 < TSource > d__ = new < SkipWhileIterator > d__52<TSource>(-2);
            d__.<>
            3
            __source = source;
            d__.<>
            3
            __predicate = predicate;
            return d__;
        }

        private static IEnumerable<TSource> SkipWhileIterator<TSource>(IEnumerable<TSource> source,
                                                                       Func<TSource, int, bool> predicate)
        {
        <
            SkipWhileIterator > d__59 < TSource > d__ = new < SkipWhileIterator > d__59<TSource>(-2);
            d__.<>
            3
            __source = source;
            d__.<>
            3
            __predicate = predicate;
            return d__;
        }

        /// <summary>Computes the sum of a sequence of <see cref="T:System.Decimal" /> values.</summary>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Decimal" /> values to calculate the sum of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
        public static decimal Sum(this IEnumerable<decimal> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            decimal num = 0M;
            foreach (decimal num2 in source)
            {
                num += num2;
            }
            return num;
        }

        /// <summary>Computes the sum of a sequence of <see cref="T:System.Double" /> values.</summary>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Double" /> values to calculate the sum of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static double Sum(this IEnumerable<double> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            double num = 0.0;
            foreach (double num2 in source)
            {
                num += num2;
            }
            return num;
        }

        /// <summary>Computes the sum of a sequence of <see cref="T:System.Int32" /> values.</summary>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Int32" /> values to calculate the sum of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
        public static int Sum(this IEnumerable<int> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            int num = 0;
            foreach (int num2 in source)
            {
                num += num2;
            }
            return num;
        }

        /// <summary>Computes the sum of a sequence of <see cref="T:System.Int64" /> values.</summary>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Int64" /> values to calculate the sum of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        public static long Sum(this IEnumerable<long> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            long num = 0L;
            foreach (long num2 in source)
            {
                num += num2;
            }
            return num;
        }

        /// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Decimal" /> values.</summary>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Decimal" /> values to calculate the sum of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
        public static decimal? Sum(this IEnumerable<decimal?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            decimal num = 0M;
            foreach (decimal? nullable in source)
            {
                if (nullable.HasValue)
                {
                    num += nullable.GetValueOrDefault();
                }
            }
            return new decimal?(num);
        }

        /// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Double" /> values.</summary>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Double" /> values to calculate the sum of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static double? Sum(this IEnumerable<double?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            double num = 0.0;
            foreach (double? nullable in source)
            {
                if (nullable.HasValue)
                {
                    num += nullable.GetValueOrDefault();
                }
            }
            return new double?(num);
        }

        /// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Int32" /> values.</summary>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Int32" /> values to calculate the sum of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
        public static int? Sum(this IEnumerable<int?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            int num = 0;
            foreach (int? nullable in source)
            {
                if (nullable.HasValue)
                {
                    num += nullable.GetValueOrDefault();
                }
            }
            return new int?(num);
        }

        /// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Int64" /> values.</summary>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Int64" /> values to calculate the sum of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        public static long? Sum(this IEnumerable<long?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            long num = 0L;
            foreach (long? nullable in source)
            {
                if (nullable.HasValue)
                {
                    num += nullable.GetValueOrDefault();
                }
            }
            return new long?(num);
        }

        /// <summary>Computes the sum of a sequence of nullable <see cref="T:System.Single" /> values.</summary>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <param name="source">A sequence of nullable <see cref="T:System.Single" /> values to calculate the sum of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static float? Sum(this IEnumerable<float?> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            double num = 0.0;
            foreach (float? nullable in source)
            {
                if (nullable.HasValue)
                {
                    num += (double) nullable.GetValueOrDefault();
                }
            }
            return new float?((float) num);
        }

        /// <summary>Computes the sum of a sequence of <see cref="T:System.Single" /> values.</summary>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <param name="source">A sequence of <see cref="T:System.Single" /> values to calculate the sum of.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static float Sum(this IEnumerable<float> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            double num = 0.0;
            foreach (float num2 in source)
            {
                num += num2;
            }
            return (float) num;
        }

        /// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Decimal" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The sum of the projected values.</returns>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
        public static decimal? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            return source.Select<TSource, decimal?>(selector).Sum();
        }

        /// <summary>Computes the sum of the sequence of <see cref="T:System.Decimal" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The sum of the projected values.</returns>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Decimal.MaxValue" />.</exception>
        public static decimal Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            return source.Select<TSource, decimal>(selector).Sum();
        }

        /// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Double" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The sum of the projected values.</returns>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static double? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        {
            return source.Select<TSource, double?>(selector).Sum();
        }

        /// <summary>Computes the sum of the sequence of <see cref="T:System.Double" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The sum of the projected values.</returns>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static double Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            return source.Select<TSource, double>(selector).Sum();
        }

        /// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Int32" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The sum of the projected values.</returns>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
        public static int? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        {
            return source.Select<TSource, int?>(selector).Sum();
        }

        /// <summary>Computes the sum of the sequence of <see cref="T:System.Int32" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The sum of the projected values.</returns>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
        public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return source.Select<TSource, int>(selector).Sum();
        }

        /// <summary>Computes the sum of the sequence of <see cref="T:System.Int64" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The sum of the projected values.</returns>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        public static long Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            return source.Select<TSource, long>(selector).Sum();
        }

        /// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Int64" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The sum of the projected values.</returns>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="F:System.Int64.MaxValue" />.</exception>
        public static long? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        {
            return source.Select<TSource, long?>(selector).Sum();
        }

        /// <summary>Computes the sum of the sequence of nullable <see cref="T:System.Single" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The sum of the projected values.</returns>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static float? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        {
            return source.Select<TSource, float?>(selector).Sum();
        }

        /// <summary>Computes the sum of the sequence of <see cref="T:System.Single" /> values that are obtained by invoking a transform function on each element of the input sequence.</summary>
        /// <returns>The sum of the projected values.</returns>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="selector" /> is null.</exception>
        public static float Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            return source.Select<TSource, float>(selector).Sum();
        }

        /// <summary>Returns a specified number of contiguous elements from the start of a sequence.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the specified number of elements from the start of the input sequence.</returns>
        /// <param name="source">The sequence to return elements from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            return TakeIterator<TSource>(source, count);
        }

        private static IEnumerable<TSource> TakeIterator<TSource>(IEnumerable<TSource> source, int count)
        {
        <
            TakeIterator > d__3a < TSource > d__a = new < TakeIterator > d__3a<TSource>(-2);
            d__a.<>
            3
            __source = source;
            d__a.<>
            3
            __count = count;
            return d__a;
        }

        /// <summary>Returns elements from a sequence as long as a specified condition is true.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements from the input sequence that occur before the element at which the test no longer passes.</returns>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source,
                                                              Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            return TakeWhileIterator<TSource>(source, predicate);
        }

        /// <summary>Returns elements from a sequence as long as a specified condition is true. The element's index is used in the logic of the predicate function.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements from the input sequence that occur before the element at which the test no longer passes.</returns>
        /// <param name="source">The sequence to return elements from.</param>
        /// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source,
                                                              Func<TSource, int, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            return TakeWhileIterator<TSource>(source, predicate);
        }

        private static IEnumerable<TSource> TakeWhileIterator<TSource>(IEnumerable<TSource> source,
                                                                       Func<TSource, bool> predicate)
        {
        <
            TakeWhileIterator > d__40 < TSource > d__ = new < TakeWhileIterator > d__40<TSource>(-2);
            d__.<>
            3
            __source = source;
            d__.<>
            3
            __predicate = predicate;
            return d__;
        }

        private static IEnumerable<TSource> TakeWhileIterator<TSource>(IEnumerable<TSource> source,
                                                                       Func<TSource, int, bool> predicate)
        {
        <
            TakeWhileIterator > d__46 < TSource > d__ = new < TakeWhileIterator > d__46<TSource>(-2);
            d__.<>
            3
            __source = source;
            d__.<>
            3
            __predicate = predicate;
            return d__;
        }

        /// <summary>Performs a subsequent ordering of the elements in a sequence in ascending order according to a key.</summary>
        /// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted according to a key.</returns>
        /// <param name="source">An <see cref="T:System.Linq.IOrderedEnumerable`1" /> that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source,
                                                                        Func<TSource, TKey> keySelector)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            return source.CreateOrderedEnumerable<TKey>(keySelector, null, false);
        }

        /// <summary>Performs a subsequent ordering of the elements in a sequence in ascending order by using a specified comparer.</summary>
        /// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted according to a key.</returns>
        /// <param name="source">An <see cref="T:System.Linq.IOrderedEnumerable`1" /> that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source,
                                                                        Func<TSource, TKey> keySelector,
                                                                        IComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            return source.CreateOrderedEnumerable<TKey>(keySelector, comparer, false);
        }

        /// <summary>Performs a subsequent ordering of the elements in a sequence in descending order, according to a key.</summary>
        /// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted in descending order according to a key.</returns>
        /// <param name="source">An <see cref="T:System.Linq.IOrderedEnumerable`1" /> that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
        public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(
            this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            return source.CreateOrderedEnumerable<TKey>(keySelector, null, true);
        }

        /// <summary>Performs a subsequent ordering of the elements in a sequence in descending order by using a specified comparer.</summary>
        /// <returns>An <see cref="T:System.Linq.IOrderedEnumerable`1" /> whose elements are sorted in descending order according to a key.</returns>
        /// <param name="source">An <see cref="T:System.Linq.IOrderedEnumerable`1" /> that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IComparer`1" /> to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
        public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(
            this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            return source.CreateOrderedEnumerable<TKey>(keySelector, comparer, true);
        }

        /// <summary>Creates an array from a <see cref="T:System.Collections.Generic.IEnumerable`1" />.</summary>
        /// <returns>An array that contains the elements from the input sequence.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create an array from.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            Buffer<TSource> buffer = new Buffer<TSource>(source);
            return buffer.ToArray();
        }

        /// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function.</summary>
        /// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains keys and values.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.-or-<paramref name="keySelector" /> produces a key that is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="keySelector" /> produces duplicate keys for two elements.</exception>
        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source,
                                                                            Func<TSource, TKey> keySelector)
        {
            return source.ToDictionary<TSource, TKey, TSource>(keySelector, IdentityFunction<TSource>.Instance, null);
        }

        /// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function and key comparer.</summary>
        /// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains keys and values.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the keys returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.-or-<paramref name="keySelector" /> produces a key that is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="keySelector" /> produces duplicate keys for two elements.</exception>
        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source,
                                                                            Func<TSource, TKey> keySelector,
                                                                            IEqualityComparer<TKey> comparer)
        {
            return source.ToDictionary<TSource, TKey, TSource>(keySelector, IdentityFunction<TSource>.Instance, comparer);
        }

        /// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to specified key selector and element selector functions.</summary>
        /// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.-or-<paramref name="keySelector" /> produces a key that is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="keySelector" /> produces duplicate keys for two elements.</exception>
        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source,
                                                                                       Func<TSource, TKey> keySelector,
                                                                                       Func<TSource, TElement>
                                                                                           elementSelector)
        {
            return source.ToDictionary<TSource, TKey, TElement>(keySelector, elementSelector, null);
        }

        /// <summary>Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function, a comparer, and an element selector function.</summary>
        /// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.Dictionary`2" /> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.-or-<paramref name="keySelector" /> produces a key that is null.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="keySelector" /> produces duplicate keys for two elements.</exception>
        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source,
                                                                                       Func<TSource, TKey> keySelector,
                                                                                       Func<TSource, TElement>
                                                                                           elementSelector,
                                                                                       IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (keySelector == null)
            {
                throw Error.ArgumentNull("keySelector");
            }
            if (elementSelector == null)
            {
                throw Error.ArgumentNull("elementSelector");
            }
            Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(comparer);
            foreach (TSource local in source)
            {
                dictionary.Add(keySelector(local), elementSelector(local));
            }
            return dictionary;
        }

        /// <summary>Creates a <see cref="T:System.Collections.Generic.List`1" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" />.</summary>
        /// <returns>A <see cref="T:System.Collections.Generic.List`1" /> that contains elements from the input sequence.</returns>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Collections.Generic.List`1" /> from.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> is null.</exception>
        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            return new List<TSource>(source);
        }

        /// <summary>Creates a <see cref="T:System.Linq.Lookup`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function.</summary>
        /// <returns>A <see cref="T:System.Linq.Lookup`2" /> that contains keys and values.</returns>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Linq.Lookup`2" /> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
        public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source,
                                                                     Func<TSource, TKey> keySelector)
        {
            return
                (ILookup<TKey, TSource>)
                Lookup<TKey, TSource>.Create<TSource>(source, keySelector, IdentityFunction<TSource>.Instance, null);
        }

        /// <summary>Creates a <see cref="T:System.Linq.Lookup`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function and key comparer.</summary>
        /// <returns>A <see cref="T:System.Linq.Lookup`2" /> that contains keys and values.</returns>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Linq.Lookup`2" /> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> is null.</exception>
        public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source,
                                                                     Func<TSource, TKey> keySelector,
                                                                     IEqualityComparer<TKey> comparer)
        {
            return
                (ILookup<TKey, TSource>)
                Lookup<TKey, TSource>.Create<TSource>(source, keySelector, IdentityFunction<TSource>.Instance, comparer);
        }

        /// <summary>Creates a <see cref="T:System.Linq.Lookup`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to specified key selector and element selector functions.</summary>
        /// <returns>A <see cref="T:System.Linq.Lookup`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence.</returns>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Linq.Lookup`2" /> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.</exception>
        public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source,
                                                                                Func<TSource, TKey> keySelector,
                                                                                Func<TSource, TElement> elementSelector)
        {
            return
                (ILookup<TKey, TElement>)
                Lookup<TKey, TElement>.Create<TSource>(source, keySelector, elementSelector, null);
        }

        /// <summary>Creates a <see cref="T:System.Linq.Lookup`2" /> from an <see cref="T:System.Collections.Generic.IEnumerable`1" /> according to a specified key selector function, a comparer and an element selector function.</summary>
        /// <returns>A <see cref="T:System.Linq.Lookup`2" /> that contains values of type <paramref name="TElement" /> selected from the input sequence.</returns>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1" /> to create a <see cref="T:System.Linq.Lookup`2" /> from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector" />.</typeparam>
        /// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="keySelector" /> or <paramref name="elementSelector" /> is null.</exception>
        public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source,
                                                                                Func<TSource, TKey> keySelector,
                                                                                Func<TSource, TElement> elementSelector,
                                                                                IEqualityComparer<TKey> comparer)
        {
            return
                (ILookup<TKey, TElement>)
                Lookup<TKey, TElement>.Create<TSource>(source, keySelector, elementSelector, comparer);
        }

        /// <summary>Produces the set union of two sequences by using the default equality comparer.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements from both input sequences, excluding duplicates.</returns>
        /// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements form the first set for the union.</param>
        /// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements form the second set for the union.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="first" /> or <paramref name="second" /> is null.</exception>
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
            {
                throw Error.ArgumentNull("first");
            }
            if (second == null)
            {
                throw Error.ArgumentNull("second");
            }
            return UnionIterator<TSource>(first, second, null);
        }

        /// <summary>Produces the set union of two sequences by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements from both input sequences, excluding duplicates.</returns>
        /// <param name="first">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements form the first set for the union.</param>
        /// <param name="second">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> whose distinct elements form the second set for the union.</param>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare values.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="first" /> or <paramref name="second" /> is null.</exception>
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second,
                                                          IEqualityComparer<TSource> comparer)
        {
            if (first == null)
            {
                throw Error.ArgumentNull("first");
            }
            if (second == null)
            {
                throw Error.ArgumentNull("second");
            }
            return UnionIterator<TSource>(first, second, comparer);
        }

        private static IEnumerable<TSource> UnionIterator<TSource>(IEnumerable<TSource> first,
                                                                   IEnumerable<TSource> second,
                                                                   IEqualityComparer<TSource> comparer)
        {
        <
            UnionIterator > d__81 < TSource > d__ = new < UnionIterator > d__81<TSource>(-2);
            d__.<>
            3
            __first = first;
            d__.<>
            3
            __second = second;
            d__.<>
            3
            __comparer = comparer;
            return d__;
        }

        /// <summary>Filters a sequence of values based on a predicate.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements from the input sequence that satisfy the condition.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source,
                                                          Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            if (source is Iterator<TSource>)
            {
                return ((Iterator<TSource>) source).Where(predicate);
            }
            if (source is TSource[])
            {
                return new WhereArrayIterator<TSource>((TSource[]) source, predicate);
            }
            if (source is List<TSource>)
            {
                return new WhereListIterator<TSource>((List<TSource>) source, predicate);
            }
            return new WhereEnumerableIterator<TSource>(source, predicate);
        }

        /// <summary>Filters a sequence of values based on a predicate. Each element's index is used in the logic of the predicate function.</summary>
        /// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains elements from the input sequence that satisfy the condition.</returns>
        /// <param name="source">An <see cref="T:System.Collections.Generic.IEnumerable`1" /> to filter.</param>
        /// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="source" /> or <paramref name="predicate" /> is null.</exception>
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source,
                                                          Func<TSource, int, bool> predicate)
        {
            if (source == null)
            {
                throw Error.ArgumentNull("source");
            }
            if (predicate == null)
            {
                throw Error.ArgumentNull("predicate");
            }
            return WhereIterator<TSource>(source, predicate);
        }

        private static IEnumerable<TSource> WhereIterator<TSource>(IEnumerable<TSource> source,
                                                                   Func<TSource, int, bool> predicate)
        {
        <
            WhereIterator > d__0 < TSource > d__ = new < WhereIterator > d__0<TSource>(-2);
            d__.<>
            3
            __source = source;
            d__.<>
            3
            __predicate = predicate;
            return d__;
        }

        #region Nested type: Iterator

        private abstract class Iterator<TSource> : IEnumerable<TSource>, IEnumerable, IEnumerator<TSource>, IDisposable,
                                                   IEnumerator
        {
            internal TSource current;
            internal int state;
            private int threadId;

            public Iterator()
            {
                this.threadId = Thread.CurrentThread.ManagedThreadId;
            }

            #region IEnumerable<TSource> Members

            public IEnumerator<TSource> GetEnumerator()
            {
                if ((this.threadId == Thread.CurrentThread.ManagedThreadId) && (this.state == 0))
                {
                    this.state = 1;
                    return this;
                }
                Enumerable.Iterator<TSource> iterator = this.Clone();
                iterator.state = 1;
                return iterator;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            #endregion

            #region IEnumerator<TSource> Members

            public virtual void Dispose()
            {
                this.current = default(TSource);
                this.state = -1;
            }

            public abstract bool MoveNext();

            void IEnumerator.Reset()
            {
                throw new NotImplementedException();
            }

            public TSource Current
            {
                get { return this.current; }
            }

            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            #endregion

            public abstract Enumerable.Iterator<TSource> Clone();
            public abstract IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector);
            public abstract IEnumerable<TSource> Where(Func<TSource, bool> predicate);
        }

        #endregion

        #region Nested type: WhereArrayIterator

        private class WhereArrayIterator<TSource> : Enumerable.Iterator<TSource>
        {
            private int index;
            private Func<TSource, bool> predicate;
            private TSource[] source;

            public WhereArrayIterator(TSource[] source, Func<TSource, bool> predicate)
            {
                this.source = source;
                this.predicate = predicate;
            }

            public override Enumerable.Iterator<TSource> Clone()
            {
                return new Enumerable.WhereArrayIterator<TSource>(this.source, this.predicate);
            }

            public override bool MoveNext()
            {
                if (base.state == 1)
                {
                    while (this.index < this.source.Length)
                    {
                        TSource arg = this.source[this.index];
                        this.index++;
                        if (this.predicate(arg))
                        {
                            base.current = arg;
                            return true;
                        }
                    }
                    this.Dispose();
                }
                return false;
            }

            public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
            {
                return new Enumerable.WhereSelectArrayIterator<TSource, TResult>(this.source, this.predicate, selector);
            }

            public override IEnumerable<TSource> Where(Func<TSource, bool> predicate)
            {
                return new Enumerable.WhereArrayIterator<TSource>(this.source,
                                                                  Enumerable.CombinePredicates<TSource>(this.predicate,
                                                                                                        predicate));
            }
        }

        #endregion

        #region Nested type: WhereEnumerableIterator

        private class WhereEnumerableIterator<TSource> : Enumerable.Iterator<TSource>
        {
            private IEnumerator<TSource> enumerator;
            private Func<TSource, bool> predicate;
            private IEnumerable<TSource> source;

            public WhereEnumerableIterator(IEnumerable<TSource> source, Func<TSource, bool> predicate)
            {
                this.source = source;
                this.predicate = predicate;
            }

            public override Enumerable.Iterator<TSource> Clone()
            {
                return new Enumerable.WhereEnumerableIterator<TSource>(this.source, this.predicate);
            }

            public override void Dispose()
            {
                if (this.enumerator != null)
                {
                    this.enumerator.Dispose();
                }
                this.enumerator = null;
                base.Dispose();
            }

            public override bool MoveNext()
            {
                switch (base.state)
                {
                    case 1:
                        this.enumerator = this.source.GetEnumerator();
                        base.state = 2;
                        break;

                    case 2:
                        break;

                    default:
                        goto Label_0069;
                }
                while (this.enumerator.MoveNext())
                {
                    TSource current = this.enumerator.Current;
                    if (this.predicate(current))
                    {
                        base.current = current;
                        return true;
                    }
                }
                this.Dispose();
                Label_0069:
                return false;
            }

            public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
            {
                return new Enumerable.WhereSelectEnumerableIterator<TSource, TResult>(this.source, this.predicate,
                                                                                      selector);
            }

            public override IEnumerable<TSource> Where(Func<TSource, bool> predicate)
            {
                return new Enumerable.WhereEnumerableIterator<TSource>(this.source,
                                                                       Enumerable.CombinePredicates<TSource>(
                                                                           this.predicate, predicate));
            }
        }

        #endregion

        #region Nested type: WhereListIterator

        private class WhereListIterator<TSource> : Enumerable.Iterator<TSource>
        {
            private List<TSource>.Enumerator enumerator;
            private Func<TSource, bool> predicate;
            private List<TSource> source;

            public WhereListIterator(List<TSource> source, Func<TSource, bool> predicate)
            {
                this.source = source;
                this.predicate = predicate;
            }

            public override Enumerable.Iterator<TSource> Clone()
            {
                return new Enumerable.WhereListIterator<TSource>(this.source, this.predicate);
            }

            public override bool MoveNext()
            {
                switch (base.state)
                {
                    case 1:
                        this.enumerator = this.source.GetEnumerator();
                        base.state = 2;
                        break;

                    case 2:
                        break;

                    default:
                        goto Label_0069;
                }
                while (this.enumerator.MoveNext())
                {
                    TSource current = this.enumerator.Current;
                    if (this.predicate(current))
                    {
                        base.current = current;
                        return true;
                    }
                }
                this.Dispose();
                Label_0069:
                return false;
            }

            public override IEnumerable<TResult> Select<TResult>(Func<TSource, TResult> selector)
            {
                return new Enumerable.WhereSelectListIterator<TSource, TResult>(this.source, this.predicate, selector);
            }

            public override IEnumerable<TSource> Where(Func<TSource, bool> predicate)
            {
                return new Enumerable.WhereListIterator<TSource>(this.source,
                                                                 Enumerable.CombinePredicates<TSource>(this.predicate,
                                                                                                       predicate));
            }
        }

        #endregion

        #region Nested type: WhereSelectArrayIterator

        private class WhereSelectArrayIterator<TSource, TResult> : Enumerable.Iterator<TResult>
        {
            private int index;
            private Func<TSource, bool> predicate;
            private Func<TSource, TResult> selector;
            private TSource[] source;

            public WhereSelectArrayIterator(TSource[] source, Func<TSource, bool> predicate,
                                            Func<TSource, TResult> selector)
            {
                this.source = source;
                this.predicate = predicate;
                this.selector = selector;
            }

            public override Enumerable.Iterator<TResult> Clone()
            {
                return new Enumerable.WhereSelectArrayIterator<TSource, TResult>(this.source, this.predicate,
                                                                                 this.selector);
            }

            public override bool MoveNext()
            {
                if (base.state == 1)
                {
                    while (this.index < this.source.Length)
                    {
                        TSource arg = this.source[this.index];
                        this.index++;
                        if ((this.predicate == null) || this.predicate(arg))
                        {
                            base.current = this.selector(arg);
                            return true;
                        }
                    }
                    this.Dispose();
                }
                return false;
            }

            public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
            {
                return new Enumerable.WhereSelectArrayIterator<TSource, TResult2>(this.source, this.predicate,
                                                                                  Enumerable.CombineSelectors
                                                                                      <TSource, TResult, TResult2>(
                                                                                          this.selector, selector));
            }

            public override IEnumerable<TResult> Where(Func<TResult, bool> predicate)
            {
                return (IEnumerable<TResult>) new Enumerable.WhereEnumerableIterator<TResult>(this, predicate);
            }
        }

        #endregion

        #region Nested type: WhereSelectEnumerableIterator

        private class WhereSelectEnumerableIterator<TSource, TResult> : Enumerable.Iterator<TResult>
        {
            private IEnumerator<TSource> enumerator;
            private Func<TSource, bool> predicate;
            private Func<TSource, TResult> selector;
            private IEnumerable<TSource> source;

            public WhereSelectEnumerableIterator(IEnumerable<TSource> source, Func<TSource, bool> predicate,
                                                 Func<TSource, TResult> selector)
            {
                this.source = source;
                this.predicate = predicate;
                this.selector = selector;
            }

            public override Enumerable.Iterator<TResult> Clone()
            {
                return new Enumerable.WhereSelectEnumerableIterator<TSource, TResult>(this.source, this.predicate,
                                                                                      this.selector);
            }

            public override void Dispose()
            {
                if (this.enumerator != null)
                {
                    this.enumerator.Dispose();
                }
                this.enumerator = null;
                base.Dispose();
            }

            public override bool MoveNext()
            {
                switch (base.state)
                {
                    case 1:
                        this.enumerator = this.source.GetEnumerator();
                        base.state = 2;
                        break;

                    case 2:
                        break;

                    default:
                        goto Label_007C;
                }
                while (this.enumerator.MoveNext())
                {
                    TSource current = this.enumerator.Current;
                    if ((this.predicate == null) || this.predicate(current))
                    {
                        base.current = this.selector(current);
                        return true;
                    }
                }
                this.Dispose();
                Label_007C:
                return false;
            }

            public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
            {
                return new Enumerable.WhereSelectEnumerableIterator<TSource, TResult2>(this.source, this.predicate,
                                                                                       Enumerable.CombineSelectors
                                                                                           <TSource, TResult, TResult2>(
                                                                                               this.selector, selector));
            }

            public override IEnumerable<TResult> Where(Func<TResult, bool> predicate)
            {
                return (IEnumerable<TResult>) new Enumerable.WhereEnumerableIterator<TResult>(this, predicate);
            }
        }

        #endregion

        #region Nested type: WhereSelectListIterator

        private class WhereSelectListIterator<TSource, TResult> : Enumerable.Iterator<TResult>
        {
            private List<TSource>.Enumerator enumerator;
            private Func<TSource, bool> predicate;
            private Func<TSource, TResult> selector;
            private List<TSource> source;

            public WhereSelectListIterator(List<TSource> source, Func<TSource, bool> predicate,
                                           Func<TSource, TResult> selector)
            {
                this.source = source;
                this.predicate = predicate;
                this.selector = selector;
            }

            public override Enumerable.Iterator<TResult> Clone()
            {
                return new Enumerable.WhereSelectListIterator<TSource, TResult>(this.source, this.predicate,
                                                                                this.selector);
            }

            public override bool MoveNext()
            {
                switch (base.state)
                {
                    case 1:
                        this.enumerator = this.source.GetEnumerator();
                        base.state = 2;
                        break;

                    case 2:
                        break;

                    default:
                        goto Label_007C;
                }
                while (this.enumerator.MoveNext())
                {
                    TSource current = this.enumerator.Current;
                    if ((this.predicate == null) || this.predicate(current))
                    {
                        base.current = this.selector(current);
                        return true;
                    }
                }
                this.Dispose();
                Label_007C:
                return false;
            }

            public override IEnumerable<TResult2> Select<TResult2>(Func<TResult, TResult2> selector)
            {
                return new Enumerable.WhereSelectListIterator<TSource, TResult2>(this.source, this.predicate,
                                                                                 Enumerable.CombineSelectors
                                                                                     <TSource, TResult, TResult2>(
                                                                                         this.selector, selector));
            }

            public override IEnumerable<TResult> Where(Func<TResult, bool> predicate)
            {
                return (IEnumerable<TResult>) new Enumerable.WhereEnumerableIterator<TResult>(this, predicate);
            }
        }

        #endregion

        #region Nested type: ???

        [CompilerGenerated]
        private sealed class <

        CastIterator
    >
        d__aa<TResult>
    :
        IEnumerable<TResult>
    ,
        IEnumerable
    ,
        IEnumerator<TResult>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TResult<>
        2
        __current;
        public
        IEnumerable<>
        3
        __source;
        public
        IEnumerator<>
        7
        __wrapac;
        public
        IDisposable<>
        7
        __wrapad;
        private
        int  <>
        l__initialThreadId;
        public
        object  <
        obj > 5
        __ab;
        public
        IEnumerable source;

        [
        DebuggerHidden]
            public <
        CastIterator > d__aa(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finallyae()
        {
            this.<>
            1
            __state = -1;
            this.<>
            7
            __wrapad = this.<>
            7
            __wrapac as IDisposable;
            if (this.<>
            7
            __wrapad != null)
            {
                this.<>
                7
                __wrapad.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<>
                    7
                    __wrapac = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_0070;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    goto Label_0070;

                    default:
                    goto Label_0083;
                }
                Label_003C:
                this.<
                obj > 5
                __ab = this.<>
                7
                __wrapac.Current;
                this.<>
                2
                __current = (TResult) this.<
                obj > 5
                __ab;
                this.<>
                1
                __state = 2;
                return true;
                Label_0070:
                if (this.<>
                7
                __wrapac.MoveNext())
                {
                    goto Label_003C;
                }
                this.<>
                m__Finallyae();
                Label_0083:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TResult > IEnumerable<TResult>.GetEnumerator()
        {
            Enumerable.<
            CastIterator > d__aa < TResult > _aa;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                _aa = (Enumerable.<
                CastIterator > d__aa<TResult>)
                this;
            }
        else
            {
                _aa = new Enumerable.<
                CastIterator > d__aa<TResult>(0);
            }
            _aa.source = this.<>
            3
            __source;
            return _aa;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TResult>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finallyae();
                }
                return;
            }
        }

        TResult
        IEnumerator<TResult>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        ConcatIterator
    >
        d__71<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        IEnumerable < TSource > <>
        3
        __first;
        public
        IEnumerable < TSource > <>
        3
        __second;
        public
        IEnumerator < TSource > <>
        7
        __wrap74;
        public
        IEnumerator < TSource > <>
        7
        __wrap76;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __72;
        public
        TSource < element > 5
        __73;
        public
        IEnumerable<TSource> first;
        public
        IEnumerable<TSource> second;

        [
        DebuggerHidden]
            public <
        ConcatIterator > d__71(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally75()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap74 != null)
            {
                this.<>
                7
                __wrap74.Dispose();
            }
        }

    private
        void  <>
        m__Finally77()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap76 != null)
            {
                this.<>
                7
                __wrap76.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<>
                    7
                    __wrap74 = this.first.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_0079;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    goto Label_0079;

                    case 4:
                    goto Label_00CE;

                    default:
                    goto Label_00E8;
                }
                Label_0047:
                this.<
                element > 5
                __72 = this.<>
                7
                __wrap74.Current;
                this.<>
                2
                __current = this.<
                element > 5
                __72;
                this.<>
                1
                __state = 2;
                return true;
                Label_0079:
                if (this.<>
                7
                __wrap74.MoveNext())
                {
                    goto Label_0047;
                }
                this.<>
                m__Finally75();
                this.<>
                7
                __wrap76 = this.second.GetEnumerator();
                this.<>
                1
                __state = 3;
                while (this.<>
                7
                __wrap76.MoveNext())
                {
                    this.<
                    element > 5
                    __73 = this.<>
                    7
                    __wrap76.Current;
                    this.<>
                    2
                    __current = this.<
                    element > 5
                    __73;
                    this.<>
                    1
                    __state = 4;
                    return true;
                    Label_00CE:
                    this.<>
                    1
                    __state = 3;
                }
                this.<>
                m__Finally77();
                Label_00E8:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            ConcatIterator > d__71 < TSource > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                ConcatIterator > d__71<TSource>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                ConcatIterator > d__71<TSource>(0);
            }
            d__.first = this.<>
            3
            __first;
            d__.second = this.<>
            3
            __second;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally75();
                }
                break;

                case 3:
                case 4:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally77();
                }
                return;
            }
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        DefaultIfEmptyIterator
    >
        d__9e<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        TSource<>
        3
        __defaultValue;
        public
        IEnumerable < TSource > <>
        3
        __source;
        private
        int  <>
        l__initialThreadId;
        public
        IEnumerator < TSource > <
        e > 5
        __9f;
        public
        TSource defaultValue;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        DefaultIfEmptyIterator > d__9e(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finallya0()
        {
            this.<>
            1
            __state = -1;
            if (this.<
            e > 5
            __9f != null)
            {
                this.<
                e > 5
                __9f.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    break;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    if (this.<
                    e > 5
                    __9f.MoveNext())
                    {
                        goto Label_004E;
                    }
                    goto Label_009E;

                    case 3:
                    this.<>
                    1
                    __state = 1;
                    goto Label_009E;

                    default:
                    goto Label_00A4;
                }
                this.<>
                1
                __state = -1;
                this.<
                e > 5
                __9f = this.source.GetEnumerator();
                this.<>
                1
                __state = 1;
                if (!this.<
                e > 5
                __9f.MoveNext())
                {
                    goto Label_0080;
                }
                Label_004E:
                this.<>
                2
                __current = this.<
                e > 5
                __9f.Current;
                this.<>
                1
                __state = 2;
                return true;
                Label_0080:
                this.<>
                2
                __current = this.defaultValue;
                this.<>
                1
                __state = 3;
                return true;
                Label_009E:
                this.<>
                m__Finallya0();
                Label_00A4:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            DefaultIfEmptyIterator > d__9e < TSource > d__e;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__e = (Enumerable.<
                DefaultIfEmptyIterator > d__9e<TSource>)
                this;
            }
        else
            {
                d__e = new Enumerable.<
                DefaultIfEmptyIterator > d__9e<TSource>(0);
            }
            d__e.source = this.<>
            3
            __source;
            d__e.defaultValue = this.<>
            3
            __defaultValue;
            return d__e;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                case 3:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finallya0();
                }
                return;
            }
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        DistinctIterator
    >
        d__7a<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        IEqualityComparer < TSource > <>
        3
        __comparer;
        public
        IEnumerable < TSource > <>
        3
        __source;
        public
        IEnumerator < TSource > <>
        7
        __wrap7d;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __7c;
        public
        Set < TSource > <
        set > 5
        __7b;
        public
        IEqualityComparer<TSource> comparer;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        DistinctIterator > d__7a(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally7e()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap7d != null)
            {
                this.<>
                7
                __wrap7d.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    set > 5
                    __7b = new Set<TSource>(this.comparer);
                    this.<>
                    7
                    __wrap7d = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_0092;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    goto Label_0092;

                    default:
                    goto Label_00A5;
                }
                Label_0050:
                this.<
                element > 5
                __7c = this.<>
                7
                __wrap7d.Current;
                if (this.<
                set > 5
                __7b.Add(this. < element > 5
                __7c))
                {
                    this.<>
                    2
                    __current = this.<
                    element > 5
                    __7c;
                    this.<>
                    1
                    __state = 2;
                    return true;
                }
                Label_0092:
                if (this.<>
                7
                __wrap7d.MoveNext())
                {
                    goto Label_0050;
                }
                this.<>
                m__Finally7e();
                Label_00A5:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            DistinctIterator > d__7a < TSource > d__a;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__a = (Enumerable.<
                DistinctIterator > d__7a<TSource>)
                this;
            }
        else
            {
                d__a = new Enumerable.<
                DistinctIterator > d__7a<TSource>(0);
            }
            d__a.source = this.<>
            3
            __source;
            d__a.comparer = this.<>
            3
            __comparer;
            return d__a;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally7e();
                }
                return;
            }
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        ExceptIterator
    >
        d__92<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        IEqualityComparer < TSource > <>
        3
        __comparer;
        public
        IEnumerable < TSource > <>
        3
        __first;
        public
        IEnumerable < TSource > <>
        3
        __second;
        public
        IEnumerator < TSource > <>
        7
        __wrap95;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __94;
        public
        Set < TSource > <
        set > 5
        __93;
        public
        IEqualityComparer<TSource> comparer;
        public
        IEnumerable<TSource> first;
        public
        IEnumerable<TSource> second;

        [
        DebuggerHidden]
            public <
        ExceptIterator > d__92(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally96()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap95 != null)
            {
                this.<>
                7
                __wrap95.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    set > 5
                    __93 = new Set<TSource>(this.comparer);
                    foreach (TSource local in this.second)
                    {
                        this.<
                        set > 5
                        __93.Add(local);
                    }
                    this.<>
                    7
                    __wrap95 = this.first.GetEnumerator();
                    this.<>
                    1
                    __state = 2;
                    while (this.<>
                    7
                    __wrap95.MoveNext())
                    {
                        this.<
                        element > 5
                        __94 = this.<>
                        7
                        __wrap95.Current;
                        if (!this.<
                        set > 5
                        __93.Add(this. < element > 5
                        __94))
                        {
                            continue;
                        }
                        this.<>
                        2
                        __current = this.<
                        element > 5
                        __94;
                        this.<>
                        1
                        __state = 3;
                        return true;
                        Label_00BA:
                        this.<>
                        1
                        __state = 2;
                    }
                    this.<>
                    m__Finally96();
                    break;

                    case 3:
                    goto Label_00BA;
                }
                return false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            ExceptIterator > d__92 < TSource > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                ExceptIterator > d__92<TSource>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                ExceptIterator > d__92<TSource>(0);
            }
            d__.first = this.<>
            3
            __first;
            d__.second = this.<>
            3
            __second;
            d__.comparer = this.<>
            3
            __comparer;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 2:
                case 3:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally96();
                }
                return;
            }
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        GroupJoinIterator
    >
        d__6a<TOuter, TInner, TKey, TResult>
    :
        IEnumerable<TResult>
    ,
        IEnumerable
    ,
        IEnumerator<TResult>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TResult<>
        2
        __current;
        public
        IEqualityComparer < TKey > <>
        3
        __comparer;
        public
        IEnumerable < TInner > <>
        3
        __inner;
        public
        Func < TInner,
        TKey > <>
        3
        __innerKeySelector;
        public
        IEnumerable < TOuter > <>
        3
        __outer;
        public
        Func < TOuter,
        TKey > <>
        3
        __outerKeySelector;
        public
        Func < TOuter,
        IEnumerable<TInner> ,
        TResult > <>
        3
        __resultSelector;
        public
        IEnumerator < TOuter > <>
        7
        __wrap6d;
        private
        int  <>
        l__initialThreadId;
        public
        TOuter < item > 5
        __6c;
        public
        Lookup < TKey,
        TInner > <
        lookup > 5
        __6b;
        public
        IEqualityComparer<TKey> comparer;
        public
        IEnumerable<TInner> inner;
        public
        Func<TInner, TKey> innerKeySelector;
        public
        IEnumerable<TOuter> outer;
        public
        Func<TOuter, TKey> outerKeySelector;
        public
        Func<TOuter, IEnumerable<TInner>, TResult> resultSelector;

        [
        DebuggerHidden]
            public <
        GroupJoinIterator > d__6a(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally6e()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap6d != null)
            {
                this.<>
                7
                __wrap6d.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    lookup > 5
                    __6b = Lookup<TKey, TInner>.CreateForJoin(this.inner, this.innerKeySelector, this.comparer);
                    this.<>
                    7
                    __wrap6d = this.outer.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_00B2;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    goto Label_00B2;

                    default:
                    goto Label_00C5;
                }
                Label_005C:
                this.<
                item > 5
                __6c = this.<>
                7
                __wrap6d.Current;
                this.<>
                2
                __current = this.resultSelector(this. < item > 5
                __6c,
                this.<
                lookup > 5
                __6b[this.outerKeySelector(this. < item > 5
                __6c)])
                ;
                this.<>
                1
                __state = 2;
                return true;
                Label_00B2:
                if (this.<>
                7
                __wrap6d.MoveNext())
                {
                    goto Label_005C;
                }
                this.<>
                m__Finally6e();
                Label_00C5:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TResult > IEnumerable<TResult>.GetEnumerator()
        {
            Enumerable.<
            GroupJoinIterator > d__6a < TOuter,
            TInner,
            TKey,
            TResult > d__a;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__a = (Enumerable.<
                GroupJoinIterator > d__6a<TOuter, TInner, TKey, TResult>)
                this;
            }
        else
            {
                d__a = new Enumerable.<
                GroupJoinIterator > d__6a<TOuter, TInner, TKey, TResult>(0);
            }
            d__a.outer = this.<>
            3
            __outer;
            d__a.inner = this.<>
            3
            __inner;
            d__a.outerKeySelector = this.<>
            3
            __outerKeySelector;
            d__a.innerKeySelector = this.<>
            3
            __innerKeySelector;
            d__a.resultSelector = this.<>
            3
            __resultSelector;
            d__a.comparer = this.<>
            3
            __comparer;
            return d__a;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TResult>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally6e();
                }
                return;
            }
        }

        TResult
        IEnumerator<TResult>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        IntersectIterator
    >
        d__8b<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        IEqualityComparer < TSource > <>
        3
        __comparer;
        public
        IEnumerable < TSource > <>
        3
        __first;
        public
        IEnumerable < TSource > <>
        3
        __second;
        public
        IEnumerator < TSource > <>
        7
        __wrap8e;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __8d;
        public
        Set < TSource > <
        set > 5
        __8c;
        public
        IEqualityComparer<TSource> comparer;
        public
        IEnumerable<TSource> first;
        public
        IEnumerable<TSource> second;

        [
        DebuggerHidden]
            public <
        IntersectIterator > d__8b(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally8f()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap8e != null)
            {
                this.<>
                7
                __wrap8e.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    set > 5
                    __8c = new Set<TSource>(this.comparer);
                    foreach (TSource local in this.second)
                    {
                        this.<
                        set > 5
                        __8c.Add(local);
                    }
                    this.<>
                    7
                    __wrap8e = this.first.GetEnumerator();
                    this.<>
                    1
                    __state = 2;
                    while (this.<>
                    7
                    __wrap8e.MoveNext())
                    {
                        this.<
                        element > 5
                        __8d = this.<>
                        7
                        __wrap8e.Current;
                        if (!this.<
                        set > 5
                        __8c.Remove(this. < element > 5
                        __8d))
                        {
                            continue;
                        }
                        this.<>
                        2
                        __current = this.<
                        element > 5
                        __8d;
                        this.<>
                        1
                        __state = 3;
                        return true;
                        Label_00BA:
                        this.<>
                        1
                        __state = 2;
                    }
                    this.<>
                    m__Finally8f();
                    break;

                    case 3:
                    goto Label_00BA;
                }
                return false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            IntersectIterator > d__8b < TSource > d__b;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__b = (Enumerable.<
                IntersectIterator > d__8b<TSource>)
                this;
            }
        else
            {
                d__b = new Enumerable.<
                IntersectIterator > d__8b<TSource>(0);
            }
            d__b.first = this.<>
            3
            __first;
            d__b.second = this.<>
            3
            __second;
            d__b.comparer = this.<>
            3
            __comparer;
            return d__b;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 2:
                case 3:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally8f();
                }
                return;
            }
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        JoinIterator
    >
        d__61<TOuter, TInner, TKey, TResult>
    :
        IEnumerable<TResult>
    ,
        IEnumerable
    ,
        IEnumerator<TResult>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TResult<>
        2
        __current;
        public
        IEqualityComparer < TKey > <>
        3
        __comparer;
        public
        IEnumerable < TInner > <>
        3
        __inner;
        public
        Func < TInner,
        TKey > <>
        3
        __innerKeySelector;
        public
        IEnumerable < TOuter > <>
        3
        __outer;
        public
        Func < TOuter,
        TKey > <>
        3
        __outerKeySelector;
        public
        Func < TOuter,
        TInner,
        TResult > <>
        3
        __resultSelector;
        public
        IEnumerator < TOuter > <>
        7
        __wrap66;
        private
        int  <>
        l__initialThreadId;
        public
        Lookup<TKey, TInner>.Grouping < g > 5
        __64;
        public
        int  <
        i > 5
        __65;
        public
        TOuter < item > 5
        __63;
        public
        Lookup < TKey,
        TInner > <
        lookup > 5
        __62;
        public
        IEqualityComparer<TKey> comparer;
        public
        IEnumerable<TInner> inner;
        public
        Func<TInner, TKey> innerKeySelector;
        public
        IEnumerable<TOuter> outer;
        public
        Func<TOuter, TKey> outerKeySelector;
        public
        Func<TOuter, TInner, TResult> resultSelector;

        [
        DebuggerHidden]
            public <
        JoinIterator > d__61(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally67()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap66 != null)
            {
                this.<>
                7
                __wrap66.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    lookup > 5
                    __62 = Lookup<TKey, TInner>.CreateForJoin(this.inner, this.innerKeySelector, this.comparer);
                    this.<>
                    7
                    __wrap66 = this.outer.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_0104;

                    case 2:
                    goto Label_00DC;

                    default:
                    goto Label_011A;
                }
                Label_005F:
                this.<
                item > 5
                __63 = this.<>
                7
                __wrap66.Current;
                this.<
                g > 5
                __64 = this.<
                lookup > 5
                __62.GetGrouping(this.outerKeySelector(this. < item > 5
                __63),
                false)
                ;
                if (this.<
                g > 5
                __64 != null)
                {
                    this.<
                    i > 5
                    __65 = 0;
                    while (this.<
                    i > 5
                    __65 < this.<
                    g > 5
                    __64.count)
                    {
                        this.<>
                        2
                        __current = this.resultSelector(this. < item > 5
                        __63,
                        this.<
                        g > 5
                        __64.elements[this. < i > 5
                        __65])
                        ;
                        this.<>
                        1
                        __state = 2;
                        return true;
                        Label_00DC:
                        this.<>
                        1
                        __state = 1;
                        this.<
                        i > 5
                        __65++;
                    }
                }
                Label_0104:
                if (this.<>
                7
                __wrap66.MoveNext())
                {
                    goto Label_005F;
                }
                this.<>
                m__Finally67();
                Label_011A:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TResult > IEnumerable<TResult>.GetEnumerator()
        {
            Enumerable.<
            JoinIterator > d__61 < TOuter,
            TInner,
            TKey,
            TResult > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                JoinIterator > d__61<TOuter, TInner, TKey, TResult>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                JoinIterator > d__61<TOuter, TInner, TKey, TResult>(0);
            }
            d__.outer = this.<>
            3
            __outer;
            d__.inner = this.<>
            3
            __inner;
            d__.outerKeySelector = this.<>
            3
            __outerKeySelector;
            d__.innerKeySelector = this.<>
            3
            __innerKeySelector;
            d__.resultSelector = this.<>
            3
            __resultSelector;
            d__.comparer = this.<>
            3
            __comparer;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TResult>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally67();
                }
                return;
            }
        }

        TResult
        IEnumerator<TResult>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        OfTypeIterator
    >
        d__a3<TResult>
    :
        IEnumerable<TResult>
    ,
        IEnumerable
    ,
        IEnumerator<TResult>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TResult<>
        2
        __current;
        public
        IEnumerable<>
        3
        __source;
        public
        IEnumerator<>
        7
        __wrapa5;
        public
        IDisposable<>
        7
        __wrapa6;
        private
        int  <>
        l__initialThreadId;
        public
        object  <
        obj > 5
        __a4;
        public
        IEnumerable source;

        [
        DebuggerHidden]
            public <
        OfTypeIterator > d__a3(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finallya7()
        {
            this.<>
            1
            __state = -1;
            this.<>
            7
            __wrapa6 = this.<>
            7
            __wrapa5 as IDisposable;
            if (this.<>
            7
            __wrapa6 != null)
            {
                this.<>
                7
                __wrapa6.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<>
                    7
                    __wrapa5 = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_007D;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    goto Label_007D;

                    default:
                    goto Label_0090;
                }
                Label_003C:
                this.<
                obj > 5
                __a4 = this.<>
                7
                __wrapa5.Current;
                if (this.<
                obj > 5
                __a4 is TResult)
                {
                    this.<>
                    2
                    __current = (TResult) this.<
                    obj > 5
                    __a4;
                    this.<>
                    1
                    __state = 2;
                    return true;
                }
                Label_007D:
                if (this.<>
                7
                __wrapa5.MoveNext())
                {
                    goto Label_003C;
                }
                this.<>
                m__Finallya7();
                Label_0090:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TResult > IEnumerable<TResult>.GetEnumerator()
        {
            Enumerable.<
            OfTypeIterator > d__a3 < TResult > _a;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                _a = (Enumerable.<
                OfTypeIterator > d__a3<TResult>)
                this;
            }
        else
            {
                _a = new Enumerable.<
                OfTypeIterator > d__a3<TResult>(0);
            }
            _a.source = this.<>
            3
            __source;
            return _a;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TResult>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finallya7();
                }
                return;
            }
        }

        TResult
        IEnumerator<TResult>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        RangeIterator
    >
        d__b1
    :
        IEnumerable<int>
    ,
        IEnumerable
    ,
        IEnumerator<int>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        int  <>
        2
        __current;
        public
        int  <>
        3
        __count;
        public
        int  <>
        3
        __start;
        private
        int  <>
        l__initialThreadId;
        public
        int  <
        i > 5
        __b2;
        public
        int count;
        public
        int start;

        [
        DebuggerHidden]
            public <
        RangeIterator > d__b1(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        bool MoveNext
        ()
        {
            switch (this.<>
            1
            __state)
            {
                case 0:
                this.<>
                1
                __state = -1;
                this.<
                i > 5
                __b2 = 0;
                break;

                case 1:
                this.<>
                1
                __state = -1;
                this.<
                i > 5
                __b2++;
                break;

                default:
                goto Label_0066;
            }
            if (this.<
            i > 5
            __b2 < this.count)
            {
                this.<>
                2
                __current = this.start + this. < i > 5
                __b2;
                this.<>
                1
                __state = 1;
                return true;
            }
            Label_0066:
            return false;
        }

    [
        DebuggerHidden]
        IEnumerator<int>
        IEnumerable<int>.GetEnumerator()
        {
            Enumerable.<
            RangeIterator > d__b1
            _b;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                _b = this;
            }
        else
            {
                _b = new Enumerable.<
                RangeIterator > d__b1(0);
            }
            _b.start = this.<>
            3
            __start;
            _b.count = this.<>
            3
            __count;
            return _b;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
        }

        int IEnumerator<
        int >.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        RepeatIterator
    >
        d__b5<TResult>
    :
        IEnumerable<TResult>
    ,
        IEnumerable
    ,
        IEnumerator<TResult>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TResult<>
        2
        __current;
        public
        int  <>
        3
        __count;
        public
        TResult<>
        3
        __element;
        private
        int  <>
        l__initialThreadId;
        public
        int  <
        i > 5
        __b6;
        public
        int count;
        public
        TResult element;

        [
        DebuggerHidden]
            public <
        RepeatIterator > d__b5(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        bool MoveNext
        ()
        {
            switch (this.<>
            1
            __state)
            {
                case 0:
                this.<>
                1
                __state = -1;
                this.<
                i > 5
                __b6 = 0;
                break;

                case 1:
                this.<>
                1
                __state = -1;
                this.<
                i > 5
                __b6++;
                break;

                default:
                goto Label_005F;
            }
            if (this.<
            i > 5
            __b6 < this.count)
            {
                this.<>
                2
                __current = this.element;
                this.<>
                1
                __state = 1;
                return true;
            }
            Label_005F:
            return false;
        }

    [
        DebuggerHidden]
        IEnumerator < TResult > IEnumerable<TResult>.GetEnumerator()
        {
            Enumerable.<
            RepeatIterator > d__b5 < TResult > _b;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                _b = (Enumerable.<
                RepeatIterator > d__b5<TResult>)
                this;
            }
        else
            {
                _b = new Enumerable.<
                RepeatIterator > d__b5<TResult>(0);
            }
            _b.element = this.<>
            3
            __element;
            _b.count = this.<>
            3
            __count;
            return _b;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TResult>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
        }

        TResult
        IEnumerator<TResult>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        ReverseIterator
    >
        d__99<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        IEnumerable < TSource > <>
        3
        __source;
        private
        int  <>
        l__initialThreadId;
        public
        Buffer < TSource > <
        buffer > 5
        __9a;
        public
        int  <
        i > 5
        __9b;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        ReverseIterator > d__99(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        bool MoveNext
        ()
        {
            switch (this.<>
            1
            __state)
            {
                case 0:
                this.<>
                1
                __state = -1;
                this.<
                buffer > 5
                __9a = new Buffer<TSource>(this.source);
                this.<
                i > 5
                __9b = this.<
                buffer > 5
                __9a.count - 1;
                break;

                case 1:
                this.<>
                1
                __state = -1;
                this.<
                i > 5
                __9b--;
                break;

                default:
                goto Label_008C;
            }
            if (this.<
            i > 5
            __9b >= 0)
            {
                this.<>
                2
                __current = this.<
                buffer > 5
                __9a.items[this. < i > 5
                __9b]
                ;
                this.<>
                1
                __state = 1;
                return true;
            }
            Label_008C:
            return false;
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            ReverseIterator > d__99 < TSource > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                ReverseIterator > d__99<TSource>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                ReverseIterator > d__99<TSource>(0);
            }
            d__.source = this.<>
            3
            __source;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        SelectIterator
    >
        d__7<TSource, TResult>
    :
        IEnumerable<TResult>
    ,
        IEnumerable
    ,
        IEnumerator<TResult>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TResult<>
        2
        __current;
        public
        Func<TSource, int, TResult><>
        3
        __selector;
        public
        IEnumerable < TSource > <>
        3
        __source;
        public
        IEnumerator < TSource > <>
        7
        __wrapa;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __9;
        public
        int  <
        index > 5
        __8;
        public
        Func<TSource, int, TResult> selector;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        SelectIterator > d__7(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finallyb()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrapa != null)
            {
                this.<>
                7
                __wrapa.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    index > 5
                    __8 = -1;
                    this.<>
                    7
                    __wrapa = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_0094;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    goto Label_0094;

                    default:
                    goto Label_00A7;
                }
                Label_0046:
                this.<
                element > 5
                __9 = this.<>
                7
                __wrapa.Current;
                this.<
                index > 5
                __8++;
                this.<>
                2
                __current = this.selector(this. < element > 5
                __9,
                this.<
                index > 5
                __8)
                ;
                this.<>
                1
                __state = 2;
                return true;
                Label_0094:
                if (this.<>
                7
                __wrapa.MoveNext())
                {
                    goto Label_0046;
                }
                this.<>
                m__Finallyb();
                Label_00A7:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TResult > IEnumerable<TResult>.GetEnumerator()
        {
            Enumerable.<
            SelectIterator > d__7 < TSource,
            TResult > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                SelectIterator > d__7<TSource, TResult>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                SelectIterator > d__7<TSource, TResult>(0);
            }
            d__.source = this.<>
            3
            __source;
            d__.selector = this.<>
            3
            __selector;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TResult>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finallyb();
                }
                return;
            }
        }

        TResult
        IEnumerator<TResult>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        SelectManyIterator
    >
        d__14<TSource, TResult>
    :
        IEnumerable<TResult>
    ,
        IEnumerable
    ,
        IEnumerator<TResult>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TResult<>
        2
        __current;
        public
        Func < TSource,
        IEnumerable<TResult> > <>
        3
        __selector;
        public
        IEnumerable < TSource > <>
        3
        __source;
        public
        IEnumerator < TSource > <>
        7
        __wrap17;
        public
        IEnumerator < TResult > <>
        7
        __wrap19;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __15;
        public
        TResult < subElement > 5
        __16;
        public
        Func<TSource, IEnumerable<TResult>> selector;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        SelectManyIterator > d__14(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally18()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap17 != null)
            {
                this.<>
                7
                __wrap17.Dispose();
            }
        }

    private
        void  <>
        m__Finally1a()
        {
            this.<>
            1
            __state = 1;
            if (this.<>
            7
            __wrap19 != null)
            {
                this.<>
                7
                __wrap19.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<>
                    7
                    __wrap17 = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    while (this.<>
                    7
                    __wrap17.MoveNext())
                    {
                        this.<
                        element > 5
                        __15 = this.<>
                        7
                        __wrap17.Current;
                        this.<>
                        7
                        __wrap19 = this.selector(this. < element > 5
                        __15).
                        GetEnumerator();
                        this.<>
                        1
                        __state = 2;
                        while (this.<>
                        7
                        __wrap19.MoveNext())
                        {
                            this.<
                            subElement > 5
                            __16 = this.<>
                            7
                            __wrap19.Current;
                            this.<>
                            2
                            __current = this.<
                            subElement > 5
                            __16;
                            this.<>
                            1
                            __state = 3;
                            return true;
                            Label_0096:
                            this.<>
                            1
                            __state = 2;
                        }
                        this.<>
                        m__Finally1a();
                    }
                    this.<>
                    m__Finally18();
                    break;

                    case 3:
                    goto Label_0096;
                }
                return false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
        }

    [
        DebuggerHidden]
        IEnumerator < TResult > IEnumerable<TResult>.GetEnumerator()
        {
            Enumerable.<
            SelectManyIterator > d__14 < TSource,
            TResult > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                SelectManyIterator > d__14<TSource, TResult>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                SelectManyIterator > d__14<TSource, TResult>(0);
            }
            d__.source = this.<>
            3
            __source;
            d__.selector = this.<>
            3
            __selector;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TResult>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                case 3:
                try
                {
                    switch (this.<>
                    1
                    __state)
                    {
                        case 2:
                        case 3:
                        try
                        {
                        }
                        finally
                        {
                            this.<>
                            m__Finally1a();
                        }
                        break;
                    }
                }
                finally
                {
                    this.<>
                    m__Finally18();
                }
                break;

                default:
                return;
            }
        }

        TResult
        IEnumerator<TResult>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        SelectManyIterator
    >
        d__1d<TSource, TResult>
    :
        IEnumerable<TResult>
    ,
        IEnumerable
    ,
        IEnumerator<TResult>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TResult<>
        2
        __current;
        public
        Func<TSource, int, IEnumerable<TResult>><>
        3
        __selector;
        public
        IEnumerable < TSource > <>
        3
        __source;
        public
        IEnumerator < TSource > <>
        7
        __wrap21;
        public
        IEnumerator < TResult > <>
        7
        __wrap23;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __1f;
        public
        int  <
        index > 5
        __1e;
        public
        TResult < subElement > 5
        __20;
        public
        Func<TSource, int, IEnumerable<TResult>> selector;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        SelectManyIterator > d__1d(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally22()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap21 != null)
            {
                this.<>
                7
                __wrap21.Dispose();
            }
        }

    private
        void  <>
        m__Finally24()
        {
            this.<>
            1
            __state = 1;
            if (this.<>
            7
            __wrap23 != null)
            {
                this.<>
                7
                __wrap23.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    index > 5
                    __1e = -1;
                    this.<>
                    7
                    __wrap21 = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    while (this.<>
                    7
                    __wrap21.MoveNext())
                    {
                        this.<
                        element > 5
                        __1f = this.<>
                        7
                        __wrap21.Current;
                        this.<
                        index > 5
                        __1e++;
                        this.<>
                        7
                        __wrap23 = this.selector(this. < element > 5
                        __1f,
                        this.<
                        index > 5
                        __1e).
                        GetEnumerator();
                        this.<>
                        1
                        __state = 2;
                        while (this.<>
                        7
                        __wrap23.MoveNext())
                        {
                            this.<
                            subElement > 5
                            __20 = this.<>
                            7
                            __wrap23.Current;
                            this.<>
                            2
                            __current = this.<
                            subElement > 5
                            __20;
                            this.<>
                            1
                            __state = 3;
                            return true;
                            Label_00B4:
                            this.<>
                            1
                            __state = 2;
                        }
                        this.<>
                        m__Finally24();
                    }
                    this.<>
                    m__Finally22();
                    break;

                    case 3:
                    goto Label_00B4;
                }
                return false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
        }

    [
        DebuggerHidden]
        IEnumerator < TResult > IEnumerable<TResult>.GetEnumerator()
        {
            Enumerable.<
            SelectManyIterator > d__1d < TSource,
            TResult > d__d;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__d = (Enumerable.<
                SelectManyIterator > d__1d<TSource, TResult>)
                this;
            }
        else
            {
                d__d = new Enumerable.<
                SelectManyIterator > d__1d<TSource, TResult>(0);
            }
            d__d.source = this.<>
            3
            __source;
            d__d.selector = this.<>
            3
            __selector;
            return d__d;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TResult>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                case 3:
                try
                {
                    switch (this.<>
                    1
                    __state)
                    {
                        case 2:
                        case 3:
                        try
                        {
                        }
                        finally
                        {
                            this.<>
                            m__Finally24();
                        }
                        break;
                    }
                }
                finally
                {
                    this.<>
                    m__Finally22();
                }
                break;

                default:
                return;
            }
        }

        TResult
        IEnumerator<TResult>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        SelectManyIterator
    >
        d__27<TSource, TCollection, TResult>
    :
        IEnumerable<TResult>
    ,
        IEnumerable
    ,
        IEnumerator<TResult>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TResult<>
        2
        __current;
        public
        Func<TSource, int, IEnumerable<TCollection>><>
        3
        __collectionSelector;
        public
        Func < TSource,
        TCollection,
        TResult > <>
        3
        __resultSelector;
        public
        IEnumerable < TSource > <>
        3
        __source;
        public
        IEnumerator < TSource > <>
        7
        __wrap2b;
        public
        IEnumerator < TCollection > <>
        7
        __wrap2d;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __29;
        public
        int  <
        index > 5
        __28;
        public
        TCollection < subElement > 5
        __2a;
        public
        Func<TSource, int, IEnumerable<TCollection>> collectionSelector;
        public
        Func<TSource, TCollection, TResult> resultSelector;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        SelectManyIterator > d__27(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally2c()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap2b != null)
            {
                this.<>
                7
                __wrap2b.Dispose();
            }
        }

    private
        void  <>
        m__Finally2e()
        {
            this.<>
            1
            __state = 1;
            if (this.<>
            7
            __wrap2d != null)
            {
                this.<>
                7
                __wrap2d.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    index > 5
                    __28 = -1;
                    this.<>
                    7
                    __wrap2b = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    while (this.<>
                    7
                    __wrap2b.MoveNext())
                    {
                        this.<
                        element > 5
                        __29 = this.<>
                        7
                        __wrap2b.Current;
                        this.<
                        index > 5
                        __28++;
                        this.<>
                        7
                        __wrap2d = this.collectionSelector(this. < element > 5
                        __29,
                        this.<
                        index > 5
                        __28).
                        GetEnumerator();
                        this.<>
                        1
                        __state = 2;
                        while (this.<>
                        7
                        __wrap2d.MoveNext())
                        {
                            this.<
                            subElement > 5
                            __2a = this.<>
                            7
                            __wrap2d.Current;
                            this.<>
                            2
                            __current = this.resultSelector(this. < element > 5
                            __29,
                            this.<
                            subElement > 5
                            __2a)
                            ;
                            this.<>
                            1
                            __state = 3;
                            return true;
                            Label_00C5:
                            this.<>
                            1
                            __state = 2;
                        }
                        this.<>
                        m__Finally2e();
                    }
                    this.<>
                    m__Finally2c();
                    break;

                    case 3:
                    goto Label_00C5;
                }
                return false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
        }

    [
        DebuggerHidden]
        IEnumerator < TResult > IEnumerable<TResult>.GetEnumerator()
        {
            Enumerable.<
            SelectManyIterator > d__27 < TSource,
            TCollection,
            TResult > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                SelectManyIterator > d__27<TSource, TCollection, TResult>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                SelectManyIterator > d__27<TSource, TCollection, TResult>(0);
            }
            d__.source = this.<>
            3
            __source;
            d__.collectionSelector = this.<>
            3
            __collectionSelector;
            d__.resultSelector = this.<>
            3
            __resultSelector;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TResult>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                case 3:
                try
                {
                    switch (this.<>
                    1
                    __state)
                    {
                        case 2:
                        case 3:
                        try
                        {
                        }
                        finally
                        {
                            this.<>
                            m__Finally2e();
                        }
                        break;
                    }
                }
                finally
                {
                    this.<>
                    m__Finally2c();
                }
                break;

                default:
                return;
            }
        }

        TResult
        IEnumerator<TResult>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        SelectManyIterator
    >
        d__31<TSource, TCollection, TResult>
    :
        IEnumerable<TResult>
    ,
        IEnumerable
    ,
        IEnumerator<TResult>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TResult<>
        2
        __current;
        public
        Func < TSource,
        IEnumerable<TCollection> > <>
        3
        __collectionSelector;
        public
        Func < TSource,
        TCollection,
        TResult > <>
        3
        __resultSelector;
        public
        IEnumerable < TSource > <>
        3
        __source;
        public
        IEnumerator < TSource > <>
        7
        __wrap34;
        public
        IEnumerator < TCollection > <>
        7
        __wrap36;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __32;
        public
        TCollection < subElement > 5
        __33;
        public
        Func<TSource, IEnumerable<TCollection>> collectionSelector;
        public
        Func<TSource, TCollection, TResult> resultSelector;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        SelectManyIterator > d__31(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally35()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap34 != null)
            {
                this.<>
                7
                __wrap34.Dispose();
            }
        }

    private
        void  <>
        m__Finally37()
        {
            this.<>
            1
            __state = 1;
            if (this.<>
            7
            __wrap36 != null)
            {
                this.<>
                7
                __wrap36.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<>
                    7
                    __wrap34 = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    while (this.<>
                    7
                    __wrap34.MoveNext())
                    {
                        this.<
                        element > 5
                        __32 = this.<>
                        7
                        __wrap34.Current;
                        this.<>
                        7
                        __wrap36 = this.collectionSelector(this. < element > 5
                        __32).
                        GetEnumerator();
                        this.<>
                        1
                        __state = 2;
                        while (this.<>
                        7
                        __wrap36.MoveNext())
                        {
                            this.<
                            subElement > 5
                            __33 = this.<>
                            7
                            __wrap36.Current;
                            this.<>
                            2
                            __current = this.resultSelector(this. < element > 5
                            __32,
                            this.<
                            subElement > 5
                            __33)
                            ;
                            this.<>
                            1
                            __state = 3;
                            return true;
                            Label_00AA:
                            this.<>
                            1
                            __state = 2;
                        }
                        this.<>
                        m__Finally37();
                    }
                    this.<>
                    m__Finally35();
                    break;

                    case 3:
                    goto Label_00AA;
                }
                return false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
        }

    [
        DebuggerHidden]
        IEnumerator < TResult > IEnumerable<TResult>.GetEnumerator()
        {
            Enumerable.<
            SelectManyIterator > d__31 < TSource,
            TCollection,
            TResult > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                SelectManyIterator > d__31<TSource, TCollection, TResult>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                SelectManyIterator > d__31<TSource, TCollection, TResult>(0);
            }
            d__.source = this.<>
            3
            __source;
            d__.collectionSelector = this.<>
            3
            __collectionSelector;
            d__.resultSelector = this.<>
            3
            __resultSelector;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TResult>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                case 3:
                try
                {
                    switch (this.<>
                    1
                    __state)
                    {
                        case 2:
                        case 3:
                        try
                        {
                        }
                        finally
                        {
                            this.<>
                            m__Finally37();
                        }
                        break;
                    }
                }
                finally
                {
                    this.<>
                    m__Finally35();
                }
                break;

                default:
                return;
            }
        }

        TResult
        IEnumerator<TResult>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        SkipIterator
    >
        d__4d<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        int  <>
        3
        __count;
        public
        IEnumerable < TSource > <>
        3
        __source;
        private
        int  <>
        l__initialThreadId;
        public
        IEnumerator < TSource > <
        e > 5
        __4e;
        public
        int count;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        SkipIterator > d__4d(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally4f()
        {
            this.<>
            1
            __state = -1;
            if (this.<
            e > 5
            __4e != null)
            {
                this.<
                e > 5
                __4e.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    e > 5
                    __4e = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_004D;

                    case 2:
                    goto Label_008A;

                    default:
                    goto Label_00A4;
                }
                Label_003F:
                this.count--;
                Label_004D:
                if ((this.count > 0) && this.<
                e > 5
                __4e.MoveNext())
                {
                    goto Label_003F;
                }
                if (this.count <= 0)
                {
                    while (this.<
                    e > 5
                    __4e.MoveNext())
                    {
                        this.<>
                        2
                        __current = this.<
                        e > 5
                        __4e.Current;
                        this.<>
                        1
                        __state = 2;
                        return true;
                        Label_008A:
                        this.<>
                        1
                        __state = 1;
                    }
                }
                this.<>
                m__Finally4f();
                Label_00A4:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            SkipIterator > d__4d < TSource > d__d;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__d = (Enumerable.<
                SkipIterator > d__4d<TSource>)
                this;
            }
        else
            {
                d__d = new Enumerable.<
                SkipIterator > d__4d<TSource>(0);
            }
            d__d.source = this.<>
            3
            __source;
            d__d.count = this.<>
            3
            __count;
            return d__d;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally4f();
                }
                return;
            }
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        SkipWhileIterator
    >
        d__52<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        Func<TSource, bool><>
        3
        __predicate;
        public
        IEnumerable < TSource > <>
        3
        __source;
        public
        IEnumerator < TSource > <>
        7
        __wrap55;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __54;
        public
        bool  <
        yielding > 5
        __53;
        public
        Func<TSource, bool> predicate;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        SkipWhileIterator > d__52(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally56()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap55 != null)
            {
                this.<>
                7
                __wrap55.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    yielding > 5
                    __53 = false;
                    this.<>
                    7
                    __wrap55 = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_009F;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    goto Label_009F;

                    default:
                    goto Label_00B2;
                }
                Label_0046:
                this.<
                element > 5
                __54 = this.<>
                7
                __wrap55.Current;
                if (!this.<
                yielding > 5
                __53 && !this.predicate(this. < element > 5
                __54))
                {
                    this.<
                    yielding > 5
                    __53 = true;
                }
                if (this.<
                yielding > 5
                __53)
                {
                    this.<>
                    2
                    __current = this.<
                    element > 5
                    __54;
                    this.<>
                    1
                    __state = 2;
                    return true;
                }
                Label_009F:
                if (this.<>
                7
                __wrap55.MoveNext())
                {
                    goto Label_0046;
                }
                this.<>
                m__Finally56();
                Label_00B2:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            SkipWhileIterator > d__52 < TSource > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                SkipWhileIterator > d__52<TSource>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                SkipWhileIterator > d__52<TSource>(0);
            }
            d__.source = this.<>
            3
            __source;
            d__.predicate = this.<>
            3
            __predicate;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally56();
                }
                return;
            }
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        SkipWhileIterator
    >
        d__59<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        Func<TSource, int, bool><>
        3
        __predicate;
        public
        IEnumerable < TSource > <>
        3
        __source;
        public
        IEnumerator < TSource > <>
        7
        __wrap5d;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __5c;
        public
        int  <
        index > 5
        __5a;
        public
        bool  <
        yielding > 5
        __5b;
        public
        Func<TSource, int, bool> predicate;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        SkipWhileIterator > d__59(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally5e()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap5d != null)
            {
                this.<>
                7
                __wrap5d.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    index > 5
                    __5a = -1;
                    this.<
                    yielding > 5
                    __5b = false;
                    this.<>
                    7
                    __wrap5d = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_00BA;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    goto Label_00BA;

                    default:
                    goto Label_00CD;
                }
                Label_004D:
                this.<
                element > 5
                __5c = this.<>
                7
                __wrap5d.Current;
                this.<
                index > 5
                __5a++;
                if (!this.<
                yielding > 5
                __5b && !this.predicate(this. < element > 5
                __5c,
                this.<
                index > 5
                __5a))
                {
                    this.<
                    yielding > 5
                    __5b = true;
                }
                if (this.<
                yielding > 5
                __5b)
                {
                    this.<>
                    2
                    __current = this.<
                    element > 5
                    __5c;
                    this.<>
                    1
                    __state = 2;
                    return true;
                }
                Label_00BA:
                if (this.<>
                7
                __wrap5d.MoveNext())
                {
                    goto Label_004D;
                }
                this.<>
                m__Finally5e();
                Label_00CD:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            SkipWhileIterator > d__59 < TSource > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                SkipWhileIterator > d__59<TSource>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                SkipWhileIterator > d__59<TSource>(0);
            }
            d__.source = this.<>
            3
            __source;
            d__.predicate = this.<>
            3
            __predicate;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally5e();
                }
                return;
            }
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        TakeIterator
    >
        d__3a<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        int  <>
        3
        __count;
        public
        IEnumerable < TSource > <>
        3
        __source;
        public
        IEnumerator < TSource > <>
        7
        __wrap3c;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __3b;
        public
        int count;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        TakeIterator > d__3a(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally3d()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap3c != null)
            {
                this.<>
                7
                __wrap3c.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    if (this.count <= 0)
                    {
                        goto Label_009A;
                    }
                    this.<>
                    7
                    __wrap3c = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_0087;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    if (--this.count == 0)
                    {
                        goto Label_0094;
                    }
                    goto Label_0087;

                    default:
                    goto Label_009A;
                }
                Label_0045:
                this.<
                element > 5
                __3b = this.<>
                7
                __wrap3c.Current;
                this.<>
                2
                __current = this.<
                element > 5
                __3b;
                this.<>
                1
                __state = 2;
                return true;
                Label_0087:
                if (this.<>
                7
                __wrap3c.MoveNext())
                {
                    goto Label_0045;
                }
                Label_0094:
                this.<>
                m__Finally3d();
                Label_009A:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            TakeIterator > d__3a < TSource > d__a;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__a = (Enumerable.<
                TakeIterator > d__3a<TSource>)
                this;
            }
        else
            {
                d__a = new Enumerable.<
                TakeIterator > d__3a<TSource>(0);
            }
            d__a.source = this.<>
            3
            __source;
            d__a.count = this.<>
            3
            __count;
            return d__a;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally3d();
                }
                return;
            }
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        TakeWhileIterator
    >
        d__40<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        Func<TSource, bool><>
        3
        __predicate;
        public
        IEnumerable < TSource > <>
        3
        __source;
        public
        IEnumerator < TSource > <>
        7
        __wrap42;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __41;
        public
        Func<TSource, bool> predicate;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        TakeWhileIterator > d__40(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally43()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap42 != null)
            {
                this.<>
                7
                __wrap42.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<>
                    7
                    __wrap42 = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_007E;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    goto Label_007E;

                    default:
                    goto Label_0091;
                }
                Label_003C:
                this.<
                element > 5
                __41 = this.<>
                7
                __wrap42.Current;
                if (!this.predicate(this. < element > 5
                __41))
                {
                    goto Label_008B;
                }
                this.<>
                2
                __current = this.<
                element > 5
                __41;
                this.<>
                1
                __state = 2;
                return true;
                Label_007E:
                if (this.<>
                7
                __wrap42.MoveNext())
                {
                    goto Label_003C;
                }
                Label_008B:
                this.<>
                m__Finally43();
                Label_0091:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            TakeWhileIterator > d__40 < TSource > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                TakeWhileIterator > d__40<TSource>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                TakeWhileIterator > d__40<TSource>(0);
            }
            d__.source = this.<>
            3
            __source;
            d__.predicate = this.<>
            3
            __predicate;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally43();
                }
                return;
            }
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        TakeWhileIterator
    >
        d__46<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        Func<TSource, int, bool><>
        3
        __predicate;
        public
        IEnumerable < TSource > <>
        3
        __source;
        public
        IEnumerator < TSource > <>
        7
        __wrap49;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __48;
        public
        int  <
        index > 5
        __47;
        public
        Func<TSource, int, bool> predicate;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        TakeWhileIterator > d__46(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally4a()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap49 != null)
            {
                this.<>
                7
                __wrap49.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    index > 5
                    __47 = -1;
                    this.<>
                    7
                    __wrap49 = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_009C;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    goto Label_009C;

                    default:
                    goto Label_00AF;
                }
                Label_0046:
                this.<
                element > 5
                __48 = this.<>
                7
                __wrap49.Current;
                this.<
                index > 5
                __47++;
                if (!this.predicate(this. < element > 5
                __48,
                this.<
                index > 5
                __47))
                {
                    goto Label_00A9;
                }
                this.<>
                2
                __current = this.<
                element > 5
                __48;
                this.<>
                1
                __state = 2;
                return true;
                Label_009C:
                if (this.<>
                7
                __wrap49.MoveNext())
                {
                    goto Label_0046;
                }
                Label_00A9:
                this.<>
                m__Finally4a();
                Label_00AF:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            TakeWhileIterator > d__46 < TSource > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                TakeWhileIterator > d__46<TSource>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                TakeWhileIterator > d__46<TSource>(0);
            }
            d__.source = this.<>
            3
            __source;
            d__.predicate = this.<>
            3
            __predicate;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally4a();
                }
                return;
            }
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        UnionIterator
    >
        d__81<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        IEqualityComparer < TSource > <>
        3
        __comparer;
        public
        IEnumerable < TSource > <>
        3
        __first;
        public
        IEnumerable < TSource > <>
        3
        __second;
        public
        IEnumerator < TSource > <>
        7
        __wrap85;
        public
        IEnumerator < TSource > <>
        7
        __wrap87;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __83;
        public
        TSource < element > 5
        __84;
        public
        Set < TSource > <
        set > 5
        __82;
        public
        IEqualityComparer<TSource> comparer;
        public
        IEnumerable<TSource> first;
        public
        IEnumerable<TSource> second;

        [
        DebuggerHidden]
            public <
        UnionIterator > d__81(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally86()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap85 != null)
            {
                this.<>
                7
                __wrap85.Dispose();
            }
        }

    private
        void  <>
        m__Finally88()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap87 != null)
            {
                this.<>
                7
                __wrap87.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    set > 5
                    __82 = new Set<TSource>(this.comparer);
                    this.<>
                    7
                    __wrap85 = this.first.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_009D;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    goto Label_009D;

                    case 4:
                    goto Label_0105;

                    default:
                    goto Label_011F;
                }
                Label_0058:
                this.<
                element > 5
                __83 = this.<>
                7
                __wrap85.Current;
                if (this.<
                set > 5
                __82.Add(this. < element > 5
                __83))
                {
                    this.<>
                    2
                    __current = this.<
                    element > 5
                    __83;
                    this.<>
                    1
                    __state = 2;
                    return true;
                }
                Label_009D:
                if (this.<>
                7
                __wrap85.MoveNext())
                {
                    goto Label_0058;
                }
                this.<>
                m__Finally86();
                this.<>
                7
                __wrap87 = this.second.GetEnumerator();
                this.<>
                1
                __state = 3;
                while (this.<>
                7
                __wrap87.MoveNext())
                {
                    this.<
                    element > 5
                    __84 = this.<>
                    7
                    __wrap87.Current;
                    if (!this.<
                    set > 5
                    __82.Add(this. < element > 5
                    __84))
                    {
                        continue;
                    }
                    this.<>
                    2
                    __current = this.<
                    element > 5
                    __84;
                    this.<>
                    1
                    __state = 4;
                    return true;
                    Label_0105:
                    this.<>
                    1
                    __state = 3;
                }
                this.<>
                m__Finally88();
                Label_011F:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            UnionIterator > d__81 < TSource > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                UnionIterator > d__81<TSource>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                UnionIterator > d__81<TSource>(0);
            }
            d__.first = this.<>
            3
            __first;
            d__.second = this.<>
            3
            __second;
            d__.comparer = this.<>
            3
            __comparer;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally86();
                }
                break;

                case 3:
                case 4:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally88();
                }
                return;
            }
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }

        [CompilerGenerated]
        private sealed class <

        #endregion

        WhereIterator
    >
        d__0<TSource>
    :
        IEnumerable<TSource>
    ,
        IEnumerable
    ,
        IEnumerator<TSource>
    ,
        IEnumerator
    ,
        IDisposable
    {
    private
        int  <>
        1
        __state;
        private
        TSource<>
        2
        __current;
        public
        Func<TSource, int, bool><>
        3
        __predicate;
        public
        IEnumerable < TSource > <>
        3
        __source;
        public
        IEnumerator < TSource > <>
        7
        __wrap3;
        private
        int  <>
        l__initialThreadId;
        public
        TSource < element > 5
        __2;
        public
        int  <
        index > 5
        __1;
        public
        Func<TSource, int, bool> predicate;
        public
        IEnumerable<TSource> source;

        [
        DebuggerHidden]
            public <
        WhereIterator > d__0(int <>
        1
        __state)
        {
            this.<>
            1
            __state = <>
            1
            __state;
            this.<>
            l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

    private
        void  <>
        m__Finally4()
        {
            this.<>
            1
            __state = -1;
            if (this.<>
            7
            __wrap3 != null)
            {
                this.<>
                7
                __wrap3.Dispose();
            }
        }

    private
        bool MoveNext
        ()
        {
            bool flag;
            try
            {
                switch (this.<>
                1
                __state)
                {
                    case 0:
                    this.<>
                    1
                    __state = -1;
                    this.<
                    index > 5
                    __1 = -1;
                    this.<>
                    7
                    __wrap3 = this.source.GetEnumerator();
                    this.<>
                    1
                    __state = 1;
                    goto Label_009C;

                    case 2:
                    this.<>
                    1
                    __state = 1;
                    goto Label_009C;

                    default:
                    goto Label_00AF;
                }
                Label_0046:
                this.<
                element > 5
                __2 = this.<>
                7
                __wrap3.Current;
                this.<
                index > 5
                __1++;
                if (this.predicate(this. < element > 5
                __2,
                this.<
                index > 5
                __1))
                {
                    this.<>
                    2
                    __current = this.<
                    element > 5
                    __2;
                    this.<>
                    1
                    __state = 2;
                    return true;
                }
                Label_009C:
                if (this.<>
                7
                __wrap3.MoveNext())
                {
                    goto Label_0046;
                }
                this.<>
                m__Finally4();
                Label_00AF:
                flag = false;
            }
            fault
            {
                this.System.IDisposable.Dispose();
            }
            return flag;
        }

    [
        DebuggerHidden]
        IEnumerator < TSource > IEnumerable<TSource>.GetEnumerator()
        {
            Enumerable.<
            WhereIterator > d__0 < TSource > d__;
            if ((Thread.CurrentThread.ManagedThreadId == this.<>
            l__initialThreadId) &&
            (this.<>
            1
            __state == -2))
            {
                this.<>
                1
                __state = 0;
                d__ = (Enumerable.<
                WhereIterator > d__0<TSource>)
                this;
            }
        else
            {
                d__ = new Enumerable.<
                WhereIterator > d__0<TSource>(0);
            }
            d__.source = this.<>
            3
            __source;
            d__.predicate = this.<>
            3
            __predicate;
            return d__;
        }

    [
        DebuggerHidden]
        IEnumerator
        IEnumerable.GetEnumerator()
        {
            return this.System.Collections.Generic.IEnumerable<TSource>.GetEnumerator();
        }

    [
        DebuggerHidden]
        void IEnumerator.
        Reset()
        {
            throw new NotSupportedException();
        }

        void IDisposable.
        Dispose()
        {
            switch (this.<>
            1
            __state)
            {
                case 1:
                case 2:
                try
                {
                }
                finally
                {
                    this.<>
                    m__Finally4();
                }
                return;
            }
        }

        TSource
        IEnumerator<TSource>.Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }

        object IEnumerator.
        Current
        {
        [
            DebuggerHidden]
            get
            {
                return this.<>
                2
                __current;
            }
        }
    }
    }
}