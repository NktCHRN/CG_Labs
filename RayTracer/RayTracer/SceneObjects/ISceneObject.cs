namespace RayTracer.SceneObjects;
public interface ISceneObject
{
    Vector3F Position { get; set; }
    public Vector3F? GetIntersection(in Ray ray);
}
