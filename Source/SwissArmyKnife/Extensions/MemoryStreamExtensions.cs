namespace SCM.SwissArmyKnife.Extensions
{
    using System.IO;

    public static class MemoryStreamExtensions
    {
        /// <summary>
        /// Take a MemoryStream and clones it from start to end.
        /// It will leave the MemoryStream at the position where it was previously.
        /// The new stream will start at position 0.
        /// </summary>
        /// <returns></returns>
        public static MemoryStream CloneEntireStream(this MemoryStream streamToClone)
        {
            var newStream = new MemoryStream();
            var originalPosition = streamToClone.Position;

            streamToClone.Position = 0;
            streamToClone.CopyTo(newStream);
            streamToClone.Position = originalPosition;

            newStream.Position = 0;
            return newStream;
        }
    }
}