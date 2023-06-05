using ImageConverter.Core.Abstractions;
using static ImageConverter.Core.HelperMethods;

namespace ImageConverter.Core;
public sealed class ImageConverter
{
    private readonly IPluginManager _pluginManager;

    public string OutputFolder { get; set; } = "Output";

    public ImageConverter(IPluginManager pluginManager)
    {
        _pluginManager = pluginManager;
        _pluginManager.UpdatePlugins();
    }

    public FileInfo Convert(string oldFilePath, string resultFormat)
    {
        _ = Directory.CreateDirectory(OutputFolder);

        oldFilePath = FormatFileName(oldFilePath);
        resultFormat = FormatFileExtension(resultFormat);
        var oldByteArray = File.ReadAllBytes(oldFilePath);

        var reader = _pluginManager.GetReaderForByteArray(oldByteArray) ??
            throw new NotSupportedException($"Your file {oldFilePath} format is currently not supported for reading. " +
                $"Supported formats: {string.Join(", ", _pluginManager.SupportedReaderExtensions)}");
        var oldFileFormat = FormatFileExtension(reader.FileExtension);
        var image = reader.Read(oldByteArray);

        var writer = _pluginManager.GetWriterForFileExtension(resultFormat) ??
            throw new NotSupportedException($"Your goal format {resultFormat} is currently not supported for writing. " +
                $"Supported formats: {string.Join(", ", _pluginManager.SupportedWriterExtensions)}");
        var newByteArray = writer.Write(image);

        var newFileName = Path.GetFileName(oldFilePath);
        newFileName = ChangeFileExtension(newFileName, oldFileFormat, resultFormat);
        var newFilePath = Path.Combine(OutputFolder, newFileName);
        File.WriteAllBytes(newFilePath, newByteArray);

        return new FileInfo(newFilePath);
    }
}
