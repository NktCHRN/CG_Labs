namespace Lab1.RayTracer;
public class Camera : BaseSceneObject
{
    public float VerticalFieldOfView { get; } = 45;

    public Camera(Vector3F position, Vector3F rotation, float verticalFieldOfView = 45) : base(position, rotation) 
    {
        if (VerticalFieldOfView <= 0 || VerticalFieldOfView >= 180)
            throw new ArgumentOutOfRangeException(nameof(verticalFieldOfView), "Vertical FOV must be in between 0 and 180 deg.");
        VerticalFieldOfView = verticalFieldOfView;
    }
    public Camera() : base() {}
    public override bool IsIntersectedBy(in Ray ray) => false;
    public override void ObjectWasPlaced() {}
}
