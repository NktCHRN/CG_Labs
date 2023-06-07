namespace RayTracer.Abstractions;
public interface ISceneLoaderFactory
{
    ISceneLoader CreateSceneLoader(string? sceneName, string? objFileName);
}
