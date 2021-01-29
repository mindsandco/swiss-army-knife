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
        /// The MemoryStream will be at position 0.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<MemoryStream> AsMemoryStream(this Stream stream)
        {
            var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream).ConfigureAwait(false);
            memoryStream.Position = 0;
            return memoryStream;
        }

        /// <summary>
        /// Fully reads a stream into a byte array.
        /// Starts at the current stream position.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static async Task<byte[]> ToByteArray(this Stream input)
        {
            await using var newMemoryStream = new MemoryStream();
            await input.CopyToAsync(newMemoryStream).ConfigureAwait(false);
            return newMemoryStream.ToArray();
        }


        /// <summary>
        /// Debug method to take a stream and return it as a string.
        /// Reads from current stream position, and does not reset the stream position afterwards.
        /// Defaults to UTF-8 encoding
        /// </summary>
        public static string ContentToString(this Stream input, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            var streamReader = new StreamReader(input, encoding);
            return streamReader.ReadToEnd();
        }
    }
}
