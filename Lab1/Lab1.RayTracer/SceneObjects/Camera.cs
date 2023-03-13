namespace Lab1.RayTracer;
public class Camera : BaseSceneObject
{
    private float _verticalFieldOfView = 45;

    public float VerticalFieldOfView
    {
        get
        {
            return _verticalFieldOfView;
        }
        set
        {
            if (value <= 0 || value >= 180)
                throw new ArgumentOutOfRangeException(nameof(value), "Vertical FOV must be in between 0 and 180 deg.");
            _verticalFieldOfView = value;
        }
    }

    public Camera(Vector3F position, Vector3F rotation, float verticalFieldOfView = 45) : base(position, rotation) 
    {
        VerticalFieldOfView = verticalFieldOfView;
    }
    public Camera() : base() {}
    public override bool IsIntersectedBy(in Ray ray) => false;
    public override void ObjectWasPlaced() {}
}
