namespace SCM.SwissArmyKnife.Extensions
{
    using System;

    /**
     * This is for inline transformations of nullable values. C# treats structs and classes
     * differently in regards to how the nullable boxing is done, so there's an overload for both types.
     */
    public static class NullableExtensions
    {
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