using System;
using System.Diagnostics;
using System.IO;

namespace SCM.SwissArmyKnife.TestUtils
{
    /// <summary>
    /// Creates an empty, temporary directory located at "{SYSTEM_TMP_DIR}/{guid}"
    /// When disposing this object, the directory and all contents are deleted.
    /// </summary>
    public class TemporaryDirectoryFixture : IDisposable
    {
        /// <summary>
        /// Creates a TemporaryDirectoryFixture and the underlying directory.
        /// This class performs IO.
        /// </summary>
        /// <returns>The TemporaryDirectoryFixture. </returns>
        public static TemporaryDirectoryFixture Create() => new TemporaryDirectoryFixture();

        /// <summary>
        /// Gets the path to the temporary directory.
        /// </summary>
        public string DirectoryPath { get; }

        /// <summary>
        /// Gets the DirectoryInfo that wraps the underlying temporary directory.
        /// </summary>
        public DirectoryInfo DirectoryInfo { get; }

        private TemporaryDirectoryFixture()
        {
            var guid = Guid.NewGuid();
            var path = Path.Join(Path.GetTempPath(), guid.ToString());
            this.DirectoryPath = Path.GetFullPath(path);

            this.DirectoryInfo = new DirectoryInfo(this.DirectoryPath);
            this.DirectoryInfo.Create();
            Debug.Assert(this.DirectoryInfo.Exists, "Directory should exist");
        }


        /// <summary>
        /// Recursively deletes the temporary directory and all its content.
        /// </summary>
        public void Dispose()
        {
            this.ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TemporaryDirectoryFixture"/> class.
        /// </summary>
        ~TemporaryDirectoryFixture() => this.ReleaseUnmanagedResources();

        private void ReleaseUnmanagedResources()
        {
            try
            {
                this.DirectoryInfo.Delete(recursive: true);
            }
            catch (IOException)
            {
                // Sometimes we see test failures due to use being unable to remove the directories.
                // As this is just a "clean up after yourself to be nice to the filesystem" we don't need to fail on it.
                Console.WriteLine($"Was unable to remove directory {this.DirectoryInfo.FullName}");
            }
        }
    }
}
