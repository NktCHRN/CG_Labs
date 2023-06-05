namespace ImageConverter.Common;
public interface IImageReader
{
    string FileExtension { get; }
    bool CanRead(byte[] byteArray);
    Image Read(byte[] byteArray);
}
