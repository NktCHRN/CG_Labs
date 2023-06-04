using ImageConverter.Common;

namespace ImageConverter.Core.Abstractions;
public interface IPluginManager
{
    string FolderName { get; set; }
    void UpdatePlugins();
    IImageReader? GetReaderForFile(string fileName);
    IImageWriter? GetWriterForFileExtension(string type);
}
