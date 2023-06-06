using Common;
using ConsoleApp.Abstractions;
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

    public void Write(Image image)
    {
        const string fileName = "output.bmp";   // change it in 4th lab
        var format = Path.GetExtension(fileName);
        ValidationMethods.ValidateFileExtension(format);
        format = HelperMethods.FormatFileExtension(format);

        _ = Directory.CreateDirectory(OutputFolder);

        var writer = _pluginManager.GetWriterForFileExtension(format) ??
            throw new NotSupportedException($"Your output format {format} is currently not supported for writing. " +
                $"Supported formats: {string.Join(", ", _pluginManager.SupportedWriterExtensions)}");
        var byteArray = writer.Write(image);

        var filePath = Path.Combine(OutputFolder, fileName);        // consider that path may be absolute
        File.WriteAllBytes(filePath, byteArray);
    }
}
