using ImageConverter.Common;

namespace ImageConverter.Reader.PPM;
public sealed class PpmReader : IImageReader
{
    public string ImageFormat => throw new NotImplementedException();

    public bool CanRead(string fileName)
    {
        throw new NotImplementedException();
    }

    public Result<Color[,], string> Read(string fileName)
    {
        throw new NotImplementedException();
    }
}
