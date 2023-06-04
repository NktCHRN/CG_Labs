using System.Drawing;

namespace ImageConverter.Core.Abstractions;
public interface IImageReader
{
    string ImageFormat { get; }
    bool CanRead(string fileName);
    Color[,] Read(string fileName);
}
