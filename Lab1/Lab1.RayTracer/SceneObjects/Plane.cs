namespace Lab1.RayTracer;
public class Plane : BaseSceneObject
{
    private readonly Vector3F _size;

    public Plane(Vector3F position, Vector3F rotation, Vector3F size) : base(position, rotation) => _size = size;
    public Plane(Vector3F position, Vector3F rotation) : this(position, rotation, new Vector3F(0)) {}
    public Plane() : this(new Vector3F(0), new Vector3F(0)) {}
    
    public override bool IsIntersectedBy(in Ray ray)
    {
        Vector3F normal = Direction;

        float denom = normal.DotProduct(Position);

        float v = normal.DotProduct(ray.Direction);
        if (MathF.Abs(v) < 0.001)
            return false;

        float t = (denom - normal.DotProduct(ray.StartPoint)) / normal.DotProduct(ray.Direction);

        Vector3F newRay = ray.Direction * t;
        Vector3F contact = ray.StartPoint + newRay;

        Vector3F contactVec = contact - Position;

        return Math.Abs(contactVec.X) <= _size.X / 2 && Math.Abs(contactVec.Y) <= _size.Y / 2;
    }
    public override void ObjectWasPlaced() {}
}
