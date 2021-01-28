namespace SCM.SwissArmyKnife.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Extension methods for the CLR Dictionaries
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Get the key from a Dictionary, or throw an exception generated from the errorCreator function
        /// </summary>
        [Pure]
        public static TValue GetOrThrow<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key,
            Func<Exception> errorCreator)
            where TKey : notnull
        {
            if (dictionary.TryGetValue(key, out var value))
            {
                return value;
            }

            throw errorCreator();
        }

        /// <summary>
        /// Same as the Regular GetOrDefault except this one takes a factory to produce the value
        /// </summary>
        [Pure]
        public static TValue GetValueOr<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> valueProducer)
            where TKey : notnull
        {
            return dictionary.TryGetValue(key, out var value) ? value : valueProducer();
        }
    }
}
