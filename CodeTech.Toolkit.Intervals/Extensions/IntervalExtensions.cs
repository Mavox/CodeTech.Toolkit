using CodeTech.Toolkit.Intervals;
using System;
using System.Collections.Generic;

namespace CodeTech.Toolkit.Extensions
{
    /// <summary>
    /// Extensions for generic intervals
    /// </summary>
    public static class IntervalExtensions
    {
        /// <summary>
        /// Checks if an interval contains two values
        /// </summary>
        /// <typeparam name="T">Type of the interval values</typeparam>
        /// <param name="owningInterval">Owning interval</param>
        /// <param name="firstValue">First value</param>
        /// <param name="secondValue">Second value</param>
        /// <param name="captureEqual">True if equal values should be considered as containing, otherwise false</param>
        /// <returns>True if the interval contains the two values, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Throws if any provided value is null</exception>
        public static bool Contains<T>(this IInterval<T> owningInterval, T firstValue, T secondValue, bool captureEqual = true)
            where T : IComparable<T>
        {
            if (owningInterval == null)
            {
                throw new ArgumentNullException(nameof(owningInterval), "Owning interval is null");
            }
            else if (owningInterval.Start == null)
            {
                throw new ArgumentNullException(nameof(owningInterval.Start), "Owning interval Start is null");
            }
            else if (owningInterval.Stop == null)
            {
                throw new ArgumentNullException(nameof(owningInterval.Stop), "Owning interval Stop is null");
            }
            else if (firstValue == null)
            {
                throw new ArgumentNullException(nameof(firstValue), "First value is null");
            }
            else if (secondValue == null)
            {
                throw new ArgumentNullException(nameof(secondValue), "Second value is null");
            }
            else
            {
                if (captureEqual)
                {
                    return owningInterval.Start.CompareTo(firstValue) <= 0 && owningInterval.Stop.CompareTo(secondValue) >= 0;
                }
                else
                {
                    return owningInterval.Start.CompareTo(firstValue) < 0 && owningInterval.Stop.CompareTo(secondValue) > 0;
                }
            }
        }

        /// <summary>
        /// Checks if an interval contains a value
        /// </summary>
        /// <typeparam name="T">Type of the interval values</typeparam>
        /// <param name="owningInterval">Owning interval</param>
        /// <param name="value">Value</param>
        /// <param name="captureEqual">True if equal values should be considered as containing, otherwise false</param>
        /// <returns>True if the interval contains the value, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Throws if any provided value is null</exception>
        public static bool Contains<T>(this IInterval<T> owningInterval, T value, bool captureEqual = true)
            where T : IComparable<T>
        {
            if (owningInterval == null)
            {
                throw new ArgumentNullException(nameof(owningInterval), "Owning interval is null");
            }
            else if (owningInterval.Start == null)
            {
                throw new ArgumentNullException(nameof(owningInterval.Start), "Owning interval Start is null");
            }
            else if (owningInterval.Stop == null)
            {
                throw new ArgumentNullException(nameof(owningInterval.Stop), "Owning interval Stop is null");
            }
            else if(value == null)
            {
                throw new ArgumentNullException(nameof(value), "Value is null");
            }
            else
            {
                if (captureEqual)
                {
                    return owningInterval.Start.CompareTo(value) <= 0 && owningInterval.Stop.CompareTo(value) >= 0;
                }
                else
                {
                    return owningInterval.Start.CompareTo(value) < 0 && owningInterval.Stop.CompareTo(value) > 0;
                }
            }
        }

