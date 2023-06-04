using System.Drawing;

namespace ImageConverter.Core.Abstractions;
public interface IImageWriter
{
    string ImageFormat { get; }
    void Write(Stream stream, Color[,] bitMap);
}
