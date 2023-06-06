using RayTracer.Abstractions;
using RayTracer.Utility;

namespace RayTracer.SceneObjects;
public class Disk : BaseSceneObject, ISceneObject
{
    public float Radius { get; }
    public Vector3F Up => Direction;
    public Vector3F Right { get; private set; }

    public Disk(Vector3F position, Vector3F up, Vector3F right, Vector3F rotation, float radius) 
    {
        Radius = radius;
        Direction = up;
        Right = right;

        Position = position;
        Rotation = rotation;
    }

    public Disk(Vector3F position, Vector3F up, Vector3F right, Vector3F rotation) : this(position, up, right, rotation, 1) { }

    public Disk() : this(Vector3F.Zero, new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Vector3F.Zero) {}

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

        if ((contact - Position).Length > Radius)
            return null;

        return new Intersection(contact, this);
    }

    public Vector3F GetNormalAt(Vector3F point)
    {
        return Up.CrossProduct(Right).Normalized;
    }
}
