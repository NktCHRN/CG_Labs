namespace Lab1.RayTracer;
public class DirectionLight : BaseSceneObject
{
    public DirectionLight(Vector3F position, Vector3F rotation) : base(position, rotation) {}
    public DirectionLight() : base() {}
    public override Vector3F? GetIntersection(in Ray ray) { return null; }
    public override void ObjectWasPlaced() {}
}