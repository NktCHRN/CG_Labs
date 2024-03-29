﻿using Common;
using ImageConverter.Common;

namespace ImageConverter.Writer.BMP;
public sealed class BmpWriter : IImageWriter
{
    public string FileExtension => "bmp";

    private const int _bmpHeaderSize = 14;
    private const int _dibHeaderSize = 40;
    private const int _totalHeaderSize = _bmpHeaderSize + _dibHeaderSize;

    public byte[] Write(Image image)
    {
        var bytesPerPixel = image.HasAlpha ? 4 : 3; // Assuming 24-bit color depth (BPP)

        var rowSize = image.Width * bytesPerPixel;
        var paddingSize = rowSize % 4 != 0 ? 4 - (rowSize % 4) : 0;

        var dataSize = (rowSize + paddingSize) * image.Height;
        var fileSize = _totalHeaderSize + dataSize;

        using var stream = new MemoryStream(fileSize);      // important for GetBuffer usage in the end
        using var writer = new BinaryWriter(stream);

        // Write file header
        writer.Write('B');
        writer.Write('M');
        writer.Write(fileSize);
        writer.Write(0); // Reserved
        writer.Write(_totalHeaderSize); // Offset to pixel data

        // Write DIB header
        writer.Write(_dibHeaderSize); // DIB header size
        writer.Write(image.Width);
        writer.Write(image.Height);
        writer.Write((short)1); // Number of color planes
        writer.Write((short)(bytesPerPixel * 8)); // BPP
        writer.Write(0); // Compression method
        writer.Write(dataSize); // Size of raw bitmap data
        writer.Write(2835); // Horizontal resolution (pixels per meter)
        writer.Write(2835); // Vertical resolution (pixels per meter)
        writer.Write(0); // Number of colors in the color palette
        writer.Write(0); // Number of important colors

        for (var i = image.Height - 1; i >= 0; i--)
        {
            for (var j = 0; j < image.Width; j++)
            {
                var pixelValue = image[i, j];
                writer.Write(pixelValue.B);
                writer.Write(pixelValue.G);
                writer.Write(pixelValue.R);

                if (image.HasAlpha)
                {
                    writer.Write(pixelValue.Alpha);
                }
            }

            // Write padding bytes
            for (var j = 0; j < paddingSize; j++)
            {
                writer.Write((byte)0);
            }
        }

        return stream.GetBuffer();
    }
}
