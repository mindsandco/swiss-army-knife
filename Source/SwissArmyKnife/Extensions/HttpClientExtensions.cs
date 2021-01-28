namespace SCM.SwissArmyKnife.Extensions
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// Extension methods to simplify common HttpClientExtension such as working with JSON.
    /// </summary>
    public static class HttpClientExtensions
    {
        /**
         * GETs the given address, and serializes the resulting JSON object into an object.
         * Throws an error on non-2xx status messages.
         */
        public static Task<T> GetAsJson<T>(this HttpClient httpClient, string url,
            int? maxCharactersToPrint = null)
        {
            return GetAsJson<T>(httpClient, new Uri(url), maxCharactersToPrint);
        }

        /// <summary>
        /// GETs the given address, and serializes the resulting JSON object into an object.
        /// Throws an error on non-2xx status messages.
        /// </summary>
        public static async Task<T> GetAsJson<T>(this HttpClient httpClient, Uri url,
            int? maxCharactersToPrint = null)
        {
            string? body = null;
            try
            {
                var response = await httpClient.GetAsync(url).ConfigureAwait(false);
                body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<T>(body);
            }
            catch (HttpRequestException e)
            {
                string? potentiallyTruncatedBody =
                    maxCharactersToPrint.HasValue ? body?.Truncate(maxCharactersToPrint.Value) : body;

                throw new HttpRequestException(
                    $"Exception while trying to request '{url}'. Original message: '{e.Message}'. Got response (potentially truncated) '{potentiallyTruncatedBody}'",
                    e);
            }
            // Something went wrong in the serialization process
            catch (JsonException e)
            {
                string? potentiallyTruncatedBody =
                    maxCharactersToPrint.HasValue ? body?.Truncate(maxCharactersToPrint.Value) : body;

                throw new JsonException(
                    $"Exception while trying deserialize for '{url}'. Original message: '{e.Message}'. Got response (potentially truncated) {potentiallyTruncatedBody}", e
                );
            }
        }


        /**
         * POSTs the given address with a potential provided body. If the body is provided, it is json-serialized.
         * The JSON response is serialized into an T.
         * Throws an error on non-2xx status messages.
         */
        public static Task<T> PostAsJson<T>(this HttpClient httpClient, string url, object? jsonBody = null,
            int? maxCharactersToPrint = null)
        {
            return PostAsJson<T>(httpClient, new Uri(url), jsonBody, maxCharactersToPrint);
        }

        /// <summary>
        /// POSTs the given address with a potential provided body. If the body is provided, it is json-serialized.
        /// The JSON response is serialized into an T.
        /// Throws an error on non-2xx status messages.
        /// </summary>
        public static async Task<T> PostAsJson<T>(this HttpClient httpClient, Uri url, object? jsonBody = null, int? maxCharactersToPrint = null)
        {
            string? body = null;
            try
            {
                HttpContent? content = null;

                if (jsonBody != null)
                {
                    content = new StringContent(JsonConvert.SerializeObject(jsonBody), Encoding.UTF8,
                        "application/json");
                }

                var response = await httpClient.PostAsync(url, content).ConfigureAwait(false);
                body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<T>(body);
            }

            catch (HttpRequestException e)
            {
                string? potentiallyTruncatedBody =
                    maxCharactersToPrint.HasValue ? body?.Truncate(maxCharactersToPrint.Value) : body;

                throw new HttpRequestException(
                    $"Exception while trying to post '{url}'. Original message: '{e.Message}'. Got response (potentially truncated) '{potentiallyTruncatedBody}'",
                    e);
            }
            // Something went wrong in the serialization process
            catch (JsonException e)
            {
                string? potentiallyTruncatedBody =
                    maxCharactersToPrint.HasValue ? body?.Truncate(maxCharactersToPrint.Value) : body;

                throw new JsonException(
                    $"Exception while trying deserialize post for '{url}'. Original message: '{e.Message}'. Got response (potentially truncated) {potentiallyTruncatedBody}", e
                );
            }
        }
    }
}
