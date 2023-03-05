namespace Lab1.RayTracer;
class DirectionLight : ABaseSceneObject
{
    public DirectionLight(Vector3f position, Vector3f rotation) : base(position, rotation) {}
    public DirectionLight() : base() {}
    public override bool IsIntersectedBy(in Ray ray) => false;
    public override void ObjectWasPlaced() {}
}