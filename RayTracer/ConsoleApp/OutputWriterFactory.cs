using ConsoleApp.Abstractions;
using ConsoleApp.OutputPrinters;
using ImageConverter.Core;
using ImageConverter.Core.Abstractions;

namespace ConsoleApp;
public sealed class OutputWriterFactory : IOutputWriterFactory
{
    private readonly IPluginManager _pluginManager;

    public OutputWriterFactory(IPluginManager pluginManager)
    {
        _pluginManager = pluginManager;
    }

    public IOutputWriter CreateOutputWriter(string outputName)
    {
        if (string.Equals(outputName, "console", StringComparison.OrdinalIgnoreCase) )
        {
            return new ConsoleOutputWriter();
        }

        var format = Path.GetExtension(outputName);
        ValidationMethods.ValidateFileExtension(format);
        format = HelperMethods.FormatFileExtension(format);

        _pluginManager.UpdatePlugins();
        var internalImageWriter = _pluginManager.GetWriterForFileExtension(format) ??
            throw new NotSupportedException($"Your output format {format} is currently not supported for writing. " +
                $"Supported formats: {string.Join(", ", _pluginManager.SupportedWriterExtensions)}");
        return new ImageOutputWriter(internalImageWriter, outputName);
    }
}
