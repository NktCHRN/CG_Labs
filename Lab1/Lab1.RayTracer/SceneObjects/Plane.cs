namespace Lab1.RayTracer;
class Plane : BaseSceneObject
{
    private readonly Vector3f _size;

    public Plane(Vector3f position, Vector3f rotation, Vector3f size) : base(position, rotation) => _size = size;
    public Plane(Vector3f position, Vector3f rotation) : this(position, rotation, new Vector3f(0)) {}
    public Plane() : this(new Vector3f(0), new Vector3f(0)) {}
    
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

        Vector3f contactVec = contact - Position;

        return Math.Abs(contactVec.X) <= _size.X / 2 && Math.Abs(contactVec.Y) <= _size.Y / 2;
    }
    public override void ObjectWasPlaced() {}
}
