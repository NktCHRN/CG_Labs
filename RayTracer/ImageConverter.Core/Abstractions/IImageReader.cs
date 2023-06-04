using System.Drawing;

namespace ImageConverter.Core.Abstractions;
public interface IImageReader
{
    string ImageFormat { get; }
    bool CanRead(Stream stream);
    Color[,] Read(Stream stream);
}
