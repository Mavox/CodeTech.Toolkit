using System;

namespace CodeTech.Toolkit.Intervals
{
    /// <summary>
    /// Represents an interval or range of values
    /// </summary>
    /// <typeparam name="TValue">Type of the values to represent</typeparam>
    public class Interval<TValue> : IInterval<TValue> where TValue : IComparable<TValue>
    {
        /// <summary>
        /// Creates a new interval with the provided start and stop values
        /// </summary>
        /// <param name="start">Value where the interval begins</param>
        /// <param name="stop">Value where the interval ends</param>
        public Interval(TValue start, TValue stop)
        {
            Start = start;
            Stop = stop;
        }

        /// <summary>
        /// Creates a new interval with defaulted start and stop values
        /// </summary>
        public Interval()
        {

        }

        /// <summary>
        /// Gets or sets the start of the interval
        /// </summary>
        public TValue Start { get; set; }

        /// <summary>
        /// Gets or sets the stop of the interval
        /// </summary>
        public TValue Stop { get; set; }
    }
}
