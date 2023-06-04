using ImageConverter.Common;

namespace ImageConverter.Core.Abstractions;
public interface IPluginManager
{
    IEnumerable<string> SupportedReaderExtensions { get; }
    IEnumerable<string> SupportedWriterExtensions { get; }
    string PluginsFolderName { get; set; }
    void UpdatePlugins();
    IImageReader? GetReaderForByteArray(byte[] byteArray);
    IImageWriter? GetWriterForFileExtension(string type);
}
