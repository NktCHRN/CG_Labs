using ImageConverter.Core.Abstractions;
using static ImageConverter.Core.HelperMethods;

namespace ImageConverter.Core;
public sealed class ImageConverter
{
    private readonly IPluginManager _pluginManager;

    public string InputFolder { get; set; } = "Input";
    public string OutputFolder { get; set; } = "Output";

    public ImageConverter(IPluginManager pluginManager)
    {
        _pluginManager = pluginManager;
        _pluginManager.UpdatePlugins();
    }

    public FileInfo Convert(string fileName, string resultFormat)
    {
        _ = Directory.CreateDirectory(InputFolder);
        _ = Directory.CreateDirectory(OutputFolder);

        fileName = FormatFileName(fileName);
        resultFormat = FormatFileExtension(resultFormat);
        var oldFilePath = Path.Combine(InputFolder, fileName);
        var oldByteArray = File.ReadAllBytes(oldFilePath);

        var reader = _pluginManager.GetReaderForByteArray(oldByteArray) ??
            throw new NotSupportedException($"Your file {fileName} format is currently not supported for reading. " +
                $"Supported formats: {string.Join(", ", _pluginManager.SupportedReaderExtensions)}");
        var oldFileFormat = FormatFileExtension(reader.FileExtension);
        var image = reader.Read(oldByteArray);

        var writer = _pluginManager.GetWriterForFileExtension(resultFormat) ??
            throw new NotSupportedException($"Your file {fileName} format is currently not supported for writing. " +
                $"Supported formats: {string.Join(", ", _pluginManager.SupportedWriterExtensions)}");
        var newByteArray = writer.Write(image);

        fileName = ChangeFileExtension(fileName, oldFileFormat, resultFormat);
        var newFilePath = Path.Combine(OutputFolder, fileName);
        File.WriteAllBytes(newFilePath, newByteArray);

        return new FileInfo(newFilePath);
    }
}
