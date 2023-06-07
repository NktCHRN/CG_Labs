using Common;
using RayTracer.Abstractions;
using RayTracer.LightSources;
using RayTracer.SceneObjects;

namespace RayTracer.SceneLoaders;
public sealed class ObjFileSceneLoader : ISceneLoader
{
    public Scene Load(string name)
    {
        var vertices = OBJHelper.Read(name);

        Console.WriteLine($"Nr of vertices: {vertices.Length}");
        Console.WriteLine("OBJ file is loaded!");

        var mesh = new Mesh(vertices);
        var predefinedLight = new ILightSource[]
        {
            new AmbientLightSource(0.25F, Color.White),
            new DirectionalLightSource(new Vector3F(1, -1, 0), 1, Color.White),
            new DirectionalLightSource(new Vector3F(-1, -1, 0), 1, Color.White)
        };
        var predefinedCamera = new Camera(new Vector3F(0, 0, -75F), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(0), 30);

        return new Scene(predefinedCamera, new ISceneObject[] { mesh }, predefinedLight);
    }
}
