using System;

namespace CodeTech.Toolkit.Intervals
{
    /// <summary>
    /// Represents an interval or range of values
    /// </summary>
    /// <typeparam name="TValue">Type of the values to represent</typeparam>
    public interface IInterval<TValue> where TValue : IComparable<TValue>
    {
        /// <summary>
        /// Gets or sets the start of the interval
        /// </summary>
        TValue Start { get; set; }
        /// <summary>
        /// Gets or sets the stop of the interval
        /// </summary>
        TValue Stop { get; set; }
    }
}
