using System.IO;


namespace SCM.SwissArmyKnife.Extensions
{
    /// <summary>
    /// Methods for working with more efficiently with MemoryStreams.
    /// </summary>
    public static class MemoryStreamExtensions
    {
        /// <summary>
        /// Take a MemoryStream and clones it from start to end.
        /// It will leave the MemoryStream at the position where it was previously.
        /// The new stream will start at position 0.
        /// </summary>
        /// <returns>A copy of the entire <paramref name="sourceStream"/>.</returns>
        public static MemoryStream CloneEntireStream(this MemoryStream sourceStream)
        {
            var newStream = new MemoryStream();
            var originalPosition = sourceStream.Position;

            sourceStream.Position = 0;
            sourceStream.CopyTo(newStream);
            sourceStream.Position = originalPosition;

            newStream.Position = 0;
            return newStream;
        }
    }
}
