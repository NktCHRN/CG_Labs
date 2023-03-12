namespace Lab1.RayTracer;
public abstract class BaseSceneObject : ISceneObject
{
    private readonly Vector3F _baseDirection = new (0, 0, 1);

    public Vector3F Position { get; set; }


    private Vector3F _rotation;
    public Vector3F Rotation
    {
        get => _rotation;
        set
        {
            _rotation = value;

            Direction = _baseDirection.RotatedBy(_rotation);
        }
    }

    public Vector3F Direction { get; private set; }

    public BaseSceneObject()
    {
        Position = new Vector3F(0, 0, 0);
        Rotation = new Vector3F(0, 0, 0);
    }
    public BaseSceneObject(Vector3F position, Vector3F rotation)
    {
        Position = position;
        Rotation = rotation;
    }

    public abstract bool IsIntersectedBy(in Ray ray);
    public abstract void ObjectWasPlaced();
}
