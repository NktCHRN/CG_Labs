namespace RayTracer;
public class Rectangle : BaseSceneObject
{
    private readonly Vector3F _size;

    public Rectangle(Vector3F position, Vector3F rotation, Vector3F size) : base(position, rotation) => _size = size;
    public Rectangle(Vector3F position, Vector3F rotation) : this(position, rotation, new Vector3F(0)) {}
    public Rectangle() : this(new Vector3F(0), new Vector3F(0)) {}

    public override Vector3F? GetIntersection(in Ray ray)
    {
        Vector3F normal = Direction;

        float denom = normal.DotProduct(Position);

        float v = normal.DotProduct(ray.Direction);
        if (MathF.Abs(v) < 0.001)
            return null;

        float t = (denom - normal.DotProduct(ray.StartPoint)) / normal.DotProduct(ray.Direction);

        Vector3F newRay = ray.Direction * t;
        Vector3F contact = ray.StartPoint + newRay;

        Vector3F contactVec = contact - Position;

        if (Math.Abs(contactVec.X) <= _size.X / 2 && Math.Abs(contactVec.Y) <= _size.Y / 2)
            return contact;

        return null;
    }
}
