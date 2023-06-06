using Common;
using ImageConverter.Common;

namespace ImageConverter.Reader.BMP;
public sealed class BmpReader : IImageReader
{
    public string FileExtension => "bmp";

    public bool CanRead(byte[] byteArray)
    {
        return byteArray[0] is (byte)'B' && byteArray[1] is (byte)'M';
    }

    public Image Read(byte[] byteArray)
    {
        using var stream = new MemoryStream(byteArray);
        using var reader = new BinaryReader(stream);
        reader.BaseStream.Seek(18, SeekOrigin.Begin);
        var width = reader.ReadInt32();
        var height = reader.ReadInt32();
        reader.BaseStream.Seek(2, SeekOrigin.Current);
        var bpp = reader.ReadInt16();

        if (bpp is not 24 or 32)
        {
            throw new NotSupportedException($"You are trying to open {bpp}bit BMP file, but only BMP24 and BMP32 are supported");
        }

        var hasAlpha = bpp == 32;

        reader.BaseStream.Seek(54, SeekOrigin.Begin);
        var image = new Image(width, height, hasAlpha);

        var bytesPerPixel = bpp / 8;
        var rowSize = width * bytesPerPixel;
        var paddingSize = rowSize % 4;
        if (paddingSize != 0)
        {
            paddingSize = 4 - paddingSize;
        }

        for (var i = height - 1; i >= 0; i--)
        {
            for (var j = 0; j < width; j++)
            {
                var blue = reader.ReadByte();
                var green = reader.ReadByte();
                var red = reader.ReadByte();
                var alpha = hasAlpha ? reader.ReadByte() : byte.MaxValue;

                image[i, j] = Color.FromArgb(alpha, red, green, blue);
            }

            // Skip any padding bytes
            if (paddingSize > 0)
            {
                reader.BaseStream.Seek(paddingSize, SeekOrigin.Current);
            }
        }

        return image;
    }
}
