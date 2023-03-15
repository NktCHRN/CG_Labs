namespace Lab1.RayTracer.SceneObjects;

public class Sphere : BaseSceneObject
{
    private readonly float _radius;

    public Sphere(Vector3F position, Vector3F rotation, float radius) : base(position, rotation) => _radius = radius;
    public Sphere(Vector3F position, Vector3F rotation) : this(position, rotation, 1) { }
    public Sphere() : this(new Vector3F(0), new Vector3F(0)) { }

    public override Vector3F? GetIntersection(in Ray ray)
    {
        var origin = ray.StartPoint;
        var center = Position;
        var k = origin - center;

        var directionSquared = ray.Direction.DotProduct(ray.Direction);
        var radiusSquared = _radius * _radius;
        var kSquared = k.DotProduct(k);

        var a = directionSquared;
        var b = 2 * ray.Direction.DotProduct(k);
        var c = kSquared - radiusSquared;

        var discriminantSquared = b * b - 4 * a * c;

        float x1, x2 = 0;
        float t0 = 0;
        Vector3F? phit;

        if (discriminantSquared < 0)
            return null;

        x1 = (-b + MathF.Sqrt(discriminantSquared)) / (2 * a);
        x2 = (-b - MathF.Sqrt(discriminantSquared)) / (2 * a);
        if (MathF.Abs(x1) > MathF.Abs(x2))
        {
            t0 = x2;
        }
        else
        {
            t0 = x1;
        }

        phit = ray.StartPoint + ray.Direction * t0;

        return phit;
    }

    public override void ObjectWasPlaced() { }
}