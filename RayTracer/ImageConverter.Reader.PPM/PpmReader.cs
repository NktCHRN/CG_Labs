using ImageConverter.Common;

namespace ImageConverter.Reader.PPM;
public sealed class PpmReader : IImageReader
{
    public string ImageFormat => "PPM";

    public bool CanRead(string fileName)
    {
        using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(stream);
        var format = reader.ReadLine()?.Trim();
        return format?.First() is 'P' && int.TryParse(format?[1..], out _);
    }

    public Result<Image, string> Read(string fileName)
    {
        using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(stream);
        var magicNumber = reader.ReadLine()!.Trim();
        if (magicNumber != "P3")
        {
            return Result<Image, string>.Failure("Invalid PPM file format. Only P3 format is supported.");
        }

        // Ignore comments
        string line;
        do
        {
            line = reader.ReadLine()!;
        } while (line.StartsWith("#"));

        // Read image width, height, and maximum pixel value
        var dimensions = line.Trim().Split(' ');
        var width = int.Parse(dimensions[0]);
        var height = int.Parse(dimensions[1]);

        var maxValue = short.Parse(reader.ReadLine()!.Trim());
        if (maxValue > byte.MaxValue)
        {
            return Result<Image, string>.Failure($"PPM with max values more than {byte.MaxValue} are currently not supported");
        }

        // Read pixel data
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

        return Result<Image, string>.Success(image);
    }

    private static byte RoundToByteMaxValue(byte colorValue, short maxValue)
    {
        return (byte)Math.Round((double)colorValue / maxValue * byte.MaxValue, MidpointRounding.AwayFromZero);
    }
}
