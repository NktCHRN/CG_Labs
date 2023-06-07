using ConsoleApp.Abstractions;
using RayTracer;
using RayTracer.Abstractions;

namespace ConsoleApp;
public sealed class ProgramPrinter
{
    private readonly ISceneLoaderFactory _sceneLoaderFactory;
    private readonly IRenderer _renderer;
    private readonly IOutputWriterFactory _outputWriterFactory;

    public ProgramPrinter(ISceneLoaderFactory sceneLoaderFactory, IRenderer renderer, IOutputWriterFactory outputWriterFactory)
    {
        _sceneLoaderFactory = sceneLoaderFactory;
        _renderer = renderer;
        _outputWriterFactory = outputWriterFactory;
    }

    public void Print(string? sourceName, string? sceneName, string? outputName)
    {
        sourceName = sourceName?.Trim();
        sceneName = sceneName?.Trim();
        outputName = outputName?.Trim();

        Scene scene;
        try
        {
            if (string.IsNullOrWhiteSpace(outputName))
            {
                throw new ArgumentException("Output must be set");
            }

            var sceneLoader = _sceneLoaderFactory.CreateSceneLoader(sceneName, sourceName);
            scene = sceneLoader.Load(sceneName ?? sourceName!);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        Console.WriteLine("Started rendering...");
        var image = _renderer.Render(scene);
        Console.WriteLine("Rendering completed");

        var outputWriter = _outputWriterFactory.CreateOutputWriter(outputName);
        try
        {
            outputWriter.Write(image);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