        /// <summary>
        /// Checks if an interval contains another interval
        /// </summary>
        /// <typeparam name="T">Type of the interval values</typeparam>
        /// <param name="owningInterval">Owning interval</param>
        /// <param name="interval">Interval</param>
        /// <param name="captureEqual">True if equal values should be considered as containing, otherwise false</param>
        /// <returns>True if the interval contains the other interval, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Throws if any provided value is null</exception>
        public static bool Contains<T>(this IInterval<T> owningInterval, IInterval<T> interval, bool captureEqual = true)
            where T : IComparable<T>
        {
            if (interval == null)
            {
                throw new ArgumentNullException(nameof(interval), "Interval is null");
            }
            else
            {
                try
                {
                    return Contains<T>(owningInterval, interval.Start, interval.Stop, captureEqual);
                }
                catch (ArgumentNullException ex)
                {
                    if (ex.ParamName == "firstValue")
                    {
                        throw new ArgumentNullException(nameof(interval.Start), "Interval Start is null");
                    }
                    else if (ex.ParamName == "secondValue")
                    {
                        throw new ArgumentNullException(nameof(interval.Stop), "Interval Stop is null");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if two intervals intersects eachothers
        /// </summary>
        /// <typeparam name="T">Type of the interval values</typeparam>
        /// <param name="owningInterval">Owning interval</param>
        /// <param name="interval">Interval</param>
        /// <param name="captureEqual">True if equal values should be considered as intersecting, otherwise false</param>
        /// <returns>True if the two intervals intersects, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Throws if any provided value is null</exception>
        public static bool Intersects<T>(this IInterval<T> owningInterval, IInterval<T> interval, bool captureEqual = true)
            where T : IComparable<T>
        {
            if (interval == null)
            {
                throw new ArgumentNullException(nameof(interval), "Interval is null");
            }
            else if (owningInterval == null)
            {
                throw new ArgumentNullException(nameof(owningInterval), "Owning interval is null");
            }
            else
            {
                try
                {
                    return Intersects(owningInterval, interval.Start, interval.Stop, captureEqual);
                }
                catch (ArgumentNullException ex)
                {
                    if (ex.ParamName == "firstValue")
                    {
                        throw new ArgumentNullException(nameof(interval.Start), "Interval Start is null");
                    }
                    else if (ex.ParamName == "secondValue")
                    {
                        throw new ArgumentNullException(nameof(interval.Stop), "Interval Stop is null");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if an interval intersects with two values
        /// </summary>
        /// <typeparam name="T">Type of the interval values</typeparam>
        /// <param name="owningInterval">Owning interval</param>
        /// <param name="firstValue">First value</param>
        /// <param name="secondValue">Second value</param>
        /// <param name="captureEqual">True if equal values should be considered as intersecting, otherwise false</param>
        /// <returns>True if the interval intersects with the two values, otherwise false</returns>        
        /// <exception cref="ArgumentNullException">Throws if any provided value is null</exception>
        public static bool Intersects<T>(this IInterval<T> owningInterval, T firstValue, T secondValue, bool captureEqual = true)
            where T : IComparable<T>
        {
            if (owningInterval == null)
            {
                throw new ArgumentNullException(nameof(owningInterval), "Owning interval is null");
            }
            else if (owningInterval.Start == null)
            {
                throw new ArgumentNullException(nameof(owningInterval.Start), "Owning interval Start is null");
            }
            else if (owningInterval.Stop == null)
            {
                throw new ArgumentNullException(nameof(owningInterval.Stop), "Owning interval Stop is null");
            }
            else if (firstValue == null)
            {
                throw new ArgumentNullException(nameof(firstValue), "First value is null");
            }
            else if (secondValue == null)
            {
                throw new ArgumentNullException(nameof(secondValue), "Second value is null");
            }
            else
            {
                if (captureEqual)
                {
                    return (owningInterval.Start.CompareTo(secondValue) <= 0 && owningInterval.Stop.CompareTo(firstValue) >= 0);
                }
                else
                {
                    return (owningInterval.Start.CompareTo(secondValue) < 0 && owningInterval.Stop.CompareTo(firstValue) > 0);
                }
            }
        }

        /// <summary>
        /// Gets the intersection between two intervals
        /// </summary>
        /// <typeparam name="T">Type of the intervals</typeparam>
        /// <param name="owningInterval">Owning interval</param>
        /// <param name="interval">Interval</param>
        /// <param name="captureEqual">True if equal values should be considered as intersecting, otherwise false</param>
        /// <returns>Intersection</returns>
        /// <exception cref="ArgumentNullException">Throws if any provided value is null</exception>
        /// <exception cref="ArgumentException">Throws if the provided values does not intersect</exception>
        public static IInterval<T> GetIntersection<T>(this IInterval<T> owningInterval, IInterval<T> interval, bool captureEqual = true)
            where T : IComparable<T>
        {
            if(interval == null)
            {
                throw new ArgumentNullException(nameof(interval), "Interval is null");
            }
            else
            {
                try
                {
                    return GetIntersection(owningInterval, interval.Start, interval.Stop, captureEqual);
                }
                catch(ArgumentNullException ex)
                {
                    if (ex.ParamName == "firstValue")
                    {
                        throw new ArgumentNullException(nameof(interval.Start), "Interval Start is null");
                    }
                    else if (ex.ParamName == "secondValue")
                    {
                        throw new ArgumentNullException(nameof(interval.Stop), "Interval Stop is null");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the intersection between an interval and two values
        /// </summary>
        /// <typeparam name="T">Type of the interval values</typeparam>
        /// <param name="owningInterval">Owning interval</param>
        /// <param name="firstValue">First value</param>
        /// <param name="secondValue">Second value</param>
        /// <param name="captureEqual">True if equal values should be considered as intersecting, otherwise false</param>
        /// <returns>Intersection</returns>
        /// <exception cref="ArgumentNullException">Throws if any provided value is null</exception>
        /// <exception cref="ArgumentException">Throws if the provided values does not intersect</exception>
        public static IInterval<T> GetIntersection<T>(this IInterval<T> owningInterval, T firstValue, T secondValue, bool captureEqual = true)
            where T : IComparable<T>
        {
            if (owningInterval.Intersects(firstValue, secondValue, captureEqual))
            {
                Interval<T> interval = new Interval<T>
                (
                    start: owningInterval.Start.CompareTo(firstValue) > 0 ? owningInterval.Start : firstValue,
                    stop: owningInterval.Stop.CompareTo(secondValue) < 0 ? owningInterval.Stop : secondValue
                );
                return interval;
            }
            else
            {
                throw new ArgumentException(nameof(owningInterval), "The provided values does not intersect");
            }
        }

        /// <summary>
        /// Attempts to get the intersection between an interval and two values
        /// </summary>
        /// <typeparam name="T">Type of the interval values</typeparam>
        /// <param name="owningInterval">Owning interval</param>
        /// <param name="firstValue">First value</param>
        /// <param name="secondValue">Second value</param>
        /// <param name="intersection">The resulting intersection</param>
        /// <param name="captureEqual">True if equal values should be considered as intersecting, otherwise false</param>
        /// <returns>True if the values intersects, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Throws if any provided value is null</exception>
        public static bool TryGetIntersection<T>(this IInterval<T> owningInterval, T firstValue, T secondValue, out IInterval<T> intersection, bool captureEqual = true)
            where T : IComparable<T>
        {
            if (owningInterval.Intersects(firstValue, secondValue, captureEqual))
            {
                Interval<T> interval = new Interval<T>
                (
                    start: owningInterval.Start.CompareTo(firstValue) > 0 ? owningInterval.Start : firstValue,
                    stop: owningInterval.Stop.CompareTo(secondValue) < 0 ? owningInterval.Stop : secondValue
                );
                intersection = interval;
                return true;
            }
            else
            {
                intersection = default(IInterval<T>);
                return false;
            }
        }

        /// <summary>
        /// Attempts to get the intersection between two intervals
        /// </summary>
        /// <typeparam name="T">Type of the interval values</typeparam>
        /// <param name="owningInterval">Owning interval</param>
        /// <param name="interval">Interval</param>
        /// <param name="intersection">The resulting intersection</param>
        /// <param name="captureEqual">True if equal values should be considered as intersecting, otherwise false</param>
        /// <returns>True if the values intersects, otherwise false</returns>
        /// <exception cref="ArgumentNullException">Throws if any provided value is null</exception>
        public static bool TryGetIntersection<T>(this IInterval<T> owningInterval, IInterval<T> interval, out IInterval<T> intersection, bool captureEqual = true)
            where T : IComparable<T>
        {
            if (owningInterval.Intersects(interval, captureEqual))
            {
                Interval<T> intersectionInterval = new Interval<T>
                (
                    start: owningInterval.Start.CompareTo(interval.Start) > 0 ? owningInterval.Start : interval.Start,
                    stop: owningInterval.Stop.CompareTo(interval.Stop) < 0 ? owningInterval.Stop : interval.Stop
                );
                intersection = intersectionInterval;
                return true;
            }
            else
            {
                intersection = default(IInterval<T>);
                return false;
            }
        }

        /// <summary>
        /// Merges the interval with another intersecting interval
        /// </summary>
        /// <remarks>
        /// The merging process is done by extending the Start and Stop properties of the owning interval to include the provided intervals Start and Stop
        /// </remarks>
        /// <typeparam name="T">Type of the interval values</typeparam>
        /// <typeparam name="TInterval">Type of the interval</typeparam>
        /// <param name="owningInterval">Owning interval</param>
        /// <param name="interval">Interval to merge</param>
        /// <returns>Owning interval with new Start and Stop values</returns>
		public static TInterval Merge<T, TInterval>(this TInterval owningInterval, IInterval<T> interval)
            where T : IComparable<T>
            where TInterval : IInterval<T>
        {
            if (owningInterval.Intersects(interval))
            {
                owningInterval.Start = owningInterval.Start.CompareTo(interval.Start) <= 0 ? owningInterval.Start : interval.Start;
                owningInterval.Stop = owningInterval.Stop.CompareTo(interval.Stop) >= 0 ? owningInterval.Stop : interval.Stop;
            }
            return owningInterval;
        }

        /// <summary>
        /// Splits the interval at the provided value
        /// </summary>
        /// <typeparam name="T">Type of the interval values</typeparam>
        /// <param name="owningInterval">Owning interval</param>
        /// <param name="value">Value to split on</param>
        /// <returns>The splitted interval as an IEnumerable</returns>
        /// <exception cref="ArgumentNullException">Throws if any provided value is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws if the provided value is not within range of the owning interval</exception>
        public static IEnumerable<Interval<T>> Split<T>(this IInterval<T> owningInterval, T value) where T : IComparable<T>
        {
            if (owningInterval.Contains(value, true))
            {
                yield return new Interval<T>(owningInterval.Start, value);
                yield return new Interval<T>(value, owningInterval.Stop);
            }
            else throw new ArgumentOutOfRangeException(nameof(value), "Value to split on was not contained within the provided interval");
        }


        //public static IEnumerable<TInterval> MergeIntersecting<TInterval, T>(this IEnumerable<TInterval> enumerable)
        //   where TInterval : class, IInterval<T>
        //   where T : IComparable<T>
        //{
        //    var collection = enumerable.ToList();

        //    for (int outer = 0; outer < collection.Count; outer++)
        //    {
        //        var outerInterval = collection[outer];
        //        for (int inner = 0; inner < collection.Count; inner++)
        //        {
        //            var innerInterval = collection[inner];

        //            if (innerInterval != null && outerInterval != null)
        //            {
        //                bool intersecting = outerInterval.Intersects(innerInterval, true);
        //                bool referenceEquals = Object.ReferenceEquals(outerInterval, innerInterval);
        //                if (intersecting && !referenceEquals)
        //                {
        //                    collection[collection.IndexOf(innerInterval)] = null;
        //                    outerInterval.Merge(innerInterval);
        //                }
        //            }
        //        }
        //    }

        //    //collection.ForEach(outerInterval => collection.ForEach(innerInterval =>
        //    //{
        //    //    if (outerInterval != null && innerInterval != null)
        //    //    {
        //    //        bool intersecting = outerInterval.Intersects(innerInterval);
        //    //        bool referenceEquals = !Object.ReferenceEquals(outerInterval, innerInterval);
        //    //        if (intersecting && referenceEquals)
        //    //        {
        //    //            collection[collection.IndexOf(innerInterval)] = null; // Kastar InvalidOperationExceptions eftersom vi har modifierat vår collection under iterationen
        //    //            outerInterval.Merge(innerInterval);
        //    //        }
        //    //    }
        //    //}));
        //    return collection.Where(x => x != null).ToList();
        //}
    }
}
