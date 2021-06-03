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
        /// <returns>The compressed byte array.</returns>
        public static byte[] Compress(byte[] dataToCompress)
        {
            using var memoryStream = new MemoryStream();
            using var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true);

            gzipStream.Write(dataToCompress, 0, dataToCompress.Length);

            gzipStream.Dispose();
            memoryStream.Dispose();

            return memoryStream.ToArray();
        }

        /// <summary>
        /// This methid will compress a given string <paramref name="stringToCompress"/> using the given encoding <paramref name="encodingToUse"/>
        /// to compress a string to a byte array.
        /// </summary>
        /// <remarks>If no encoding <paramref name="encodingToUse"/> it will use the defualt UTF8 encoding.</remarks>
        /// <param name="stringToCompress">String to compress.</param>
        /// <param name="encodingToUse">Enconding to use for converting string to byte array.</param>
        /// <returns>Compressed byte array.</returns>
        public static byte[] Compress(string stringToCompress, Encoding? encodingToUse = null)
        {
            var encoding = encodingToUse ?? Encoding.UTF8;
            return Compress(encoding.GetBytes(stringToCompress));
        }

        /// <summary>
        /// This method will decompress a given byte array <paramref name="dataToDecompress"/> using gzip.
        /// </summary>
        /// <param name="dataToDecompress">Byte array to be decompressed.</param>
        /// <returns>The decompressed byte array.</returns>
        public static byte[] Decompress(byte[] dataToDecompress)
        {
            var memoryStream = new MemoryStream(dataToDecompress);
            var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress, true);
            var outStream = new MemoryStream();

            gzipStream.CopyTo(outStream);

            var decompressedData = outStream.ToArray();

            gzipStream.Dispose();
            memoryStream.Dispose();
            outStream.Dispose();

            return decompressedData;
        }

        /// <summary>
        /// This methid will decompress a given byte array <paramref name="dataToDecompress"/> to a string using the given encoding <paramref name="encodingToUse"/>.
        /// </summary>
        /// <remarks>If no encoding <paramref name="encodingToUse"/> it will use the defualt UTF8 encoding.</remarks>
        /// <param name="dataToDecompress">Byte array to decompress.</param>
        /// <param name="encodingToUse">Enconding to use for converting byte array to string.</param>
        /// <returns>Decompressed string.</returns>
        public static string DecompressToString(byte[] dataToDecompress, Encoding? encodingToUse = null)
        {
            var encoding = encodingToUse ?? Encoding.UTF8;
            return encoding.GetString(Decompress(dataToDecompress));
        }
    }
}
