using Common;
using RayTracer.Abstractions;
using RayTracer.Utility;

namespace RayTracer.LightSources;
public sealed class DirectionalLightSource : ILightSource
{
    private readonly Vector3F _direction;

    private readonly float _intensity;

    private readonly Color _color;

    public DirectionalLightSource(Vector3F direction) : this(direction, 1, Color.White)
    {    }

    public DirectionalLightSource(Vector3F direction, float intensity, Color color)
    {
        _direction = direction;
        _color = color;
        _intensity = intensity;

        if (intensity is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(intensity), "Light intensity must be in range [0;1]");
        }
    }

    public Color GetLightCoefficient(Intersection intersection, IEnumerable<ISceneObject> sceneObjects)
    {
        var lightNormalized = _direction.Normalized;

        var intersectionPoint = intersection.Point;
        var reversedLightRay = new Ray(intersectionPoint, intersectionPoint - lightNormalized);
        if (HasIntersectionWithAnyObject(reversedLightRay, sceneObjects, intersection.Object))
        {
            return Color.Black;
        }

        var intersectionVectorNormalized = intersection.Object.GetNormalAt(intersectionPoint);
        var lightCoefficient = (-lightNormalized).DotProduct(intersectionVectorNormalized);
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
