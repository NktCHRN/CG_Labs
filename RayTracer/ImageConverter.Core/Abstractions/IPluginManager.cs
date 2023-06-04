using ImageConverter.Common;

namespace ImageConverter.Core.Abstractions;
public interface IPluginManager
{
    string PluginsFolderName { get; set; }
    void UpdatePlugins();
    IImageReader? GetReaderForFile(string fileName);
    IImageWriter? GetWriterForFileExtension(string type);
}
