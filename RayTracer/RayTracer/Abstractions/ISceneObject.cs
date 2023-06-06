using RayTracer.Utility;

namespace RayTracer.Abstractions;
public interface ISceneObject
{
    public Intersection? GetIntersection(in Ray ray);
    public Vector3F GetNormalAt(Vector3F point);
}
