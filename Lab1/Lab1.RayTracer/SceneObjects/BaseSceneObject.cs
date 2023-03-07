namespace Lab1.RayTracer;
public abstract class BaseSceneObject : ISceneObject
{
    private readonly Vector3f _baseDirection = new (0, 0, 1);

    public Vector3f Position { get; set; }


    private Vector3f _rotation;
    public Vector3f Rotation
    {
        get => _rotation;
        set
        {
            _rotation = value;

            Direction = _baseDirection.RotatedBy(_rotation);
        }
    }

    public Vector3f Direction { get; private set; }

    public BaseSceneObject()
    {
        Position = new Vector3f(0, 0, 0);
        Rotation = new Vector3f(0, 0, 0);
    }
    public BaseSceneObject(Vector3f position, Vector3f rotation)
    {
        Position = position;
        Rotation = rotation;
    }

    public abstract bool IsIntersectedBy(in Ray ray);
    public abstract void ObjectWasPlaced();
}
