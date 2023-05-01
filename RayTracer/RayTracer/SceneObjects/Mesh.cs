namespace RayTracer.SceneObjects;

public class Mesh : BaseSceneObject
{
    public Mesh(Triangle[] triangles, Vector3F position, Vector3F rotation) : base(position, rotation)
    {
        this._triangles = triangles;
    }
    public Mesh(Vertex[] vertices, Vector3F position, Vector3F rotation) : base(position, rotation)
    {
        int triangleCount = vertices.Length / 3;
        this._triangles = new Triangle[triangleCount];
        for (int i = 0; i < triangleCount; i++)
        {
            Vertex[] arr = new Vertex[3];
            Array.Copy(vertices, i * 3, arr, 0, 3);
            this._triangles[i] = new Triangle(arr);
        }
    }
    public Mesh(Triangle[] triangles) : this(triangles, new Vector3F(0), new Vector3F(0)) {}
    public Mesh(Vertex[] vertices) : this(vertices, new Vector3F(0), new Vector3F(0)) {}
    public override Vector3F? GetIntersection(in Ray ray)
    {
        Vector3F? intersection = null;

        foreach(Triangle triangle in _triangles)
        {
            intersection = triangle.GetIntersection(ray);
            if (intersection != null)
                break;
        }

        return intersection;
    }

    private readonly Triangle[] _triangles;
}
