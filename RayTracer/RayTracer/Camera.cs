using RayTracer.Abstractions;

namespace RayTracer;
public class Camera : BaseSceneObject
{
    private float _verticalFieldOfView = 90;

    private Vector3F _baseUp;
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

            Direction = _baseDirection.RotatedBy(_rotation);
            Up = _baseUp.RotatedBy(_rotation);
        }
    }

    public Camera(Vector3F position, Vector3F direction, Vector3F up, Vector3F rotation, float verticalFieldOfView = 90)
    {
        if (up.DotProduct(direction) != 0)
            throw new ArgumentOutOfRangeException(nameof(up), "The angle between up and direction must be 90 degrees");

        Direction = direction;
        Up = up;

        _baseUp = up;
        _baseDirection = direction;

        Position = position;

        Rotation = rotation;

        VerticalFieldOfView = verticalFieldOfView;
    }
    public Camera() : base() { }
}
