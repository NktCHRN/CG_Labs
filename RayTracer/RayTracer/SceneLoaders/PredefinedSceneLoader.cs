using Common;
using RayTracer.Abstractions;
using RayTracer.LightSources;
using RayTracer.SceneObjects;

namespace RayTracer.SceneLoaders;
public sealed class PredefinedSceneLoader : ISceneLoader
{
    private const string _predefinedInputFolder = "PredifinedInput";

    private readonly Dictionary<string, Scene> _predefinedScenes = new();

    public PredefinedSceneLoader() 
    {
        _predefinedScenes["big_gradient_sphere"] = new Scene(
            new Camera(new Vector3F(0, 0, -75F), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(0), 30),
            new List<ISceneObject> { new Sphere(new Vector3F(-3, 2, 0), Vector3F.Zero, 5) },
            new List<ILightSource> { 
                new PointLightSource(new Vector3F(-10, -10, 0), 1, Color.Yellow), 
                new DirectionalLightSource(new Vector3F(-1, -1, 0.5F), 1, Color.Blue)});

        _predefinedScenes["girl_with_ball"] = new Scene(
            new Camera(new Vector3F(0, 0, -75F), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(0), 30),
            new List<ISceneObject> { 
                new Mesh(OBJHelper.Read(Path.Combine(_predefinedInputFolder, "guurl.obj"))),
                new Sphere(new Vector3F(-3, 0.75F, 0), Vector3F.Zero, 0.5F) },
            new List<ILightSource> {
                new AmbientLightSource(0.6F, Color.Magenta),
                new PointLightSource(new Vector3F(-1, 0, 0), 1, Color.Cyan),
                new DirectionalLightSource(new Vector3F(-1, -1, 0.5F), 1, Color.Blue)
            });

        _predefinedScenes["cube_and_spheres"] = new Scene(
            new Camera(new Vector3F(0, 0, -75F), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(0), 30),
            new List<ISceneObject> {
                        new Mesh(OBJHelper.Read(Path.Combine(_predefinedInputFolder, "cube.obj"))),
                        new Sphere(new Vector3F(-3, -0.5F, 0), Vector3F.Zero, 0.5F),
                        new Sphere(new Vector3F(3, 0.3F, 0), Vector3F.Zero, 0.65F),
                        new Sphere(new Vector3F(4.5F, 1.25F, 0), Vector3F.Zero, 0.85F),},
            new List<ILightSource> {
                        new AmbientLightSource(0.6F, Color.Yellow),
                        new PointLightSource(new Vector3F(-100, 0, 0), 1, Color.Green),
                        new DirectionalLightSource(new Vector3F(-1, 1, 0), 0.4F, Color.Red)});
    }

    public Scene Load(string name)
    {
        var hasScene = _predefinedScenes.TryGetValue(name, out Scene? scene);

        if (!hasScene)
        {
            throw new KeyNotFoundException("Scene was not found");
        }

        return scene!;
    }
}
