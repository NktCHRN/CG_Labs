namespace RayTracer.SceneObjects;
public class Disk : BaseSceneObject
{
    public float Radius { get; }

    public Disk(Vector3F position, Vector3F rotation, float radius) : base(position, rotation) => Radius = radius;
    public Disk(Vector3F position, Vector3F rotation) : this(position, rotation, 1) {}
    public Disk() : this(new Vector3F(0), new Vector3F(0)) {}

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

        if ((contact - Position).Length > Radius)
            return null;

        return contact;
    }
}
