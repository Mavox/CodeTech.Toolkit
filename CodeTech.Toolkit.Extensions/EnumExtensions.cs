using System;
using System.ComponentModel;
using System.Reflection;

namespace CodeTech.Toolkit.Extensions
{
    /// <summary>
    /// Extensions for Enums
    /// </summary>
    public static class EnumExtensions 
    {
        /// <summary>
        /// Gets the first attribute of the provided type from the provided enum value
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to search for</typeparam>
        /// <param name="value">Enum value to search</param>
        /// <returns>First matching attribute</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if no attribute is found</exception>
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            object[] attribs = field.GetCustomAttributes(typeof(TAttribute), true);
            if (attribs.Length > 0)
            {
                return (TAttribute)attribs[0];
            }
            else throw new ArgumentOutOfRangeException(nameof(TAttribute), "No attribute found on the provided enum");
        }

        /// <summary>
        /// Attempts to get the first attribute of the provided type from the provided enum value
        /// </summary>
        /// <typeparam name="TAttribute">Type of attribute to search for</typeparam>
        /// <param name="value">Enum to search</param>
        /// <param name="attribute">First matching attribute</param>
        /// <returns>If attribute is found, true, otherwise false</returns>
        public static bool TryGetAttribute<TAttribute>(this Enum value, out TAttribute attribute) where TAttribute : Attribute
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            object[] attribs = field.GetCustomAttributes(typeof(TAttribute), true);
            if (attribs.Length > 0)
            {
                attribute = (TAttribute)attribs[0];
                return true;
            }
            else
            {
                attribute = default(TAttribute);
                return false;
            }
        }
    }
}
