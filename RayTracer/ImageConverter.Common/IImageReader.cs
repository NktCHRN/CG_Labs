namespace ImageConverter.Common;
public interface IImageReader
{
    string FileExtension { get; }
    bool CanRead(string fileName);
    Result<Image, string> Read(string fileName);
}
