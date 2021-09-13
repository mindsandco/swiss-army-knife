using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;


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
        /// Returns a new dictionary after applying the provided function to each value from the given dictionary.
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


        /// <summary>
        /// Given a dictionary of &lt;TKey, Task&lt;TVal&gt;&gt; it awaits all the Tasks in parallel,
        /// and constructs a new dictionary&lt;TKey, TVal%gt;.
        /// </summary>
        /// <param name="dictionaryToAwait">The dictionary with tasks to await.</param>
        /// /// <typeparam name="TKey">Type of the dictionaries keys.</typeparam>
        /// <typeparam name="TVal">Type of the dictionaries values.</typeparam>
        /// <returns>A new dictionary with all the Tasks in the values turned into their actual values.</returns>
        [Pure]
        public static async Task<Dictionary<TKey, TVal>> AwaitTasksInValuesAsync<TKey, TVal>(
            this IReadOnlyDictionary<TKey, Task<TVal>> dictionaryToAwait)
            where TKey : notnull
        {
            if (dictionaryToAwait == null)
            {
                throw new ArgumentNullException(nameof(dictionaryToAwait));
            }

            var keys = dictionaryToAwait.Keys;
            var tasks = dictionaryToAwait.Values;

            // Await all tasks
            var results = await Task.WhenAll(tasks).ConfigureAwait(false);

            // Zip the results and the original keys together
            var dictionary = new Dictionary<TKey, TVal>();
            foreach (var valueTuple in keys.Zip(results, (key, val) => (key, val)))
            {
                dictionary.Add(valueTuple.key, valueTuple.val);
            }

            return dictionary;
        }
    }
}
