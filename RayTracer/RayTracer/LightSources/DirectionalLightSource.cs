using Common;
using RayTracer.Abstractions;
using RayTracer.Utility;

namespace RayTracer.LightSources;
public sealed class DirectionalLightSource : BaseLightSource
{
    private readonly Vector3F _direction;

    public DirectionalLightSource(Vector3F direction) : this(direction, 1, Color.White)
    {    }

    public DirectionalLightSource(Vector3F direction, float intensity, Color color) : base(intensity, color)
    { 
        _direction = direction;

        if (intensity is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(intensity), "Light intensity must be in range [0;1]");
        }
    }

    public override Color GetLightCoefficient(Intersection intersection, IEnumerable<ISceneObject> sceneObjects)
    {

        var lightCoefficient = ProcessDirection(_direction, intersection, sceneObjects);
        return Color.FromShadowedColor(lightCoefficient, _color);

    }
}
