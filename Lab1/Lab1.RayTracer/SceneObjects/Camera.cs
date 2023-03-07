namespace Lab1.RayTracer;
class Camera : BaseSceneObject
{
    public Camera(Vector3f position, Vector3f rotation) : base(position, rotation) {}
    public Camera() : base() {}
    public override bool IsIntersectedBy(in Ray ray) => false;
    public override void ObjectWasPlaced() {}
}