using RayTracer.Utility;

namespace RayTracer.SceneObjects;
public interface ISceneObject
{
    public Intersection? GetIntersection(in Ray ray);
    public Vector3F GetNormalAt(Vector3F point);
}
