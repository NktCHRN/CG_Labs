using ImageConverter.Common;

namespace ImageConverter.Reader.BMP;
public sealed class BmpReader : IImageReader
{
    public string FileExtension => "bmp";

    public bool CanRead(string fileName)
    {
        using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        using var reader = new BinaryReader(stream);

        byte byte1 = reader.ReadByte();
        byte byte2 = reader.ReadByte();

        return byte1 is (byte)'B' && byte2 is (byte)'M';
    }

    public Result<Image, string> Read(string fileName)
    {
        using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        using var reader = new BinaryReader(stream);
        reader.BaseStream.Seek(18, SeekOrigin.Begin);
        var width = reader.ReadInt32();
        var height = reader.ReadInt32();
        reader.BaseStream.Seek(2, SeekOrigin.Current);
        var bpp = reader.ReadInt16();

        if (bpp is not 24 or 32)
        {
            return Result<Image, string>.Failure($"You are trying to open {bpp}bit BMP file, but only BMP24 and BMP32 are supported");
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

        return Result<Image, string>.Success(image);
    }
}
