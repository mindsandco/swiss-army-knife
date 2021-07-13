using System;
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
        /// var myCompressedStringyASCII = Gzip.Compress(myString, Encoding.ASCII);
        ///
        /// // Will return the compressed string as byte array, using UTF8 for encoding
        /// var myCompressedStringUTF8 = Gzip.Compress(myString);
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
        /// var myString = "my string";
        /// var myCompressedStringASCII = Gzip.Compress(myString, Encoding.ASCII);
        /// var myCompressedStringUTF8 = Gzip.Compress(myString);
        ///
        /// // Will return the decompressed string using the provided encoding
        /// var myDecompressedStringASCII = Gzip.DecompressToString(myCompressedStringASCII, Encoding.ASCII);
        ///
        /// // Will return the decompressed string using the default (UTF8) encoding
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

        /// <summary>
        /// This method will compress a given file <paramref name="pathToFile"/> and save the compressed file to the provided output directory <paramref name="outputDirectory"/>.
        /// </summary>
        /// <remarks>
        /// If <paramref name="fileName"/> is provided the compressed file will use that name otherwise, the compressed file will use the original file name. This method adds the .gz extentions to the compressed file.
        /// </remarks>
        /// <param name="pathToFile">Path for the file to compress.</param>
        /// <param name="outputDirectory">Output directory where to save the compressed file.</param>
        /// <param name="fileName">(Optional) File name to be used for the compressed file.</param>
        /// <returns>File info of the compressed file.</returns>
        /// <exception cref="ArgumentException">Throws an exception if the file is already compressed using gzip or/and if it is hidden.</exception>
        public static FileInfo CompressFile(string pathToFile, string outputDirectory, string fileName = "")
        {
            // Create output directory in case it doesn't exist
            Directory.CreateDirectory(outputDirectory);

            var fileToCompress = new FileInfo(pathToFile);

            var compressedFilePath = @$"{outputDirectory}{Path.DirectorySeparatorChar}{(string.IsNullOrEmpty(fileName) ? fileToCompress.Name : fileName)}.gz";

            using var originalFileStream = fileToCompress.OpenRead();
            if ((File.GetAttributes(fileToCompress.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
            {
                using (var compressedFileStream = File.Create(compressedFilePath))
                {
                    using var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress);
                    originalFileStream.CopyTo(compressionStream);
                }

                return new FileInfo(compressedFilePath);
            }

            throw new ArgumentException($"Tried compressing file {pathToFile} but could not perform the task.\n Please make sure that file is not hidden or/and is not already compressed (has .gz extension).");
        }

        /// <summary>
        /// This method will decompress a given file <paramref name="pathToCompressedFile"/> and save the decompressed file to the provided output directory <paramref name="outputDirectory"/>.
        /// </summary>
        /// <remarks>
        /// If <paramref name="fileName"/> is provided the decompressed file will use that name otherwise, the decompressed file will use the original file name. This method removes the .gz extentions from the decompressed file.
        /// </remarks>
        /// <param name="pathToCompressedFile">Path for the file to decompress.</param>
        /// <param name="outputDirectory">Output directory where to save the decompressed file.</param>
        /// <param name="fileName">(Optional) File name to be used for the decompressed file.</param>
        /// <returns>File info of the decompressed file.</returns>
        /// <exception cref="ArgumentException">Throws an exception if the file does not have the ".gz" extension.</exception>
        /// <exception cref="FileNotFoundException">Throws an exception if the file does not exist.</exception>
        public static FileInfo DecompressFile(string pathToCompressedFile, string outputDirectory, string fileName = "")
        {
            // Create output directory in case it doesn't exist
            Directory.CreateDirectory(outputDirectory);

            var fileToDecompress = new FileInfo(pathToCompressedFile);

            if (!fileToDecompress.Exists)
            {
                throw new FileNotFoundException($"Could not find {fileToDecompress.FullName}");
            }

            if (!fileToDecompress.Extension.Equals(".gz", StringComparison.Ordinal))
            {
                throw new ArgumentException($"Tried decompressing {pathToCompressedFile} but file is not gzip (.gz). Please make sure to provide gzip files.");
            }

            var uncompressedFile = @$"{outputDirectory}{Path.DirectorySeparatorChar}{(string.IsNullOrEmpty(fileName) ? fileToDecompress.Name.Remove(fileToDecompress.Name.Length - fileToDecompress.Extension.Length) : fileName)}";

            using var originalFileStream = fileToDecompress.OpenRead();
            using var decompressedFileStream = File.Create(uncompressedFile);
            using var decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress);

            decompressionStream.CopyTo(decompressedFileStream);

            return new FileInfo(uncompressedFile);
        }
    }
}
