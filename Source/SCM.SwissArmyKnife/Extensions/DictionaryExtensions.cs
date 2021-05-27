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
        /// Converts the values in the given dictionary to the new type using the provided convert function. Throws when the value cannot be converted.
        /// </summary>
        /// <typeparam name="K">Key Type.</typeparam>
        /// <typeparam name="O">Original Type.</typeparam>
        /// <typeparam name="N">New Type.</typeparam>
        /// <param name="dictionary">Source Dictionary.</param>
        /// <param name="convertionFunction">Convert function.</param>
        /// <exception cref="InvalidOperationException"> Throws when the provided convert function is not able to convert the value.</exception>
        /// <example>
        /// <code>
        /// // Will return a new dictionary where value type is integer
        /// var myDictionary = new Dictionary&lt;string, string&gt; {
        ///     {"myKey", "2"}
        /// }
        /// myDictionary.ConvertValuesToNewType&lt;string, string, int&gt;("myKey", oldValue => int.Parse(oldValue, CultureInfo.InvariantCulture));
        ///
        /// // Will throw InvalidOperationException
        /// var myDictionary = new Dictionary&lt;string, string&gt; {
        ///     {"myKey", "bar"}
        /// }
        /// myDictionary.ConvertValuesToNewType&lt;string, string, int&gt;("myKey", oldValue => int.Parse(oldValue, CultureInfo.InvariantCulture));
        /// </code>
        /// </example>
        /// <returns>Dictioanary&lt;K, N&gt;</returns>
        public static Dictionary<K, N> ConvertValuesToNewType<K, O, N>(
            this IReadOnlyDictionary<K, O> dictionary,
            Func<O, N> convertionFunction)
        {
            var newDictionary = new Dictionary<K, N>();

            if (dictionary.Count == 0)
            {
                return newDictionary;
            }

            foreach (var keyValuePair in dictionary)
            {
                try
                {
                    newDictionary.Add(keyValuePair.Key, convertionFunction(keyValuePair.Value));
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Tried converting the value but encountered an error", ex);
                }
            }

            return newDictionary;
        }
    }
}
