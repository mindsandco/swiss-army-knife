using System;
using System.IO;
using FluentAssertions;
using Xunit;

namespace ScadaMinds.SwissArmyKnife.Tests
{
    using SCM.SwissArmyKnife.Extensions;

    public class MemoryStreamExtensionsTests
    {
        
        
        [Fact]
        public void CloneEntireStream_WillCloneEntireStream()
        {
            // Arrange
            using var originalMemoryStream = new MemoryStream();
            originalMemoryStream.Write(new byte[]{1, 2, 3});
            
            // Act
            var clonedStream = originalMemoryStream.CloneEntireStream();
            
            // Assert - the contents of the two streams are identical
            clonedStream.ToArray().Should().BeEquivalentTo(originalMemoryStream.ToArray());
        }
        
        [Fact]
        public void CloneEntireStream_WillCloneEntireStream_RegardlessOfPosition()
        {
            // Arrange
            using var originalMemoryStream = new MemoryStream();
            originalMemoryStream.Write(new byte[]{1, 2, 3});
            originalMemoryStream.Position = 1;
            
            // Act
            var clonedStream = originalMemoryStream.CloneEntireStream();
            
            // Assert - the contents of the two streams are identical
            clonedStream.ToArray().Should().BeEquivalentTo(new byte[]{1,2,3});
            clonedStream.Position.Should().Be(0);
        }
        
        [Fact]
        public void CloneEntireStream_WillNotMovePosition_OfOriginalStream()
        {
            // Arrange
            using var originalMemoryStream = new MemoryStream();
            originalMemoryStream.Write(new byte[]{1, 2, 3});
            originalMemoryStream.Position = 1;
            
            // Act
            var _ = originalMemoryStream.CloneEntireStream();
            
            // Assert - the position has not moved
            originalMemoryStream.Position.Should().Be(1);
        }
    }
}