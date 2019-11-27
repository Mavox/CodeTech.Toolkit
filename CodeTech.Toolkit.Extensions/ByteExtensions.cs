using System;

namespace CodeTech.Toolkit.Extensions
{
    /// <summary>
    /// Extension methods for bytes and various byte-related types
    /// </summary>
	public static class ByteExtensions
	{
        /// <summary>
        /// Convert an array of bytes to a Base64 string
        /// </summary>
        /// <param name="bytes">Bytes to convert</param>
        /// <returns>Base64 string</returns>
		public static string ToBase64(this byte[] bytes)
		{
			return Convert.ToBase64String(bytes);
		}
	}
}
