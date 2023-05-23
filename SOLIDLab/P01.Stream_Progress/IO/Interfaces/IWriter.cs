namespace P01.Stream_Progress.IO.Interfaces
{
    public interface IWriter
    {
        void Write(object value);
        void WriteLine(object value);
    }
}
