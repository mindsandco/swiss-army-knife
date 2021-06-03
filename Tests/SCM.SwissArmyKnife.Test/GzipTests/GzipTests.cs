using System;
using System.IO;
using System.Text;
using FluentAssertions;
using SCM.SwissArmyKnife.Compression;
using Xunit;

namespace SCM.SwissArmyKnife.Test.GzipTests
{
    public class GzipTests
    {
        private readonly string testString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed non nunc quis elit commodo placerat. Curabitur iaculis justo enim, " +
            "sit amet scelerisque velit auctor non. Donec eleifend tortor non sem ultrices rhoncus. Mauris porttitor, erat ac faucibus interdum, risus eros rutrum sapien, sed" +
            " lobortis sem nisi eget orci. Morbi non ex erat. Suspendisse porttitor suscipit erat, at auctor lacus placerat quis. Sed porttitor pellentesque vulputate. Nulla dictum " +
            "eros non ligula varius porttitor. Maecenas iaculis ex non sem posuere dignissim. Cras quis nisl in erat convallis egestas. Curabitur vel mi cursus, aliquam nulla eget, " +
            "cursus velit. Morbi sapien ipsum, malesuada eget nulla molestie, dapibus pulvinar turpis. Nam non arcu quis lorem dapibus fringilla. Nullam convallis in turpis vitae vehicula." +
            " Aliquam erat volutpat. Nam sapien leo, gravida eu nulla in, mollis volutpat libero.";

        [Fact]
        public void Compress_ShouldWriteGzipHeader_IfGivenEmptyInput()
        {
            // Arrange
            var testData = Array.Empty<byte>();

            // Act
            var compressedData = Gzip.Compress(testData);

            // Assert
            compressedData.Should().NotBeEmpty();
        }

        [Fact]
        public void Compress_ShouldCreateOutput_ThatIsSmallerThanTheInput()
        {
            // Arrange
            var testData = Encoding.ASCII.GetBytes(testString);

            // Act
            var compressedData = Gzip.Compress(testData);

            // Assert
            testData.Length.Should().BeGreaterThan(compressedData.Length);
        }

        [Fact]
        public void Compress_ShouldCreateOutput_WithFirstTwoBytesSetToGzipSignatureBytes()
        {
            // Arrange
            var testData = Encoding.ASCII.GetBytes(testString);

            // Act
            var compressedData = Gzip.Compress(testData);

            // Assert - if the first two bytes are gzip signature bytes
            compressedData[0].Should().Be(31);
            compressedData[1].Should().Be(139);
        }

        [Fact]
        public void Compress_WithStringAndGivenEncoding_OutputShouldBeByteArray()
        {
            //Arrange
            var encoding = Encoding.ASCII;

            //Act
            var compressedString = Gzip.Compress(testString, encoding);

            //Assert
            compressedString.Should().BeOfType(typeof(byte[]));
        }

        [Fact]
        public void Compress_WithStringAndNoEncoding_OutputShouldBeByteArray()
        {
            //Arrange

            //Act
            var compressedString = Gzip.Compress(testString);

            //Assert
            compressedString.Should().BeOfType(typeof(byte[]));
        }

        [Fact]
        public void Compress_WithEmptyStringAndNoEncoding_OutputShouldBeByteArray()
        {
            //Arrange
            var emptyString = string.Empty;

            //Act
            var compressedString = Gzip.Compress(emptyString);

            //Assert
            compressedString.Should().BeOfType(typeof(byte[]));
        }

        [Fact]
        public void Compress_WithEmptyStringAndWithEncoding_OutputShouldBeByteArray()
        {
            //Arrange
            var encoding = Encoding.ASCII;
            var emptyString = string.Empty;

            //Act
            var compressedString = Gzip.Compress(emptyString, encoding);

            //Assert
            compressedString.Should().BeOfType(typeof(byte[]));
        }


        [Fact]
        public void Decompress_OutputShouldBeEmpty_IfInputEmpty()
        {
            // Arrange
            var testData = Array.Empty<byte>();

            // Act
            var decompressedData = Gzip.Decompress(testData);

            // Assert
            decompressedData.Should().BeEmpty();
        }

        [Fact]
        public void Decompress_ShoudCreateOutput_ThatIsLargerThanTheInput()
        {
            // Arrange
            var testData = File.ReadAllBytes(@$"GzipTests{Path.DirectorySeparatorChar}TestingFiles{Path.DirectorySeparatorChar}TestCompressedData.gz");

            // Act
            var decompressedData = Gzip.Decompress(testData);

            // Assert
            decompressedData.Length.Should().BeGreaterThan(testData.Length);
        }

        [Fact]
        public void Decompress_OutputShouldMatchControlText()
        {
            // Arrange
            var testData = File.ReadAllBytes(@$"GzipTests{Path.DirectorySeparatorChar}TestingFiles{Path.DirectorySeparatorChar}TestCompressedData.gz");

            // Act
            var decompressedData = Gzip.Decompress(testData);
            var decompressedString = Encoding.ASCII.GetString(decompressedData);

            // Assert
            decompressedString.Should().BeEquivalentTo(testString);
        }


        [Fact]
        public void DecompressToString_OutputShouldMatchControlText()
        {
            // Arrange
            var testData = File.ReadAllBytes(@$"GzipTests{Path.DirectorySeparatorChar}TestingFiles{Path.DirectorySeparatorChar}TestCompressedData.gz");
            var encoding = Encoding.ASCII;

            // Act
            var decompressedString = Gzip.DecompressToString(testData, encoding);

            // Assert
            decompressedString.Should().BeEquivalentTo(testString);
        }

        [Fact]
        public void CompressDecompress_OutputShouldMatchControlString()
        {
            //Not the most reliable test, but for what it is worth I think it still brings some value.

            //Arrange
            var dataToCompress = Encoding.ASCII.GetBytes(testString);

            //Act
            var compressedData = Gzip.Compress(dataToCompress);
            var decompressedData = Gzip.Decompress(compressedData);

            //Assert
            Encoding.ASCII.GetString(decompressedData).Should().BeEquivalentTo(testString);
        }
    }
}
