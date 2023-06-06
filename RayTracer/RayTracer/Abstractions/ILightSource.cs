using Common;
using RayTracer.Utility;

namespace RayTracer.Abstractions;
public interface ILightSource
{
    Color GetLightCoefficient(Intersection intersection, IEnumerable<ISceneObject> sceneObjects);
}
