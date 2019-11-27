using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeTech.Toolkit.Extensions
{
    /// <summary>
    /// Extensions for different kinds of enumerables
    /// </summary>
    public static class EnumerableExtensions
    {
        #region SumOrDefault

        /// <summary>
        /// Computes the sum of a sequence of System.Int32 values.
        /// </summary>
        /// <param name="query">A sequence of System.Int32 values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence, or if the sequence is empty, the default value for a System.Int32</returns>
        public static int SumOrDefault(this IEnumerable<int> query)
        {
            if (query.Any())
            {
                return query.Sum();
            }
            else return default(int);
        }

        /// <summary>
        /// Computes the sum of a sequence of nullable System.Int32 values.
        /// </summary>
        /// <param name="query">A sequence of nullable System.Int32 values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence, or if the sequence is empty, the default value for a nullable System.Int32</returns>
        public static int? SumOrDefault(this IEnumerable<int?> query)
        {
            if (query.Any())
            {
                return query.Sum();
            }
            else return default(int);
        }

        /// <summary>
        /// Computes the sum of the sequence of System.Int32 values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="query">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values, or if the sequence is empty, the default value for a System.Int32</returns>
        public static int SumOrDefault<TSource>(this IEnumerable<TSource> query, Func<TSource, int> selector)
        {
            if (query.Any())
            {
                return query.Sum(selector);
            }
            else return default(int);
        }

        /// <summary>
        /// Computes the sum of the sequence of nullable System.Int32 values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="query">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values, or if the sequence is empty, the default value for a nullable System.Int32</returns>
        public static int? SumOrDefault<TSource>(this IEnumerable<TSource> query, Func<TSource, int?> selector)
        {
            if (query.Any())
            {
                return query.Sum(selector);
            }
            else return default(int);
        }

        /// <summary>
        /// Computes the sum of a sequence of System.Double values.
        /// </summary>
        /// <param name="query">A sequence of System.Double values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence, or if the sequence is empty, the default value for a System.Double</returns>
        public static double SumOrDefault(this IEnumerable<double> query)
        {
            if (query.Any())
            {
                return query.Sum();
            }
            else return default(int);
        }

        /// <summary>
        /// Computes the sum of a sequence of nullable System.Double values.
        /// </summary>
        /// <param name="query">A sequence of nullable System.Double values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence, or if the sequence is empty, the default value for a nullable System.Double</returns>
        public static double? SumOrDefault(this IEnumerable<double?> query)
        {
            if (query.Any())
            {
                return query.Sum();
            }
            else return default(int);
        }

        /// <summary>
        /// Computes the sum of the sequence of System.Double values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="query">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values, or if the sequence is empty, the default value for a System.Double</returns>
        public static double SumOrDefault<TSource>(this IEnumerable<TSource> query, Func<TSource, double> selector)
        {
            if (query.Any())
            {
                return query.Sum(selector);
            }
            else return default(double);
        }

        /// <summary>
        /// Computes the sum of the sequence of nullable System.Double values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="query">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values, or if the sequence is empty, the default value for a nullable System.Double</returns>
        public static double? SumOrDefault<TSource>(this IEnumerable<TSource> query, Func<TSource, double?> selector)
        {
            if (query.Any())
            {
                return query.Sum(selector);
            }
            else return default(double);
        }

        /// <summary>
        /// Computes the sum of a sequence of System.Decimal values.
        /// </summary>
        /// <param name="query">A sequence of System.Decimal values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence, or if the sequence is empty, the default value for a System.Decimal</returns>
        public static decimal SumOrDefault<TSource>(this IEnumerable<decimal> query)
        {
            if (query.Any())
            {
                return query.Sum();
            }
            else return default(int);
        }

        /// <summary>
        /// Computes the sum of a sequence of nullable System.Decimal values.
        /// </summary>
        /// <param name="query">A sequence of nullable System.Decimal values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence, or if the sequence is empty, the default value for a nullable System.Decimal</returns>
        public static decimal? SumOrDefault<TSource>(this IEnumerable<decimal?> query)
        {
            if (query.Any())
            {
                return query.Sum();
            }
            else return default(int);
        }

        /// <summary>
        /// Computes the sum of the sequence of System.Decimal values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="query">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values, or if the sequence is empty, the default value for a System.Decimal</returns>
        public static decimal SumOrDefault<TSource>(this IEnumerable<TSource> query, Func<TSource, decimal> selector)
        {
            if (query.Any())
            {
                return query.Sum(selector);
            }
            else return default(decimal);
        }

        /// <summary>
        /// Computes the sum of the sequence of nullable System.Decimal values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="query">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values, or if the sequence is empty, the default value for a nullable System.Decimal</returns>
        public static decimal? SumOrDefault<TSource>(this IEnumerable<TSource> query, Func<TSource, decimal?> selector)
        {
            if (query.Any())
            {
                return query.Sum(selector);
            }
            else return default(decimal);
        }

        /// <summary>
        /// Computes the sum of a sequence of System.Long values.
        /// </summary>
        /// <param name="query">A sequence of System.Long values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence, or if the sequence is empty, the default value for a System.Long</returns>
        public static long SumOrDefault<TSource>(this IEnumerable<long> query)
        {
            if (query.Any())
            {
                return query.Sum();
            }
            else return default(int);
        }

        /// <summary>
        /// Computes the sum of a sequence of nullable System.Long values.
        /// </summary>
        /// <param name="query">A sequence of nullable System.Long values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence, or if the sequence is empty, the default value for a nullable System.Long</returns>
        public static long? SumOrDefault<TSource>(this IEnumerable<long?> query)
        {
            if (query.Any())
            {
                return query.Sum();
            }
            else return default(int);
        }

        /// <summary>
        /// Computes the sum of the sequence of System.Long values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="query">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values, or if the sequence is empty, the default value for a System.Long</returns>
        public static long SumOrDefault<TSource>(this IEnumerable<TSource> query, Func<TSource, long> selector)
        {
            if (query.Any())
            {
                return query.Sum(selector);
            }
            else return default(long);
        }

        /// <summary>
        /// Computes the sum of the sequence of nullable System.Long values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="query">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values, or if the sequence is empty, the default value for a nullable System.Long</returns>
        public static long? SumOrDefault<TSource>(this IEnumerable<TSource> query, Func<TSource, long?> selector)
        {
            if (query.Any())
            {
                return query.Sum(selector);
            }
            else return default(long);
        }

        /// <summary>
        /// Computes the sum of a sequence of System.Float values.
        /// </summary>
        /// <param name="query">A sequence of System.Float values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence, or if the sequence is empty, the default value for a System.Float</returns>
        public static float SumOrDefault<TSource>(this IEnumerable<float> query)
        {
            if (query.Any())
            {
                return query.Sum();
            }
            else return default(int);
        }

        /// <summary>
        /// Computes the sum of a sequence of nullable System.Float values.
        /// </summary>
        /// <param name="query">A sequence of nullable System.Float values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence, or if the sequence is empty, the default value for a nullable System.Float</returns>
        public static float? SumOrDefault<TSource>(this IEnumerable<float?> query)
        {
            if (query.Any())
            {
                return query.Sum();
            }
            else return default(int);
        }

        /// <summary>
        /// Computes the sum of the sequence of System.Float values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="query">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values, or if the sequence is empty, the default value for a System.Float</returns>
        public static float SumOrDefault<T>(this IEnumerable<T> query, Func<T, float> selector)
        {
            if (query.Any())
            {
                return query.Sum(selector);
            }
            else return default(float);
        }

        /// <summary>
        /// Computes the sum of the sequence of nullable System.Float values that are obtained by
        /// invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="query">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the projected values, or if the sequence is empty, the default value for a nullable System.Float</returns>
        public static float? SumOrDefault<TSource>(this IEnumerable<TSource> query, Func<TSource, float?> selector)
        {
            if (query.Any())
            {
                return query.Sum(selector);
            }
            else return default(float);
        }

        #endregion

        /// <summary>
        /// Invokes a transform function on each element of a generic sequence and returns
        /// the minimum resulting value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TValue">The type of the value returned by selector.</typeparam>
        /// <param name="query">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in the sequence, or if the sequence is empty, the default value for TValue</returns>
        public static TValue MinOrDefault<TSource, TValue>(this IEnumerable<TSource> query, Func<TSource, TValue> selector)
        {
            if (query.Any())
            {
                return query.Min(selector);
            }
            else return default(TValue);
        }

        /// <summary>
        /// Returns the minimum value in a generic sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="query">A sequence of values to determine the minimum value of.</param>
        /// <returns>The minimum value in the sequence, or if the sequence is empty, the default value for TSource</returns>
        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> query)
        {
            if (query.Any())
            {
                return query.Min();
            }
            else return default(TSource);
        }

        /// <summary>
        /// Invokes a transform function on each element of a generic sequence and returns
        /// the maximum resulting value.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TValue">The type of the value returned by selector.</typeparam>
        /// <param name="query">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in the sequence, or if the sequence is empty, the default value for TValue</returns>
        public static TValue MaxOrDefault<TSource, TValue>(this IEnumerable<TSource> query, Func<TSource, TValue> selector)
        {
            if (query.Any())
            {
                return query.Max(selector);
            }
            else return default(TValue);
        }

        /// <summary>
        /// Returns the maximum value in a generic sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="query">A sequence of values to determine the maximum value of.</param>
        /// <returns>The maximum value in the sequence, or if the sequence is empty, the default value for TSource</returns>
        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> query)
        {
            if (query.Any())
            {
                return query.Max();
            }
            else return default(TSource);
        }

        /// <summary>
        /// Shuffles the order of the provided sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="query">A sequence of elements to shuffle.</param>
        /// <returns>The sequence in a shuffled order.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> query)
        {
            var indexableQuery = query.ToArray();
            Random random = new Random();

            for (int i = 0; i < indexableQuery.Length; i++)
            {
                int j = random.Next(i, indexableQuery.Length);
                yield return indexableQuery[j];
                indexableQuery[j] = indexableQuery[i];
            }
        }

        /// <summary>
        /// Produces the set difference of two sequences by using the default equality comparer
        /// to compare values.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <param name="owningEnumerable">
        /// An System.Collections.Generic.IEnumerable`1 whose elements that are not also
        /// in second will be returned.
        /// </param>
        /// <param name="enumerable">
        /// An System.Collections.Generic.IEnumerable`1 whose elements that also occur in
        /// the first sequence will cause those elements to be removed from the returned
        /// sequence.
        /// </param>
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> owningEnumerable, IEnumerable<TSource> enumerable)
        {
            return Enumerable.Except(owningEnumerable, enumerable);
        }

        /// <summary>
        /// Divides a sequence into batches of a specified size.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">A sequence of elements to batch.</param>
        /// <param name="batchSize">Maximum size of each batch.</param>
        /// <returns>A sequence of sequences divided into the specified batchsize.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if the batchsize is not a positive value.</exception>
        public static IEnumerable<IEnumerable<TSource>> Batch<TSource>(this IEnumerable<TSource> source, int batchSize)
        {
            if (batchSize > 0)
            {
                using (var enumerator = source.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        yield return YieldBatchElements(enumerator, batchSize - 1).ToList();
                    }
                }
            }
            else throw new ArgumentOutOfRangeException(nameof(batchSize), "The batchsize has to be a positive value");
        }

        private static IEnumerable<T> YieldBatchElements<T>(IEnumerator<T> source, int batchSize)
        {
            yield return source.Current;
            for (int i = 0; i < batchSize && source.MoveNext(); i++)
            {
                yield return source.Current;
            }
        }
    }
}
