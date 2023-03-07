namespace Lab1.RayTracer;
class Plane : BaseSceneObject
{
    private readonly Vector3f _size;

    public Plane(Vector3f position, Vector3f rotation, Vector3f size) : base(position, rotation) => _size = size;
    public Plane(Vector3f position, Vector3f rotation) : this(position, rotation, new Vector3f(0)) {}
    public Plane() : this(new Vector3f(0), new Vector3f(0)) {}
    
    public override bool IsIntersectedBy(in Ray ray) => false;
    public override void ObjectWasPlaced() {}
}
