using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;


namespace SCM.SwissArmyKnife.Extensions
{
    /// <summary>
    /// Extension methods for the CLR Dictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Get the value for given dictionary key. If no value can be found, it will throw an exception generated from the <paramref name="errorCreator"/> function.
        /// </summary>
        ///
        /// <example>
        /// <code>
        /// var myDictionary = new Dictionary&lt;string, string&gt; {
        ///     {"myKey", "myValue"}
        /// }
        /// // Will return "myValue"
        /// myDictionary.GetOrThrow("myKey", () => new ArgumentException("tried to get invalid key"));
        /// // Will throw ArgumentException
        /// myDictionary.GetOrThrow("nonExistingKey", () => new ArgumentException("tried to get invalid key"));
        /// </code>
        /// </example>
        [Pure]
        public static TValue GetOrThrow<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key,
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
        /// Same as the Regular GetOrDefault except this one takes a factory to produce the value.
        /// </summary>
        [Pure]
        public static TValue GetValueOr<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> valueProducer)
            where TKey : notnull
        {
            return dictionary.TryGetValue(key, out var value) ? value : valueProducer();
        }
    }
}
