using Common;
using RayTracer.Abstractions;
using RayTracer.Utility;


namespace RayTracer.LightSources;

public sealed class PointLightSource : BaseLightSource
{
    private readonly Vector3F _position;


    public PointLightSource(Vector3F position) : this(position, 1, Color.White)
    { }

    public PointLightSource(Vector3F position, float intensity, Color color) : base(intensity, color)
    {
        _position = position;

        if (intensity is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(intensity), "Light intensity must be in range [0;1]");
        }
    }

    public override Color GetLightCoefficient(Intersection intersection, IEnumerable<ISceneObject> sceneObjects)
    {

        var intersectionPoint = intersection.Point;
        var direction = intersectionPoint - _position;

        var lightCoefficient = ProcessDirection(direction, intersection, sceneObjects);
        return Color.FromShadowedColor(lightCoefficient, _color);
    }
}