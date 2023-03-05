namespace Lab1.RayTracer;
class Disk : ABaseSceneObject
{
    public Disk(Vector3f position, Vector3f rotation, float radius) : base(position, rotation) => this.radius = radius;
    public Disk(Vector3f position, Vector3f rotation) : this(position, rotation, 1) {}
    public Disk() : this(new Vector3f(0), new Vector3f(0)) {}
    
    public override bool IsIntersectedBy(in Ray ray) => false;
    public override void ObjectWasPlaced() {}

    private float radius;
}