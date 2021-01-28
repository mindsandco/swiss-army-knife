using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ScadaMinds.SwissArmyKnife.Tests
{
    using SCM.SwissArmyKnife.Extensions;

    public class StreamExtensionsTests
    {
        [Fact]
        public async Task AsMemoryStream_WillCopyAStreamAsANewMemoryStream_WithPosition0()
        {
            // Arrange
            using var stream = new MemoryStream();
            stream.Write(new byte[]{1, 2, 3});
            stream.Position = 1;

            // Act
            var asMemoryStream = await stream.AsMemoryStream();

            // Assert - the new stream only contains the leftovers of the old stream
            asMemoryStream.Position.Should().Be(0);
            asMemoryStream.ToArray().Should().BeEquivalentTo(new byte[] {2, 3});
        }
        
        [Fact]
        public async Task ToByteArray_WillConvertAMemoryStream_ToAByteArray()
        {
            
            // Arrange
            using var stream = new MemoryStream();
            stream.Write(new byte[]{1, 2, 3});
            stream.Position = 1;

            // Act
            var returnedByteArray = await stream.ToByteArray();

            // Assert - the byte array contains the leftovers of the old stream
            returnedByteArray.Should().BeEquivalentTo(new byte[] {2, 3});
        }
        
        [Fact]
        public void ContentToString_WillUse_SpecifiedEncoding()
        {
            // Arrange
            var stringToPutIn = "foo";
            using var stream = new MemoryStream();

            // Act
            stream.Write(Encoding.ASCII.GetBytes(stringToPutIn));
            stream.Position = 0;
            var stringFromStream = stream.ContentToString(Encoding.ASCII);

            // Assert - the returned string is the same
            stringFromStream.Should().Be("foo");
        }
        
        [Fact]
        public void ContentToString_WillDefault_ToUtf8Encoding()
        {
            // Arrange
            var unicodeString = "℁℁℁";
            using var stream = new MemoryStream();

            // Act
            stream.Write(Encoding.UTF8.GetBytes(unicodeString));
            stream.Position = 0;
            var stringFromStream = stream.ContentToString();

            // Assert - the returned string is the same
            stringFromStream.Should().Be("℁℁℁");
        }
    }
}