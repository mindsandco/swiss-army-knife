namespace SCM.SwissArmyKnife.Extensions
{
    using System;
    using System.Text;

    public static class StringExtensions
    {
        /**
         * Repeat string n times
         * From https://dzone.com/articles/string-repeat-method-for-c
         */
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

        // Truncates a string to a given max length.
        /// <summary>
        /// Truncate a string to a maximum of maxLength characters.
        /// If the string is truncated, "..." is appended to it afterwards.
        /// </summary>
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