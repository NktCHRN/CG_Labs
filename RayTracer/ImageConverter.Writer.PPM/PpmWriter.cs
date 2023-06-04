using ImageConverter.Common;

namespace ImageConverter.Writer.PPM;
public sealed class PpmWriter : IImageWriter
{
    public string FileExtension => "ppm";

    public byte[] Write(Image image)
    {
        using var stream = new MemoryStream();
        using var writer = new StreamWriter(stream);
        writer.WriteLine("P3");
        writer.WriteLine($"{image.Width} {image.Height}");
        writer.WriteLine(byte.MaxValue);

        for (var i = 0; i < image.Height; i++)
        {
            for (var j = 0; j < image.Width; j++)
            {
                writer.Write($"{image[i,j].Red} ");
                writer.Write($"{image[i, j].Green} ");
                writer.Write($"{image[i, j].Blue} ");
            }
            writer.WriteLine();
        }

        return stream.ToArray();
    }
}
