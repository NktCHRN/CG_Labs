using RayTracer.Abstractions;
namespace RayTracer;

public class Scene
{
    private readonly List<ISceneObject> _sceneObjects = new ();
    public IReadOnlyList<ISceneObject> Objects => _sceneObjects;

    private readonly List<ILightSource> _lightSources = new ();
    public IReadOnlyList<ILightSource> LightSources => _lightSources;

    public Camera Camera { get; }

    public Scene(Camera camera)
    {
        Camera = camera;
    }

    public Scene(Camera camera, IList<ISceneObject> sceneObjects, IList<ILightSource> lightSources)
    {
        Camera = camera;
        _sceneObjects.AddRange(sceneObjects);
        _lightSources.AddRange(lightSources);
    }

    public void AddObject(ISceneObject obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        _sceneObjects.Add(obj);
    }

    public void AddLightSource(ILightSource lightSource)
    {
        ArgumentNullException.ThrowIfNull(_lightSources);
        _lightSources.Add(lightSource);
    }
}
