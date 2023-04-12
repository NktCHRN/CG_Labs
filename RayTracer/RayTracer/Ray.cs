namespace RayTracer;
public struct Ray {
    public Vector3F StartPoint { get; set; }
    public Vector3F EndPoint { get; set; }
    public Vector3F Direction { get => EndPoint - StartPoint; }
    public Ray()
    {
        StartPoint = Vector3F.Zero;
        EndPoint = Vector3F.One;
    }
    public Ray(Vector3F startPoint, Vector3F endPoint)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
    }
};
