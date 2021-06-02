using System;
using System.IO;
using System.Text;
using FluentAssertions;
using SCM.SwissArmyKnife.GzipOperations;
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
        public void Compress_IfInputThenOutput()
        {
            // Arrange
            var testData = Encoding.ASCII.GetBytes(testString);

            // Act
            var compressedData = Gzip.Compress(testData);

            // Assert
            compressedData.Should().NotBeEmpty();
        }

        [Fact]
        public void Compress_IfEmptyInputThenOutput()
        {
            // Arrange
            var testData = Array.Empty<byte>();

            // Act
            var compressedData = Gzip.Compress(testData);

            // Assert
            compressedData.Should().NotBeEmpty(); //because of the gzip header
        }

        [Fact]
        public void Compress_OutputSmallerThenInput()
        {
            // Arrange
            var testData = Encoding.ASCII.GetBytes(testString);

            // Act
            var compressedData = Gzip.Compress(testData);

            // Assert
            testData.Length.Should().BeGreaterThan(compressedData.Length);
        }

        [Fact]
        public void Compress_OutputFirstTwoBytes()
        {
            // Arrange
            var testData = Encoding.ASCII.GetBytes(testString);

            // Act
            var compressedData = Gzip.Compress(testData);

            // Assert
            compressedData[0].Should().Be(31);
            compressedData[1].Should().Be(139);
        }

        [Fact]
        public void Compress_OuputAgainstTestedOutput()
        {
            //The TestCompressedData.gz was created using gzip utility with -n flag set.
            //There is little use in equaling byte by byte as the output can differ.

            // Arrange
            var testData = Encoding.ASCII.GetBytes(testString);

            // Act
            var compressedData = Gzip.Compress(testData);

            // Assert
            var testedCompressedData = File.ReadAllBytes(@$"GzipTests{Path.DirectorySeparatorChar}TestingFiles{Path.DirectorySeparatorChar}TestCompressedData.gz");
            compressedData.Length.Should().Be(testedCompressedData.Length);
        }

        [Fact]
        public void Decompress_IfInputThenOutput()
        {
            // Arrange
            var testData = File.ReadAllBytes(@$"GzipTests{Path.DirectorySeparatorChar}TestingFiles{Path.DirectorySeparatorChar}TestCompressedData.gz");

            // Act
            var decompressedData = Gzip.Decompress(testData);

            // Assert
            decompressedData.Should().NotBeEmpty();
        }

        [Fact]
        public void Decompress_IfEmptyInputThenOutput()
        {
            // Arrange
            var testData = Array.Empty<byte>();

            // Act
            var decompressedData = Gzip.Decompress(testData);

            // Assert
            decompressedData.Should().BeEmpty();
        }

        [Fact]
        public void Decompress_OutputLargerThenInput()
        {
            // Arrange
            var testData = File.ReadAllBytes(@$"GzipTests{Path.DirectorySeparatorChar}TestingFiles{Path.DirectorySeparatorChar}TestCompressedData.gz");

            // Act
            var decompressedData = Gzip.Decompress(testData);

            // Assert
            decompressedData.Length.Should().BeGreaterThan(testData.Length);
        }

        [Fact]
        public void Decompress_OuputMatchesText()
        {
            // Arrange
            var testData = File.ReadAllBytes(@$"GzipTests{Path.DirectorySeparatorChar}TestingFiles{Path.DirectorySeparatorChar}TestCompressedData.gz");

            // Act
            var decompressedData = Gzip.Decompress(testData);
            var decompressedString = Encoding.ASCII.GetString(decompressedData);

            // Assert
            decompressedString.Should().BeEquivalentTo(testString);
        }
    }
}
