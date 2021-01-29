namespace SCM.SwissArmyKnife.Extensions
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Methods for working with more efficiently with HttpResponses
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Ensures that the HttpResponse returns with an OK status code.
        /// If it does not, it will call the "beforeThrowing" function, and then throw a HttpRequestException.
        /// It will pass the error and the request body as a string into the orElseFunction.
        ///
        /// It basically works like EnsureStatusCode, except that it gives you a logging hook.
        /// </summary>
        /// <param name="message">The HttpResponseMessage to ensure is valid</param>
        /// <param name="beforeThrowing">
        /// The function to be called if the request was not successful.
        /// This will get passed the HttpRequestException, and the response body.
        /// You do not need to throw the error.
        /// </param>
        /// <returns></returns>
        public static async Task EnsureSuccessStatusCodeEx(this HttpResponseMessage message,
            Action<HttpRequestException, string> beforeThrowing)
        {
            try
            {
                message.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                var responseBody = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
                beforeThrowing(e, responseBody);
                throw;
            }
        }
    }
}
