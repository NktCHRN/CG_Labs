namespace Lab1.RayTracer.SceneObjects;

public class Sphere : BaseSceneObject
{
    public float Radius { get; }

    public Sphere(Vector3F position, Vector3F rotation, float radius) : base(position, rotation) => Radius = radius;
    public Sphere(Vector3F position, Vector3F rotation) : this(position, rotation, 1) { }
    public Sphere() : this(new Vector3F(0), new Vector3F(0)) { }

    public override Vector3F? GetIntersection(in Ray ray)
    {
        var origin = ray.StartPoint;
        var center = Position;
        var k = origin - center;

        var directionSquared = ray.Direction.DotProduct(ray.Direction);
        var radiusSquared = Radius * Radius;
        var kSquared = k.DotProduct(k);

        var a = directionSquared;
        var b = 2 * ray.Direction.DotProduct(k);
        var c = kSquared - radiusSquared;

        var discriminantSquared = b * b - 4 * a * c;

        if (discriminantSquared < 0)
            return null;

        var x1 = (-b + MathF.Sqrt(discriminantSquared)) / (2 * a);
        var x2 = (-b - MathF.Sqrt(discriminantSquared)) / (2 * a);

        var t = (MathF.Abs(x1) > MathF.Abs(x2)) ? x2 : x1;

        return ray.StartPoint + (ray.Direction * t);
    }
}
