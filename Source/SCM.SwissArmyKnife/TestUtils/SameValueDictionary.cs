using System;
using System.Collections;
using System.Collections.Generic;

namespace SCM.SwissArmyKnife.TestUtils
{
    /// <summary>
    /// A stub read-only dictionary that will pretend any key exists in it, and return the default value
    /// given in the constructor.
    ///
    /// Does not support enumerating over it, or listing keys.
    /// <inheritdoc cref="IReadOnlyDictionary{TKey,TValue}"/>
    /// </summary>
    public class SameValueDictionary<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : notnull
    {
        private readonly TValue returnedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="SameValueDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="returnedValue">The value that will be returned on each read operation.</param>
        public SameValueDictionary(TValue returnedValue) => this.returnedValue = returnedValue;

        /// <summary>
        /// Is not implemented - will throw an error if attempting to enumerate over it.
        /// </summary>
        /// <returns>Nothing, it will always throw.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => throw new NotImplementedException();

        /// <summary>
        /// Is not implemented - will throw an error if attempting to enumerate over it.
        /// </summary>
        /// <returns>Nothing, it will always throw.</returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// How many elements are in this dictionary? Will always pretend to only have one element.
        /// </summary>
        public int Count => 1;

        /// <inheritdoc cref="ContainsKey"/>
        public bool ContainsKey(TKey key) => true;

        /// <inheritdoc cref="TryGetValue"/>
        public bool TryGetValue(TKey key, out TValue value)
        {
            value = this.returnedValue;
            return true;
        }

        /// <summary>
        /// Will always return the "returnedValue" set in the constructor
        /// </summary>
        public TValue this[TKey key]
        {
            get => this.returnedValue;
            set
            {
                // do nothing.
            }
        }

        /// <summary>
        /// Gets an error. Not implemented. SameValueDictionary cannot be enumerated over.
        /// </summary>
        public ICollection<TKey> Keys => throw new NotImplementedException();


        /// <summary>
        /// Gets an error. Not implemented. SameValueDictionary cannot be enumerated over.
        /// </summary>
        public ICollection<TValue> Values => throw new NotImplementedException();
    }
}
