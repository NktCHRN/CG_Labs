namespace ImageConverter.Common;
public interface IImageReader
{
    string ImageFormat { get; }
    bool CanRead(string fileName);
    Color[,] Read(string fileName);
}
