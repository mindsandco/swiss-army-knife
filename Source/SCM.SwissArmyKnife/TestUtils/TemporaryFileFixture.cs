using System;
using System.Diagnostics;
using System.IO;

namespace SCM.SwissArmyKnife.TestUtils
{
    /// <summary>
    /// Creates 0-byte, temporary file in the system temp location.
    /// When disposing this object, the file is deleted.
    /// </summary>
    public class TemporaryFileFixture : IDisposable
    {
        /// <summary>
        /// Create a TemporaryFileFixture.
        /// This method performs IO.
        /// </summary>
        /// <returns>A TemporaryFileFixture for a temporary file.</returns>
        public static TemporaryFileFixture Create() => new TemporaryFileFixture();

        /// <summary>
        /// Gets the path to the temporary file that this fixture governs.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Gets the FileInfo field that wraps the field that this fixture governs.
        /// </summary>
        public FileInfo FileInfo { get; }

        private TemporaryFileFixture()
        {
            this.Path = System.IO.Path.GetFullPath(System.IO.Path.GetTempFileName());
            this.FileInfo = new FileInfo(this.Path);
            Debug.Assert(this.FileInfo.Exists == true, "File should exist");
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TemporaryFileFixture"/> class.
        /// </summary>
        ~TemporaryFileFixture() => this.ReleaseUnmanagedResources();

        /// <summary>
        /// Deletes the temporary file that this fixture governed.
        /// </summary>
        public void Dispose()
        {
            this.ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        private void ReleaseUnmanagedResources() => this.FileInfo.Delete();
    }
}
