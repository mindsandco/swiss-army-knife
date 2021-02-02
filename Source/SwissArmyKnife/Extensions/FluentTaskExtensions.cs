using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.SwissArmyKnife.Extensions
{
    /// <summary>
    /// Bunch of extensions for Tasks to make LINQ-based syntax a bit prettier.
    /// E.g instead of (await foo()).Select(DoStuff)
    /// you can write
    /// await foo().Select(DoStuff).
    /// </summary>
    [SuppressMessage("ReSharper", "VSTHRD200", Justification = "We do not want to name methods with `Async` at the end, to keep names similar to LINQ.")]
    [SuppressMessage("ReSharper", "VSTHRD003", Justification = "Allow waiting for 'foreign' tasks here. Tasks don't travel very far and it is expected if the consumer needs scheduling or has conflict, that they will handle it themselves.")]
    public static class FluentTaskExtensions
    {
        /// <summary>
        /// If you have an async function that returns a single element and you'd like to transform it before saving it,
        /// you can call this.
        /// await MyMethodAsync().Select(i => i + 2)
        /// is equivalent to
        /// (await MyMethodAsync()) + 2
        /// you can call await MyMethodAsync().Select(/*...*/)
        /// instead of (await MyMethodAsync()).Select(/*...*/).
        /// </summary>
        public static async Task<TOut> Select<TIn, TOut>(this Task<TIn> task, Func<TIn, TOut> selector) =>
            selector(await task.ConfigureAwait(false));

        /// <summary>
        /// If you have an async function that returns an IEnumerable you can transform it directly.
        /// await MyMethodAsync().Select(i => i + 2)
        /// is equivalent to
        /// (await MyMethodAsync()).Select(i => i + 2)
        /// </summary>
        public static async Task<IEnumerable<TOut>> Select<TIn, TOut>(this Task<IEnumerable<TIn>> task, Func<TIn, TOut> selector) =>
            (await task.ConfigureAwait(false)).Select(selector);

        /// <summary>
        /// If you have an async function you can call await MyMethodAsync().First()
        /// instead of (await MyMethodAsync()).First().
        /// </summary>
        public static async Task<TOut> First<TOut>(this Task<IEnumerable<TOut>> task) =>
            (await task.ConfigureAwait(false)).First();

        /// <summary>
        /// If you have an async function you can call await MyMethodAsync().SelectMany(/*...*/)
        /// instead of (await MyMethodAsync()).SelectMany(/*...*/).
        /// </summary>
        public static async Task<IEnumerable<TOut>> SelectMany<TIn, TOut>(this Task<IEnumerable<TIn>> task, Func<TIn, IEnumerable<TOut>> selector) =>
            (await task.ConfigureAwait(false)).SelectMany(selector); // TODO i'm not sure this works..

        /// <summary>
        /// If you have an async function you can call await MyMethodAsync().ToList()
        /// instead of (await MyMethodAsync()).ToList().
        /// </summary>
        public static async Task<List<TOut>> ToList<TOut>(this Task<IEnumerable<TOut>> task) =>
            (await task.ConfigureAwait(false)).ToList();
    }
}
