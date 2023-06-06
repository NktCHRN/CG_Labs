using Common;
using RayTracer.Abstractions;
using RayTracer.Utility;


namespace RayTracer.LightSources;

public sealed class PointLightSource : ILightSource
{
    private readonly Vector3F _position;

    private readonly float _intensity;

    private readonly Color _color;

    public PointLightSource(Vector3F position) : this(position, 1, Color.White)
    { }

    public PointLightSource(Vector3F position, float intensity, Color color)
    {
        _position = position;
        _color = color;
        _intensity = intensity;

        if (intensity is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(intensity), "Light intensity must be in range [0;1]");
        }
    }

    public Color GetLightCoefficient(Intersection intersection, IEnumerable<ISceneObject> sceneObjects)
    {

        var intersectionPoint = intersection.Point;
        var lightDirection = (intersectionPoint - _position).Normalized;
        var lightRay = new Ray(intersectionPoint, _position);

        if (HasIntersectionWithAnyObject(lightRay, sceneObjects, intersection.Object))
        {
            return Color.Black;
        }

        var intersectionVectorNormalized = intersection.Object.GetNormalAt(intersectionPoint);
        var lightCoefficient = (-lightDirection).DotProduct(intersectionVectorNormalized);
        lightCoefficient = Math.Max(lightCoefficient, 0);
        lightCoefficient *= _intensity;

        return Color.FromShadowedColor(lightCoefficient, _color);
    }

    internal static bool HasIntersectionWithAnyObject(Ray ray, IEnumerable<ISceneObject> sceneObjects, ISceneObject bypassObject)
    {
        foreach (var sceneObject in sceneObjects)
        {
            var intersectionPoint = sceneObject.GetIntersection(ray);

            if (intersectionPoint is not null && intersectionPoint.Value.Object != bypassObject)
            {
                return true;
            }
        }

        return false;
    }
}