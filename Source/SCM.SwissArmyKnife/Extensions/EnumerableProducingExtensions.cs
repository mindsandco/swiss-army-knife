using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace SCM.SwissArmyKnife.Extensions
{
    /// <summary>
    /// Various extensions to succinctly produce IEnumerables out of single items.
    /// </summary>
    public static class EnumerableProducingExtensions
    {
        // From https://stackoverflow.com/questions/1577822/passing-a-single-item-as-ienumerablet

        /// <summary>
        /// Wraps this object instance into an IEnumerable;
        /// consisting of a single item.
        /// </summary>
        /// <typeparam name="T"> Type of the object. </typeparam>
        /// <param name="item"> The instance that will be wrapped. </param>
        /// <returns> An IEnumerable consisting of a single item. </returns>
        [Pure]
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }

        /// <summary>
        /// Wraps this object instance into an IAsyncEnumerable;
        /// consisting of a single item.
        /// </summary>
        /// <typeparam name="T"> Type of the object. </typeparam>
        /// <param name="item"> The instance that will be wrapped. </param>
        /// <returns> An IAsyncEnumerable consisting of a single item. </returns>
        [Pure]
        public static async IAsyncEnumerable<T> YieldAsync<T>(this T item)
        {
            await Task.CompletedTask.ConfigureAwait(false); // Just to satisfy the compiler
            yield return item;
        }
    }
}
