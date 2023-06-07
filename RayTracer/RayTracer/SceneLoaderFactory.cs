using RayTracer.Abstractions;
using RayTracer.SceneLoaders;

namespace RayTracer;
public sealed class SceneLoaderFactory : ISceneLoaderFactory
{
    public ISceneLoader CreateSceneLoader(string? sceneName, string? objFileName)
    {
        return (sceneName, objFileName) switch
        {
            (not null, not null) => throw new ArgumentException("Both scene name and source file name cannot be set at the same time"),
            (not null, null) => new PredefinedSceneLoader(),
            (null, not null) => new ObjFileSceneLoader(),
            _ => throw new ArgumentException("Both scene name and source file name cannot be null"),
        };
    }
}
