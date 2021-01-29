using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace SCM.SwissArmyKnife.Extensions
{

    /// <summary>
    /// Extensions for performing stream-related tasks.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Copies the stream over to a new memoryStream.
        /// The new MemoryStream will be at position 0.
        /// </summary>
        /// <param name="sourceStream">A MemoryStream to copy over to a new stream. Will be exhausted afterwards.</param>
        /// <returns>A new MemoryStream with the content from <paramref name="sourceStream"/> This stream will be at position 0. </returns>
        public static async Task<MemoryStream> AsMemoryStream(this Stream sourceStream)
        {
            var memoryStream = new MemoryStream();
            await sourceStream.CopyToAsync(memoryStream).ConfigureAwait(false);
            memoryStream.Position = 0;
            return memoryStream;
        }

        /// <summary>
        /// Fully reads a stream into a byte array.
        /// Starts at the current stream position.
        /// </summary>
        /// <param name="sourceStream">A MemoryStream to copy over to a byte array. Will be exhausted afterwards.</param>
        /// <returns>A byte array with the content from <paramref name="sourceStream"/>.</returns>
        public static async Task<byte[]> ToByteArray(this Stream sourceStream)
        {
            await using var newMemoryStream = new MemoryStream();
            await sourceStream.CopyToAsync(newMemoryStream).ConfigureAwait(false);
            return newMemoryStream.ToArray();
        }


        /// <summary>
        /// Take a stream and return the content as a string.
        /// Reads from current stream position, and does not reset the stream position afterwards.
        /// Defaults to UTF-8 encoding.
        /// </summary>
        public static string ContentToString(this Stream sourceStream, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            var streamReader = new StreamReader(sourceStream, encoding);
            return streamReader.ReadToEnd();
        }
    }
}
