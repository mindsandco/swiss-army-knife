using System;
using System.IO;
using FluentAssertions;
using SCM.SwissArmyKnife.TestUtils;
using Xunit;

namespace SCM.SwissArmyKnife.Test.TestUtils
{
    public class TemporaryDirectoryFixtureTests
    {
        [Fact]
        public void Create_ShouldCreateTemporaryDirectory()
        {
            // See that temporary file wraps an existing file
            using var temporaryDirectory = TemporaryDirectoryFixture.Create();
            temporaryDirectory.DirectoryInfo.FullName.Should().NotBeEmpty();
            temporaryDirectory.DirectoryInfo.Exists.Should().BeTrue();
        }

        [Fact]
        public void Dispose_ShouldDeleteTheDirectory()
        {
            // See that temporary file wraps an existing file
            DirectoryInfo temporaryDirectoryInfo;
            using (var temporaryDirectory = TemporaryDirectoryFixture.Create())
            {
                temporaryDirectory.DirectoryInfo.Exists.Should().BeTrue();
                temporaryDirectoryInfo = temporaryDirectory.DirectoryInfo;
            }

            temporaryDirectoryInfo.Exists.Should().BeFalse();
        }
    }
}
