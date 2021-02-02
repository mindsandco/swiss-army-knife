using System;
using System.Diagnostics.Contracts;
using System.Text;


namespace SCM.SwissArmyKnife.Extensions
{
    /// <summary>
    /// Collection of string-related extension methods.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Repeat a string n times. Based on <a href="https://dzone.com/articles/string-repeat-method-for-c">this</a>.
        /// </summary>
        [Pure]
        public static string Repeat(this string @this, int timesToRepeat)
        {
            if (timesToRepeat <= 0)
            {
                throw new ArgumentException("timesToRepeat must be <= 0");
            }

            return new StringBuilder(@this.Length * timesToRepeat)
                .AppendJoin(@this, new string[timesToRepeat + 1])
                .ToString();
        }

        /// <summary>
        /// Truncate a string to a maximum of <paramref name="maxLength"/> characters.
        /// If the string is truncated, "..." is appended to it afterwards.
        /// </summary>
        [Pure]
        public static string Truncate(this string @this, int maxLength)
        {
            if (maxLength <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength), "maxlength cannot be <= 0");
            }

            if (maxLength >= @this.Length)
            {
                return @this; // Return as-is, no truncation needed
            }

            return @this.Substring(0, maxLength) + "...";
        }
    }
}
