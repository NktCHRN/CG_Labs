namespace ImageConverter.Common;
public interface IImageReader
{
    string ImageFormat { get; }
    bool CanRead(string fileName);
    Result<Image, string> Read(string fileName);
}
