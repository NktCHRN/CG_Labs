namespace Lab1.RayTracer;
public interface ISceneObject
{
    Vector3F Position { get; set; }
    public Vector3F? GetIntersection(in Ray ray);
}
