namespace Lab1.RayTracer;
public struct Ray {
    public Ray()
    {
        this.startPoint = Vector3f.Zero;
        this.endPoint = Vector3f.One;
    }
    public Ray(Vector3f startPoint, Vector3f endPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
    }
    
    public Vector3f startPoint;
    public Vector3f endPoint;
    public Vector3f direction { get => endPoint - startPoint; }
};