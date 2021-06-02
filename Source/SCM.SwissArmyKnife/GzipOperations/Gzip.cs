using System.IO;
using System.IO.Compression;

namespace SCM.SwissArmyKnife.GzipOperations
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
            var memoryStream = new MemoryStream();
            var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true);

            gzipStream.Write(dataToCompress, 0, dataToCompress.Length);
            gzipStream.Close();

            return memoryStream.ToArray();
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
            gzipStream.Close();

            return outStream.ToArray();
        }
    }
}
