using ImageConverter.Common;
using ImageConverter.Core.Abstractions;
using System.Reflection;

namespace ImageConverter.Core;
public sealed class PluginManager : IPluginManager
{
    private List<IImageReader> _readers = new();

    private List<IImageWriter> _writers = new();

    public void UpdatePlugins(string folderName = "Plugins")
    {
        _readers = new();
        _writers = new();

        var files = Directory.GetFiles(folderName)
            .Where(f => f.EndsWith(".dll"));
        foreach (var file in files)
        {
            var assembly = Assembly.LoadFile(Path.GetFullPath(file));
            foreach (var type in assembly.GetExportedTypes())
            {
                if (type.GetInterfaces().Any(i => i == typeof(IImageReader)))
                {
                    var reader = (IImageReader)Activator.CreateInstance(type)!;
                    _readers.Add(reader);
                }
                else if (type.GetInterfaces().Any(i => i == typeof(IImageWriter)))
                {
                    var writer = (IImageWriter)Activator.CreateInstance(type)!;
                    _writers.Add(writer);
                }
            }
        }
    }

    public IImageReader? GetReaderForFile(string fileName)
        => _readers.FirstOrDefault(r => r.CanRead(fileName));

    public IImageWriter? GetWriterForFileExtension(string type)
    {
        type = type.Trim().ToLower();
        return _writers.FirstOrDefault(w => w.FileExtension == type);
    }
}
