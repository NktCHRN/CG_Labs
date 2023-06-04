using ImageConverter.Common;
using System.Drawing;

namespace ImageConverter.Writer.BMP;
public sealed class BmpWriter : IImageWriter
{
    public string ImageFormat => throw new NotImplementedException();

    public void Write(string fileName, Color[,] bitMap)
    {
        throw new NotImplementedException();
    }
}

