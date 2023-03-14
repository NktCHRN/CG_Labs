namespace Lab1.RayTracer;
public class Camera : BaseSceneObject
{
    private float _verticalFieldOfView = 45;

    public Vector3F Up { get; private set; }

    public float VerticalFieldOfView
    {
        get
        {
            return _verticalFieldOfView;
        }
        set
        {
            if (value <= 0 || value >= 180)
                throw new ArgumentOutOfRangeException(nameof(VerticalFieldOfView), "Vertical FOV must be in between 0 and 180 deg.");
            _verticalFieldOfView = value;
        }
    }

    public Vector3F Right => Up.CrossProduct(Direction) * RightCorrection;

    private float _rightCorrection;
    public float RightCorrection
    {
        get
        {
            return _rightCorrection;
        }
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(RightCorrection), "Right correction must be bigger than 0.");

            _rightCorrection = value;
        }
    }

    public Vector3F ScreenCenter => Position + Direction;

    public override Vector3F Rotation
    {
        get => _rotation;
        set
        {
            _rotation = value;

            Direction = Direction.RotatedBy(_rotation);
            Up = Up.RotatedBy(_rotation);
        }
    }

    public Camera(Vector3F position, Vector3F direction, Vector3F up, Vector3F rotation, float verticalFieldOfView = 45) 
    {
        Direction = direction;
        Up = up;
        Position = position;

        if (Up.DotProduct(Direction) != 0)
            throw new ArgumentOutOfRangeException(nameof(up), "The angle between up and direction must be 90 degrees");

        Rotation = rotation;

        VerticalFieldOfView = verticalFieldOfView;
    }
    public Camera() : base() {}
    public override bool IsIntersectedBy(in Ray ray) => false;
    public override void ObjectWasPlaced() {}
}
