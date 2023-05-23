namespace P01.Stream_Progress.Models
{
    using Interfaces;
    public class StreamProgressInfo
    {
        private IFile file;

        public StreamProgressInfo(IFile file)
        {
            this.file = file;
        }

        public int CalculateCurrentPercent()
        {
            return file.BytesSent * 100 / file.Length;
        }
    }
}
