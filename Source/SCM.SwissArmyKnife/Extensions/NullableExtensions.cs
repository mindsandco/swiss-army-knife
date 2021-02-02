using System;
using System.Diagnostics.Contracts;

namespace SCM.SwissArmyKnife.Extensions
{
    /// <summary>
    /// Extensions for inline-transformations of nullable values
    /// C# treats structs and classes differently in regards to how the nullable boxing is done, so there's an overload for both types.
    /// </summary>
    public static class NullableExtensions
    {
        /// <summary>
        /// Takes a nullable struct, and if it is not-null, applies the given transformation to it.
        /// If it is null, the transformation function is not applied.
        /// </summary>
        [Pure]
        public static TRes? TransformIfExists<T, TRes>(this T? nullable, Func<T, TRes> transformation)
            where T : struct
            where TRes : struct
        {
            if (!nullable.HasValue)
            {
                return null;
            }

            return transformation(nullable.Value);
        }

        /// <summary>
        /// Takes a nullable class, and if it is not-null, applies the given transformation to it.
        /// If it is null, the transformation function is not applied.
        /// </summary>
        [Pure]
        public static TRes? TransformIfExists<T, TRes>(this T? nullable, Func<T, TRes> transformation)
            where T : class
            where TRes : class
        {
            if (nullable == null)
            {
                return null;
            }

            return transformation(nullable);
        }
    }
}
