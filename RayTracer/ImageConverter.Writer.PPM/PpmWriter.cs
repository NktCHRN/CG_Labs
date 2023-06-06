using Common;
using ImageConverter.Common;
using System.Text;

namespace ImageConverter.Writer.PPM;
public sealed class PpmWriter : IImageWriter
{
    public string FileExtension => "ppm";

    public byte[] Write(Image image)
    {
        var builder = new StringBuilder();
        builder.AppendLine("P3");
        builder.AppendLine($"{image.Width} {image.Height}");
        builder.AppendLine(byte.MaxValue.ToString());

        for (var i = 0; i < image.Height; i++)
        {
            for (var j = 0; j < image.Width; j++)
            {
                builder.Append($"{image[i,j].R} ");
                builder.Append($"{image[i, j].G} ");
                builder.Append($"{image[i, j].B} ");
            }
            builder.AppendLine();
        }

        return Encoding.ASCII.GetBytes(builder.ToString());
    }
}
