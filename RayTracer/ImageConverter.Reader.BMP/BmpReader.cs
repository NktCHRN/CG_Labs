using ImageConverter.Common;

namespace ImageConverter.Reader.BMP;
public sealed class BmpReader : IImageReader
{
    public string ImageFormat => throw new NotImplementedException();

    public bool CanRead(string fileName)
    {
        throw new NotImplementedException();
    }

    public Color[,] Read(string fileName)
    {
        throw new NotImplementedException();
    }
}
