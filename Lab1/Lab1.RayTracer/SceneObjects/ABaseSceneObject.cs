namespace Lab1.RayTracer;
public abstract class ABaseSceneObject : ISceneObject
{
    public ABaseSceneObject()
    {
        this.Position = new Vector3f(0, 0, 0);
        this.Rotation = new Vector3f(0, 0, 0);
    }
    public ABaseSceneObject(Vector3f position, Vector3f rotation)
    {
        this.Position = position;
        this.Rotation = rotation;
    }

    public abstract bool IsIntersectedBy(in Ray ray);
    public abstract void ObjectWasPlaced();

    private readonly Vector3f baseDirection = new Vector3f(0, 0, 1);

    private Vector3f position;
    public Vector3f Position
    {
        get => position;
        set => this.position = value;
    }
    
    private Vector3f rotation;
    public Vector3f Rotation
    {
        get => rotation;
        set
        {
            Vector3f oldValue = this.rotation;
            this.rotation = value;

            this.direction = this.baseDirection.RotatedBy(rotation);
        }
    }

    public Vector3f direction{ get; private set; }
}