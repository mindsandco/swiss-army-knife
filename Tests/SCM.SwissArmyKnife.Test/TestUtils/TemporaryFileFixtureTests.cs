using System;
using System.IO;
using FluentAssertions;
using SCM.SwissArmyKnife.TestUtils;
using Xunit;

namespace SCM.SwissArmyKnife.Test.TestUtils
{
    public class TemporaryFileFixtureTests
    {
        [Fact]
        public void Create_ShouldCreateTemporaryFile()
        {
            // See that temporary file wraps an existing file
            using var temporaryFile = TemporaryFileFixture.Create();
            temporaryFile.FileInfo.FullName.Should().NotBeEmpty();
            temporaryFile.FileInfo.Exists.Should().BeTrue();
            temporaryFile.FileInfo.Length.Should().Be(0);
        }

        [Fact]
        public void Dispose_ShouldDeleteTheFile()
        {
            // See that temporary file wraps an existing file
            FileInfo temporaryFileInfo;
            using (var temporaryFile = TemporaryFileFixture.Create())
            {
                temporaryFile.FileInfo.Exists.Should().BeTrue();
                temporaryFileInfo = temporaryFile.FileInfo;
            }

            temporaryFileInfo.Exists.Should().BeFalse();
        }
    }
}
