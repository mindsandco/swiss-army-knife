using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace SCM.SwissArmyKnife.Extensions
{
    /// <summary>
    /// Extension methods for the BCL Dictionaries.
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

        /// <summary>
        /// Returns a new dictionary after appling the provided function to each value from the given dictionary.
        /// </summary>
        /// <typeparam name="TKey">Key Type.</typeparam>
        /// <typeparam name="TOldValue">Original Type.</typeparam>
        /// <typeparam name="TNewValue">New Type.</typeparam>
        /// <param name="dictionary">Source Dictionary.</param>
        /// <param name="selector">Selector function.</param>
        /// <example>
        /// <code>
        /// // Will return a new dictionary where value type is integer
        /// var myDictionary = new Dictionary&lt;string, string&gt; {
        ///     {"myKey", "2"}
        /// }
        /// myDictionary.SelectValues(oldValue => int.Parse(oldValue, CultureInfo.InvariantCulture));
        ///
        /// // Will throw exception
        /// var myDictionary = new Dictionary&lt;string, string&gt; {
        ///     {"myKey", "bar"}
        /// }
        /// myDictionary.SelectValues(oldValue => int.Parse(oldValue, CultureInfo.InvariantCulture));
        /// </code>
        /// </example>
        /// <returns>Dictioanary&lt;TKey, TNewValue&gt;.</returns>
        [Pure]
        public static Dictionary<TKey, TNewValue> SelectValues<TKey, TOldValue, TNewValue>(
            this IReadOnlyDictionary<TKey, TOldValue> dictionary,
            Func<TOldValue, TNewValue> selector)
        {
            var newDictionary = new Dictionary<TKey, TNewValue>();

            foreach (var keyValuePair in dictionary)
            {
                newDictionary.Add(keyValuePair.Key, selector(keyValuePair.Value));
            }

            return newDictionary;
        }
    }
}
