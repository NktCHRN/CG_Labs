using ImageConverter.Common;
using ImageConverter.Core.Abstractions;
using System.Reflection;

namespace ImageConverter.Core;
public sealed class PluginManager : IPluginManager
{
    private List<IImageReader> _readers = new();
    private List<IImageWriter> _writers = new();

    public IEnumerable<string> SupportedReaderExtensions => _readers.Select(r => r.FileExtension);
    public IEnumerable<string> SupportedWriterExtensions => _writers.Select(w => w.FileExtension);

    public string PluginsFolderName { get; set; } = "Plugins";


    public void UpdatePlugins()
    {
        _readers = new();
        _writers = new();

        var files = Directory.GetFiles(PluginsFolderName)
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

    public IImageReader? GetReaderForByteArray(byte[] byteArray)
        => _readers.FirstOrDefault(r => r.CanRead(byteArray));

    public IImageWriter? GetWriterForFileExtension(string type)
    {
        type = type.Trim().ToLower();
        return _writers.FirstOrDefault(w => w.FileExtension == type);
    }
}
