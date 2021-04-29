using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Newtonsoft.Json;

namespace SCM.SwissArmyKnife.Compression
{
    /// <summary>
    /// A static class for working with the GZip compression
    /// </summary>
    public static class GZip
    {

        /// <summary>
        /// Takes a string and compresses it via GZip using the given encoding.
        /// </summary>
        /// <param name="stringToCompress">The string to compress.</param>
        /// <param name="encoding">The encoding to use when compressing. Defaults to UTF8.</param>
        /// <returns>A byte array containing the compressed string.</returns>
        public static byte[] Compress(string stringToCompress, Encoding? encoding = null)
        {
            var encodingToUse = encoding ?? Encoding.UTF8;

            var inputBytes = encodingToUse.GetBytes(stringToCompress);
            using var outputStream = new MemoryStream();
            // For some reason we can't use a "using declaration here" as the outputstream.toArray
            // needs to be outside of the lifetime of the gzipstream
            using (var gZipStream = new GZipStream(outputStream, CompressionLevel.Fastest))
            {
                gZipStream.Write(inputBytes, 0, stringToCompress.Length);
            }

            return outputStream.ToArray();
        }


        /// <summary>
        /// Takes a GZip-compressed byte array and decompresses it into a string via the given encoding.
        /// </summary>
        /// <param name="bytesToDecompress">The GZip-compressed byte-array.</param>
        /// <param name="encoding">The encoding to use when decompresssing. Defaults to UTF 8.</param>
        /// <returns>The string decompressed from the bytes.</returns>
        public static string Decompress(byte[] bytesToDecompress, Encoding? encoding = null)
        {
            var encodingToUse = encoding ?? Encoding.UTF8;

            using var inputStream = new MemoryStream(bytesToDecompress);
            using var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress);
            using var resultStream = new MemoryStream();
            gZipStream.CopyTo(resultStream);
            return encodingToUse.GetString(resultStream.ToArray());
        }

        /// <summary>
        /// Takes in a GZipped ArraySegment, decompresses it and uses JSON.Net to deserialize it
        /// into the given object.
        /// This method is stream-based so avoids heavy allocations.
        /// </summary>
        /// <param name="bytesToDecompress">The bytes to decompress and deserialize.</param>
        /// <param name="encoding">The encoding to use when decompresssing. Defaults to UTF 8.</param>
        /// <typeparam name="T">Which type to serialize it to via JSON.Net</typeparam>
        /// <returns>The deserialized object from the string</returns>
        /// <exception cref="JsonSerializationException">Thrown if we couldn't deserialize the string. The error message will contain the raw decompressed string.</exception>
        public static T Decompress<T>(ArraySegment<byte> bytesToDecompress, Encoding? encoding = null)
        {
            var encodingToUse = encoding ?? Encoding.UTF8;

            var originalArray = bytesToDecompress.Array!;
            var arrayIndex = bytesToDecompress.Offset;
            var arrayCount = bytesToDecompress.Count;

            var inputStream = new MemoryStream(originalArray, arrayIndex, arrayCount);
            using var gzipStream = new GZipStream(inputStream, CompressionMode.Decompress);
            using var sr = new StreamReader(gzipStream, encodingToUse);
            using JsonReader reader = new JsonTextReader(sr);
            var serializer = JsonSerializer.CreateDefault();
            var deserialized = serializer.Deserialize<T>(reader);

            // Occasionally if we get faulty messages, the Deserialize call will return null.
            // We'll try to print a proper error message if that happens by decompressing the bytes again.
            if (deserialized != null)
            {
                return deserialized;
            }

            // Error handling
            var memoryStream = new MemoryStream(originalArray, arrayIndex, arrayCount);
            string json = Decompress(memoryStream.ToArray(), encodingToUse);
            throw new JsonSerializationException(
                $"Unable to deserialize json: '{json}'");
        }
    }
}
