using Common;
using Microsoft.VisualBasic;
using RayTracer.Utility;
namespace RayTracer.Abstractions;

public abstract class BaseLightSource : ILightSource 
{
    protected readonly float _intensity;

    protected readonly Color _color;

    public abstract Color GetLightCoefficient(Intersection intersection, IEnumerable<ISceneObject> sceneObjects);

    protected BaseLightSource(float intensity, Color color)
    {
        _intensity = intensity;
        _color = color; 
    }

    protected float ProcessDirection(Vector3F direction, Intersection intersection, IEnumerable<ISceneObject> sceneObjects)
    {
        var intersectionPoint = intersection.Point;
        var lightNormalized = direction.Normalized;

        var reversedLightRay = new Ray(intersectionPoint, intersectionPoint - lightNormalized);
        
        if (HasIntersectionWithAnyObject(reversedLightRay, sceneObjects, intersection.Object))
        {
            return 0;
        }

        var intersectionVectorNormalized = intersection.Object.GetNormalAt(intersectionPoint);
        var lightCoefficient = (-lightNormalized).DotProduct(intersectionVectorNormalized);
        lightCoefficient = Math.Max(lightCoefficient, 0);
        lightCoefficient *= _intensity;

        return lightCoefficient; 
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




