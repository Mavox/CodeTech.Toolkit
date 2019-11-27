using CodeTech.Toolkit.Intervals;
using System;

namespace CodeTech.Toolkit.Extensions
{
    /// <summary>
    /// Extensions for DateTime intervals and DateTimeOffset intervals
    /// </summary>
    public static class IntervalDateTimeExtensions
    {
        /// <summary>
        /// Gets the duration of a datetime-interval
        /// </summary>
        /// <param name="obj">Datetime-interval</param>
        /// <returns>Timespan representing the duration of the interval</returns>
        public static TimeSpan Duration(this IInterval<DateTime> obj)
        {
            return obj.Stop - obj.Start;
        }

        /// <summary>
        /// Gets the duration of a datetimeoffset-interval
        /// </summary>
        /// <param name="obj">DatetimeOffset-interval</param>
        /// <returns>Timespan representing the duration of the interval</returns>
        public static TimeSpan Duration(this IInterval<DateTimeOffset> obj)
        {
            return obj.Stop - obj.Start;
        }

        /// <summary>
        /// Creates a new timespan interval based on the provided datetime interval.
        /// </summary>
        /// <param name="obj">DateTime interval to use when creating a new TimeSpan interval</param>
        /// <returns>TimeSpan interval</returns>
        /// <remarks>
        /// The timeSpan interval will use the DateTime intervals Start TimeoFDay as a startvalue and use the Start TimeOfDay plus the duration of the interval as the stopvalue
        /// </remarks>
        public static IInterval<TimeSpan> ToTimeSpanInterval(this IInterval<DateTime> obj)
        {
            return new Interval<TimeSpan>
            (
                start: obj.Start.TimeOfDay,
                stop: obj.Start.TimeOfDay + obj.Duration()
            );
        }

        /// <summary>
        /// Creates a new timespan interval based on the provided datetimeoffset interval.
        /// </summary>
        /// <param name="obj">DateTimeOffset interval to use when creating a new TimeSpan interval</param>
        /// <returns>TimeSpan interval</returns>
        /// <remarks>
        /// The timeSpan interval will use the DateTimeOffsets intervals Start TimeoFDay as a startvalue and use the Start TimeOfDay plus the duration of the interval as the stopvalue
        /// </remarks>
        public static IInterval<TimeSpan> ToTimeSpanInterval(this IInterval<DateTimeOffset> obj)
        {
            return new Interval<TimeSpan>
            (
                start: obj.Start.TimeOfDay,
                stop: obj.Start.TimeOfDay + obj.Duration()
            );
        }

        /// <summary>
        /// Creates a new DateTimeOffset interval based on a provided DateTime interval and provided kind
        /// </summary>
        /// <param name="obj">Interval to use</param>
        /// <param name="kind">Kind to use when creating DateTimeOffsets</param>
        /// <returns>DateTimeOffset interval</returns>
        /// <remarks>
        /// The Kind property in the provided intervals Start and Stop values will be ignored and instead the provided kind will be used
        /// </remarks>
        public static IInterval<DateTimeOffset> ToDateTimeOffsetInterval(this IInterval<DateTime> obj, DateTimeKind kind)
        {
            return new Interval<DateTimeOffset>(DateTime.SpecifyKind(obj.Start, kind), DateTime.SpecifyKind(obj.Stop, kind));
        }

        /// <summary>
        /// Creates a datetime interval converted to the current timezone
        /// </summary>
        /// <param name="obj">Interval</param>
        /// <returns>Local datetime interval</returns>
        public static IInterval<DateTime> ToLocalDateTimeInterval(this IInterval<DateTimeOffset> obj)
        {
            return new Interval<DateTime>(obj.Start.LocalDateTime, obj.Stop.LocalDateTime);
        }

        /// <summary>
        /// Creates a datetime interval with the original datetimes
        /// </summary>
        /// <param name="obj">Interval</param>
        /// <returns>Native datetime interval</returns>
        public static IInterval<DateTime> ToNativeDateTimeInterval(this IInterval<DateTimeOffset> obj)
        {
            return new Interval<DateTime>(obj.Start.DateTime, obj.Stop.DateTime);
        }

        /// <summary>
        /// Creates a datetime interval converted to UTC-format
        /// </summary>
        /// <param name="obj">Interval</param>
        /// <returns>Utc datetime interval</returns>
        public static IInterval<DateTime> ToUtcDateTimeInterval(this IInterval<DateTimeOffset> obj)
        {
            return new Interval<DateTime>(obj.Start.UtcDateTime, obj.Stop.UtcDateTime);
        }
    }
}
