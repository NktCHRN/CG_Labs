namespace Lab1.RayTracer;
public class DirectionLight : BaseSceneObject
{
    public DirectionLight(Vector3F position, Vector3F rotation) : base(position, rotation) {}
    public DirectionLight() : base() {}
    public override bool IsIntersectedBy(in Ray ray) => false;
    public override void ObjectWasPlaced() {}
}