namespace Lab1.RayTracer;
public interface ISceneObject
{
    public void ObjectWasPlaced();
    public Vector3F? GetIntersection(in Ray ray);
}