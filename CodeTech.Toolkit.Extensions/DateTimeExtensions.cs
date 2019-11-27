using System;

namespace CodeTech.Toolkit.Extensions
{
    /// <summary>
    /// Extension methods for DateTime and DateTimeOffset
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Determines if a DateTime occurs during midnight (ambigous on two dates)
        /// </summary>
        /// <param name="dateTime">DateTime to check</param>
        /// <returns>True if the DateTime occurs during midnight, otherwise false</returns>
        public static bool IsMidnight(this DateTime dateTime)
        {
            return dateTime.TimeOfDay == TimeSpan.Zero;
        }

        /// <summary>
        /// Creates a DateTime based on the provided DateTime, but with the specified kind
        /// </summary>
        /// <param name="dateTime">DateTime to change kind on</param>
        /// <param name="kind">Kind to use</param>
        /// <returns>New DateTime with the date and time of the provided DateTime and with the specified kind</returns>
        public static DateTime SpecifyKind(this DateTime dateTime, DateTimeKind kind)
        {
            return DateTime.SpecifyKind(dateTime, kind);
        }

        /// <summary>
        /// Truncates a DateTime to the closest time equal to the provided TimeSpan value
        /// </summary>
        /// <param name="dateTime">DateTime to truncate</param>
        /// <param name="timeSpan">TimeSpan to truncate DateTime to</param>
        /// <returns>Truncated DateTime</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws if the provided timespan is negative or zero
        /// </exception>
        /// <example>
        /// A datetime of 1970-01-01 11:20:15 and a timespan of 00:15:00 would yield a datetime of 1970-01-01 11:15:00
        /// A datetime of 1970-01-01 11:20:15 and a timespan of 00:05:00 would yield a datetime of 1970-01-01 11:20:00
        /// A datetime of 1970-01-01 11:20:15 and a timespan of 01:00:00 would yield a datetime of 1970-01-01 11:00:00
        /// </example>
        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            if (timeSpan <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(timeSpan), "The provided TimeSpan must be greater than Timespan.Zero");
            }
            else
            {
                return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
            }
        }

        /// <summary>
        /// Truncates a DateTimeOffset to the closest time equal to the provided TimeSpan value
        /// </summary>
        /// <param name="dateTime">DateTimeOffset to truncate</param>
        /// <param name="timeSpan">TimeSpan to truncate DateTime to</param>
        /// <returns>Truncated DateTimeOffset</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws if the provided timespan is negative or zero
        /// </exception>
        /// <example>
        /// A datetimeoffset of 1970-01-01 11:20:15 +01:00 and a timespan of 00:15:00 would yield a datetimeoffset of 1970-01-01 11:15:00 +01:00
        /// A datetimeoffset of 1970-01-01 11:20:15 +01:00 and a timespan of 00:05:00 would yield a datetimeoffset of 1970-01-01 11:20:00 +01:00
        /// A datetimeoffset of 1970-01-01 11:20:15 +01:00 and a timespan of 01:00:00 would yield a datetimeoffset of 1970-01-01 11:00:00 +01:00
        /// </example>
        public static DateTimeOffset Truncate(this DateTimeOffset dateTime, TimeSpan timeSpan)
        {
            if (timeSpan <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(nameof(timeSpan), "The provided TimeSpan must be greater than Timespan.Zero");
            }
            else
            {
                return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
            }
        }
    }
}
