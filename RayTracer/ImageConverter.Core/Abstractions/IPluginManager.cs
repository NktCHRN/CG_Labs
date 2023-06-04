using ImageConverter.Common;

namespace ImageConverter.Core.Abstractions;
public interface IPluginManager
{
    IReadOnlyCollection<IImageReader> Readers { get; }
    IReadOnlyCollection<IImageWriter> Writers { get; }
    void UpdatePlugins(string folderName = "Plugins");
    IImageReader? GetReaderForType(string type);
    IImageWriter? GetWriterForType(string type);
}
