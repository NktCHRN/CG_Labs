using Common;
using RayTracer.Abstractions;
using RayTracer.Utility;


namespace RayTracer.LightSources;

public sealed class AmbientLightSource : ILightSource
{

    private readonly float _intensity;

    private readonly Color _color;

    public AmbientLightSource(float intensity) : this(intensity, Color.White)
    { }
    

    public AmbientLightSource(float intensity, Color color)
    {
        _color = color;
        _intensity = intensity;

        if (intensity is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(intensity), "Light intensity must be in range [0;1]");
        }
    }

    public Color GetLightCoefficient(Intersection intersection, IEnumerable<ISceneObject> sceneObjects)
    {

        var lightCoefficient = 1.0f;
        lightCoefficient *= _intensity;
        return Color.FromShadowedColor(lightCoefficient, _color);

    }

}