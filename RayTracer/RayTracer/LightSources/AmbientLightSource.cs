using Common;
using RayTracer.Abstractions;
using RayTracer.Utility;

namespace RayTracer.LightSources;

public sealed class AmbientLightSource : BaseLightSource
{

    private readonly Random random = new Random();
    private const int _raysCount = 50; 

    public AmbientLightSource(float intensity) : this(intensity, Color.White)
    { }


    public AmbientLightSource(float intensity, Color color) : base(intensity, color)
    {

        if (intensity is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(intensity), "Light intensity must be in range [0;1]");
        }
    }

    public Vector3F SampleLight()
    {
        float u = (float)random.NextDouble();
        float v = (float)random.NextDouble();

        float theta = 2 * MathF.PI * u;
        float phi = MathF.Acos(2 * v - 1);

        float x = MathF.Cos(theta) * MathF.Sin(phi);
        float y = MathF.Sin(theta) * MathF.Sin(phi);
        float z = MathF.Cos(phi);

        return new Vector3F(x, y, z);
    }

    public override Color GetLightCoefficient(Intersection intersection, IEnumerable<ISceneObject> sceneObjects)
    {

        float lightCoefficient = 0;

        for (int i = 0; i < _raysCount; i++)
        {
            Vector3F randRay = SampleLight();
            lightCoefficient += ProcessDirection(randRay, intersection, sceneObjects);
        }

        lightCoefficient /= _raysCount;

        return Color.FromShadowedColor(lightCoefficient, _color);
    }

}