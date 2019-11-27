using System;

namespace CodeTech.Toolkit.Extensions
{
    /// <summary>
    /// Extensions for Unix timestamps
    /// </summary>
    public static class UnixTimeStampExtensions
    {
        /// <summary>
        /// Converts a DateTime into a unix timestamp
        /// </summary>
        /// <param name="dateTime">DateTime to convert</param>
        /// <returns>Unix timestamp</returns>
        /// <exception cref="ArgumentException">
        /// Throws if the datetime is not a UTC-datetime
        /// </exception>
        public static double ToUnixTimeStamp(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return (double)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            }
            else throw new ArgumentException(nameof(dateTime.Kind), "A unix timestamp must be based on a UTC-date");
        }

        /// <summary>
        /// Converts a unix timestamp into a UTC DateTime
        /// </summary>
        /// <param name="timeStamp">Unix timestamp to convert</param>
        /// <returns>UTC DateTime</returns>
        public static DateTime FromUnixTimeStamp(this double timeStamp)
        {
            return ((long)timeStamp).FromUnixTimeStamp();
        }

        /// <summary>
        /// Converts a unix timestamp into a UTC DateTime
        /// </summary>
        /// <param name="timeStamp">Unix timestamp to convert</param>
        /// <returns>UTC DateTime</returns>
        public static DateTime FromUnixTimeStamp(this long timeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timeStamp);
        }

        /// <summary>
        /// Converts a unix timestamp into a UTC DateTime
        /// </summary>
        /// <param name="timeStamp">Unix timestamp to convert</param>
        /// <returns>UTC DateTime</returns>
        public static DateTime FromUnixTimeStamp(this int timeStamp)
        {
            return ((long)timeStamp).FromUnixTimeStamp();
        }
    }
}
