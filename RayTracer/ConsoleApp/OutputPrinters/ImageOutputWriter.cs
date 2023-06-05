using ConsoleApp.Abstractions;
using ImageConverter.Common;
using ImageConverter.Core;
using ImageConverter.Core.Abstractions;

namespace ConsoleApp.OutputPrinters;
public sealed class ImageOutputWriter : IOutputWriter
{
    private readonly IPluginManager _pluginManager;

    public string OutputFolder {get; set;} = "Output";

    public ImageOutputWriter(IPluginManager pluginManager)
    {
        _pluginManager = pluginManager;
        _pluginManager.UpdatePlugins();
    }

    public void Write(float[,] matrix)
    {
        const string fileName = "output.bmp";   // change it in 4th lab
        var format = Path.GetExtension(fileName);
        ValidationMethods.ValidateFileExtension(format);
        format = HelperMethods.FormatFileExtension(format);

        _ = Directory.CreateDirectory(OutputFolder);

        int width = matrix.GetLength(1);
        int height = matrix.GetLength(0);

        var image = new Image(width, height);

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                float val = matrix[i, j] * 255;

                byte byteValue = 0;
                if (val > 0)
                    byteValue = Convert.ToByte(val);

                image[i, j] = Color.FromRgb(byteValue, byteValue, byteValue);
            }
        }

        var writer = _pluginManager.GetWriterForFileExtension(format) ??
            throw new NotSupportedException($"Your output format {format} is currently not supported for writing. " +
                $"Supported formats: {string.Join(", ", _pluginManager.SupportedWriterExtensions)}");
        var byteArray = writer.Write(image);

        var filePath = Path.Combine(OutputFolder, fileName);
        File.WriteAllBytes(filePath, byteArray);
    }
}
