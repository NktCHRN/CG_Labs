namespace ImageConverter.Common;
public interface IImageReader
{
    string FileExtension { get; }
    bool CanRead(byte[] byteArray);
    Result<Image, string> Read(byte[] byteArray);
}
