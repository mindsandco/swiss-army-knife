using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using SCM.SwissArmyKnife.Extensions;
using SCM.SwissArmyKnife.Files;
using Xunit;

namespace SCM.SwissArmyKnife.Test.Files
{
    public class FileLocatorTests
    {
        [Fact]
        public void TryGetSolutionDirectory_ShouldFindSolutionDirectory_WhenNotGivenAnyDirectory()
        {
            var startingDirectory = FileLocator.TryGetSolutionDirectoryInfo();

            startingDirectory.GetFiles().Should().ContainSingle(f => f.Name == "SwissArmyKnife.sln");
        }

        [Fact]
        public void TryGetSolutionDirectory_ShouldThrowErrorWhenGivenDirectoryWithoutSolution()
        {
            Action action = () => FileLocator.TryGetSolutionDirectoryInfo("/");

            action.Should().Throw<IOException>().WithMessage("*Unable to find*");
        }

        [Fact]
        public void GetFileStartingAtSolutionDirectory_ShouldReturnFileInfoForFile_WhenGivenCorrectFileName()
        {
            var fileInfo = FileLocator.GetFileStartingAtSolutionDirectory("global.json");
            fileInfo.Name.Should().Be("global.json");
            fileInfo.Exists.Should().BeTrue();
        }

        [Fact]
        public void GetFileStartingAtSolutionDirectory_ShouldReturnFileInfoForFile_WhenGivenCorrectPath()
        {
            var fileInfo = FileLocator.GetFileStartingAtSolutionDirectory("Images", "Icon.png");
            fileInfo.Name.Should().Be("Icon.png");
            fileInfo.Exists.Should().BeTrue();
        }

        [Fact]
        public void GetFileStartingAtSolutionDirectory_ShouldReturnThrowError_IfPathCouldNotBeFound()
        {
            Action action = () => FileLocator.GetFileStartingAtSolutionDirectory("Images", "NOT_A_REAL_PATH");
            action.Should().Throw<IOException>().WithMessage("Could not find file at path*");
        }
    }
}
