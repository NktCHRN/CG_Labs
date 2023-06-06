using Common;
using ImageConverter.Common;

namespace ImageConverter.Reader.PPM;
public sealed class PpmReader : IImageReader
{
    public string FileExtension => "ppm";

    public bool CanRead(byte[] byteArray)
    {
        return (char)byteArray[0] is 'P' 
            && (char)byteArray[1] is >= '1' and <= '6' 
            && (char)byteArray[2] is ' ' or '\n' or '\r';
    }

    public Image Read(byte[] byteArray)
    {
        using var stream = new MemoryStream(byteArray);
        using var reader = new StreamReader(stream);
        var magicNumber = reader.ReadLine()!.Trim();
        if (magicNumber != "P3")
        {
            throw new NotSupportedException($"You are trying to open P{magicNumber} PPM file, but only P3 is supported");
        }

        // Ignore comments
        string line;
        do
        {
            line = reader.ReadLine()!;
        } while (line.StartsWith("#"));

        var dimensions = line.Trim().Split(' ');
        var width = int.Parse(dimensions[0]);
        var height = int.Parse(dimensions[1]);

        var maxValue = short.Parse(reader.ReadLine()!.Trim());
        if (maxValue > byte.MaxValue)
        {
            throw new NotSupportedException($"PPM with max values more than {byte.MaxValue} are currently not supported");
        }

        var image = new Image(width, height);
        var colorNumbers = reader.ReadToEnd()
            .ReplaceLineEndings()
            .Replace(Environment.NewLine, " ")
            .Split(' ')
            .Where(c => !string.IsNullOrWhiteSpace(c))
            .ToList();
        int currentColorNumber = 0;
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var r = RoundToByteMaxValue(byte.Parse(colorNumbers[currentColorNumber++]), maxValue);
                var g = RoundToByteMaxValue(byte.Parse(colorNumbers[currentColorNumber++]), maxValue);
                var b = RoundToByteMaxValue(byte.Parse(colorNumbers[currentColorNumber++]), maxValue);
                image[i,j] = Color.FromRgb(r, g, b);
            }
        }

        return image;
    }

    private static byte RoundToByteMaxValue(byte colorValue, short maxValue)
    {
        return (byte)Math.Round((double)colorValue / maxValue * byte.MaxValue, MidpointRounding.AwayFromZero);
    }
}
