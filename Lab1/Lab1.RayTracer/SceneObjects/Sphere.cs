namespace Lab1.RayTracer.SceneObjects;

public class Sphere : BaseSceneObject
{
    private readonly float _radius;

    public Sphere(Vector3f position, Vector3f rotation, float radius) : base(position, rotation) => _radius = radius;
    public Sphere(Vector3f position, Vector3f rotation) : this(position, rotation, 1) { }
    public Sphere() : this(new Vector3f(0), new Vector3f(0)) { }

    public override bool IsIntersectedBy(in Ray ray)
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

        return discriminantSquared >= 0;
    }

    public override void ObjectWasPlaced() { }
}