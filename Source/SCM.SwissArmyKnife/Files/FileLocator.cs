using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SCM.SwissArmyKnife.Files
{
    /// <summary>
    /// Utilities to help locating files.
    /// </summary>
    public static class FileLocator
    {
        /// <summary>
        /// This method will search parent directories from <paramref name="startingPath"/> until it reaches a directory
        /// with a '.sln' file in it and return that.
        /// This is useful if you e.g. want to build a file path starting from your solution directory.
        /// The implementation is inspired by https://stackoverflow.com/questions/19001423/getting-path-to-the-parent-folder-of-the-solution-file-using-c-sharp .
        /// </summary>
        /// <param name="startingPath">The path to start searching from.</param>
        /// <returns>The DirectoryInfo of the solution file.</returns>
        /// <exception cref="IOException">Will throw an IOException if no solution file could be found.</exception>
        public static DirectoryInfo TryGetSolutionDirectoryInfo(string? startingPath = null)
        {
            var pathToStartFrom = Path.GetFullPath(startingPath ?? Directory.GetCurrentDirectory());

            return TryGetSolutionDirectoryInfoInternal(pathToStartFrom) ?? throw new IOException(
                $"Unable to find a path to the solution directory, when searching upwards from '{pathToStartFrom}'");
        }

        /// <summary>
        /// Finds a file by starting to search from the nearest solution directory (looking from Directory.GetCurrentDirectory()).
        /// It will join the paths given and then return a FileInfo with the path.
        /// </summary>
        /// <param name="partsToJoin">The separate path parts to be given to Path.Join().</param>
        /// <returns>A fileInfo if the file could be found.</returns>
        /// <exception cref="IOException">If the file was not found this will throw an IOException.</exception>
        public static FileInfo GetFileStartingAtSolutionDirectory(params string[] partsToJoin)
        {
            var startingPath = Directory.GetCurrentDirectory();
            var solutionDir = TryGetSolutionDirectoryInfo(startingPath);

            var pathSegments = new List<string>(capacity: partsToJoin.Length + 1) {solutionDir.FullName};
            pathSegments.AddRange(partsToJoin);

            var joinedPath = Path.Join(pathSegments.ToArray());
            var fileInfo = new FileInfo(joinedPath);

            if (!fileInfo.Exists)
            {
                throw new IOException($"Could not find file at path '{fileInfo.FullName}'");
            }

            return fileInfo;
        }

        private static DirectoryInfo? TryGetSolutionDirectoryInfoInternal(string? currentPath)
        {
            // CurrentPath is only null if we're at the root of the filesystem
            if (currentPath == null)
            {
                return null;
            }


            var directory = new DirectoryInfo(currentPath);

            return directory.GetFiles("*.sln").Any()
                ? directory
                : TryGetSolutionDirectoryInfoInternal(directory.Parent?.FullName);
        }
    }
}
