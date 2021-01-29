using System;
using System.Collections.Generic;
using System.Linq;


namespace SCM.SwissArmyKnife.Extensions
{

    /// <summary>
    /// Extensions to make working with the CLR Random class a little less painful. Includes methods to get
    /// a bunch of different types.
    /// </summary>
    public static class RandomExtensions
    {

        /// <summary>
        /// Returns a double within the range [minValue..maxValue)
        /// (minValue inclusive, maxValue exclusive)
        /// Based on <a href="https://stackoverflow.com/questions/1064901/random-number-between-2-double-numbers">this</a>.
        /// </summary>
        public static double NextDouble(this Random @this, double minValue, double maxValue)
        {
            return @this.NextDouble() * (maxValue - minValue) + minValue;
        }

        /// <summary>
        /// Returns a random boolean. There's a 50/50 chance of each.
        /// </summary>
        public static bool NextBoolean(this Random @this)
        {
            return @this.Next(2) == 0;
        }

        /// <summary>
        /// Returns a random byte. Can be from the range [0..byte.maxValue]
        /// (both inclusive).
        /// </summary>
        public static byte NextByte(this Random @this)
        {
            return (byte)@this.Next(byte.MaxValue + 1);
        }

        /// <summary>
        /// Selects one specific item from an Enumerable.
        /// There's an equal chance it will select each item.
        /// </summary>
        public static T Choice<T>(this Random @this, IEnumerable<T> enumerable)
        {
            var list = enumerable.ToList();
            var index = @this.Next(list.Count);
            return list[index];
        }
    }
}
