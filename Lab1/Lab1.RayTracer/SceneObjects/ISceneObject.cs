namespace Lab1.RayTracer;
public interface ISceneObject
{
    public void ObjectWasPlaced();
    public bool IsIntersectedBy(in Ray ray);
}