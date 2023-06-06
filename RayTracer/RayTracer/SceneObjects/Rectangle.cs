using RayTracer.Abstractions;
using RayTracer.Utility;

namespace RayTracer.SceneObjects;
public class Rectangle : BaseSceneObject, ISceneObject
{
    private readonly Vector3F _size;
    public Vector3F Up => Direction;
    public Vector3F Right { get; private set; }

    public Rectangle(Vector3F position, Vector3F up, Vector3F right, Vector3F rotation, Vector3F size)
    {
        _size = size;
        Direction = up;
        Right = right;

        Position = position;
        Rotation = rotation;
    }
    public Rectangle(Vector3F position, Vector3F up, Vector3F right, Vector3F rotation) : this(position, up, right, rotation, Vector3F.One) {}
    public Rectangle() : this(new Vector3F(0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(0)) {}

    public override Vector3F Rotation
    {
        get => _rotation;
        set
        {
            _rotation = value;

            Direction = Direction.RotatedBy(_rotation);
            Right = Right.RotatedBy(_rotation);
        }
    }

    public Intersection? GetIntersection(in Ray ray)
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
            return new Intersection(contact, this);

        return null;
    }

    public Vector3F GetNormalAt(Vector3F point)
    {
        return Up.CrossProduct(Right).Normalized;
    }
}
