namespace ImageConverter.Common;
public interface IImageWriter
{
    string FileExtension { get; }
    void Write(string fileName, Image image);
}
