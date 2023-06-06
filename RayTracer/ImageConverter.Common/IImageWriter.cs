using Common;

namespace ImageConverter.Common;
public interface IImageWriter
{
    string FileExtension { get; }
    byte[] Write(Image image);
}
