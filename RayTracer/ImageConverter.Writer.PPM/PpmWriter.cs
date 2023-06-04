using ImageConverter.Common;
using System.Drawing;

namespace ImageConverter.Writer.PPM;
public sealed class PpmWriter : IImageWriter
{
    public string ImageFormat => throw new NotImplementedException();

    public void Write(string fileName, Color[,] bitMap)
    {
        throw new NotImplementedException();
    }
}
