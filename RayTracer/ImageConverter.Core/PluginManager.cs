using ImageConverter.Common;
using ImageConverter.Core.Abstractions;

namespace ImageConverter.Core;
public sealed class PluginManager : IPluginManager
{
    public IReadOnlyCollection<IImageReader> Readers { get; } = new List<IImageReader>();

    public IReadOnlyCollection<IImageWriter> Writers { get; } = new List<IImageWriter>();

    public void UpdatePlugins(string folderName)
    {
        throw new NotImplementedException();
    }

    public IImageReader? GetReaderForType(string type)
    {
        throw new NotImplementedException();
    }

    public IImageWriter? GetWriterForType(string type)
    {
        throw new NotImplementedException();
    }
}
