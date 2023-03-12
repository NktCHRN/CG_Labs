namespace Lab1.RayTracer;
class Camera : BaseSceneObject
{
    public Camera(Vector3F position, Vector3F rotation) : base(position, rotation) {}
    public Camera() : base() {}
    public override bool IsIntersectedBy(in Ray ray) => false;
    public override void ObjectWasPlaced() {}
}