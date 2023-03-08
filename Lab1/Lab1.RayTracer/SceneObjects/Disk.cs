namespace Lab1.RayTracer;
class Disk : BaseSceneObject
{
    private readonly float _radius;

    public Disk(Vector3f position, Vector3f rotation, float radius) : base(position, rotation) => _radius = radius;
    public Disk(Vector3f position, Vector3f rotation) : this(position, rotation, 1) {}
    public Disk() : this(new Vector3f(0), new Vector3f(0)) {}
    
    public override bool IsIntersectedBy(in Ray ray)
    {
        Vector3f normal = Direction;

        float denom = normal.DotProduct(Position);

        float v = normal.DotProduct(ray.Direction);
        if (MathF.Abs(v) < 0.001)
            return false;

        float t = (denom - normal.DotProduct(ray.StartPoint)) / normal.DotProduct(ray.Direction);

        Vector3f newRay = ray.Direction * t;
        Vector3f contact = ray.StartPoint + newRay;

        return (contact - Position).Length() < _radius;
    }
    public override void ObjectWasPlaced() {}
}
