namespace Lab1.RayTracer;
public interface ISceneObject
{
    public Vector3F? GetIntersection(in Ray ray);
}
