namespace Lab1.RayTracer;
public struct Ray {
    public Vector3f StartPoint { get; set; }
    public Vector3f EndPoint { get; set; }
    public Vector3f Direction { get => EndPoint - StartPoint; }
    public Ray()
    {
        StartPoint = Vector3f.Zero;
        EndPoint = Vector3f.One;
    }
    public Ray(Vector3f startPoint, Vector3f endPoint)
    {
        StartPoint = startPoint;
        EndPoint = endPoint;
    }
};
