using ImageConverter.Common;

namespace ImageConverter.Core.Abstractions;
public interface IPluginManager
{
    void UpdatePlugins(string folderName = "Plugins");
    IImageReader? GetReaderForFile(string fileName);
    IImageWriter? GetWriterForFileExtension(string type);
}
