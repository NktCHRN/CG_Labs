using ImageConverter.Common;

namespace ImageConverter.Writer.PPM;
public sealed class PpmWriter : IImageWriter
{
    public string ImageFormat => "PPM";

    public void Write(string fileName, Image image)
    {
        using var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
        using var reader = new StreamWriter(stream);
        reader.WriteLine("P3");
        reader.WriteLine($"{image.Width} {image.Height}");
        reader.WriteLine(byte.MaxValue);

        for (var i = 0; i < image.Height; i++)
        {
            for (var j = 0; j < image.Width; j++)
            {
                reader.Write($"{image[i,j].Red} ");
                reader.Write($"{image[i, j].Green} ");
                reader.Write($"{image[i, j].Blue} ");
            }
            reader.WriteLine();
        }
    }
}
