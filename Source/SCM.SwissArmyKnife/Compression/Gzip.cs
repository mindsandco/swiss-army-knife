using System.Diagnostics.Contracts;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace SCM.SwissArmyKnife.Compression
{
    /// <summary>
    /// Various utility functions for working with Gzip.
    /// </summary>
    public static class Gzip
    {
        /// <summary>
        /// This method will compress a given byte array <paramref name="dataToCompress"/> using gzip.
        /// </summary>
        /// <param name="dataToCompress">The byte array that will be compressed.</param>
        /// <example>
        /// <code>
        /// var myByteArray = new byte[] {1, 2, 3, 5, 6, 7 };
        /// // Will return the compressed byte array
        ///
        /// var myCompressedByteArray = Gzip.Compress(myByteArray);
        /// </code>
        /// </example>
        /// <returns>The compressed byte array.</returns>
        [Pure]
        public static byte[] Compress(byte[] dataToCompress)
        {
            using var memoryStream = new MemoryStream();
            using var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true);

            gzipStream.Write(dataToCompress, 0, dataToCompress.Length);

            gzipStream.Close();

            return memoryStream.ToArray();
        }

        /// <summary>
        /// This method will compress a given string <paramref name="stringToCompress"/> using the given encoding <paramref name="encodingToUse"/>
        /// to compress a string to a byte array.
        /// </summary>
        /// <remarks>If no encoding <paramref name="encodingToUse"/> it will use the default UTF8 encoding.</remarks>
        /// <param name="stringToCompress">String to compress.</param>
        /// <param name="encodingToUse">Enconding to use for converting string to byte array.</param>
        /// <example>
        /// <code>
        /// var myString = "my string";
        ///
        /// // Will return the compressed string as byte array, using ASCII for encoding
        /// var myCompressedByteArrayASCII = Gzip.Compress(myByteArray, Encoding.ASCII);
        ///
        /// // Will return the compressed string as byte array, using UTF8 for encoding
        /// var myCompressedByteArrayUTF8 = Gzip.Compress(myByteArray);
        /// </code>
        /// </example>
        /// <returns>Compressed byte array.</returns>
        [Pure]
        public static byte[] Compress(string stringToCompress, Encoding? encodingToUse = null)
        {
            var encoding = encodingToUse ?? Encoding.UTF8;
            return Compress(encoding.GetBytes(stringToCompress));
        }

        /// <summary>
        /// This method will decompress a given byte array <paramref name="dataToDecompress"/> using gzip.
        /// </summary>
        /// <param name="dataToDecompress">Byte array to be decompressed.</param>
        /// <example>
        /// <code>
        /// var myCompressedByteArray = Gzip.Compress(new byte[] { 1, 2, 3, 5, 6, 7 });
        ///
        /// // Will return the decompressed byte array
        /// var myDecompressedByteArray = Gzip.Decompress(myCompressedByteArray);
        /// </code>
        /// </example>
        /// <returns>The decompressed byte array.</returns>
        [Pure]
        public static byte[] Decompress(byte[] dataToDecompress)
        {
            var memoryStream = new MemoryStream(dataToDecompress);
            var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress, true);
            var outStream = new MemoryStream();

            gzipStream.CopyTo(outStream);

            return outStream.ToArray();
        }

        /// <summary>
        /// This method will decompress a given byte array <paramref name="dataToDecompress"/> to a string using the given encoding <paramref name="encodingToUse"/>.
        /// </summary>
        /// <remarks>If no encoding <paramref name="encodingToUse"/> it will use the default UTF8 encoding.</remarks>
        /// <param name="dataToDecompress">Byte array to decompress.</param>
        /// <param name="encodingToUse">Enconding to use for converting byte array to string.</param>
        /// <example>
        /// <code>
        /// var myCompressedStringASCII = Gzip.Compress("my string", Encoding.ASCII);
        /// var myCompressedStringUTF8 = Gzip.Compress("me string");
        ///
        /// // Will return the decompressed string using the provided encoding
        /// var myDecompressedStringASCII = Gzip.DecompressToString(myCompressedStringASCII, Encoding.ASCII);
        ///
        /// // Will return the decompressed string using the default encoding
        /// var myDecompressedStringUTF8 = Gzip.DecompressToString(myCompressedStringUTF8);
        /// </code>
        /// </example>
        /// <returns>Decompressed string.</returns>
        [Pure]
        public static string DecompressToString(byte[] dataToDecompress, Encoding? encodingToUse = null)
        {
            var encoding = encodingToUse ?? Encoding.UTF8;
            return encoding.GetString(Decompress(dataToDecompress));
        }
    }
}
