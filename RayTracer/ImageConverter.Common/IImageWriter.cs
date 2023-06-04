namespace ImageConverter.Common;
public interface IImageWriter
{
    string ImageFormat { get; }
    void Write(string fileName, Image image);
}
