namespace ImageConverter.Common;
public interface IImageReader
{
    string ImageFormat { get; }
    bool CanRead(string fileName);
    Result<Color[,], string> Read(string fileName);
}
