using RayTracer.Abstractions;
using RayTracer.Utility;

namespace RayTracer.SceneObjects;

public class Mesh : BaseSceneObject, ISceneObject
{
    public Mesh(Triangle[] triangles, Vector3F position, Vector3F rotation) : base(position, rotation)
    {
        _triangles = triangles;
    }
    public Mesh(Vertex[] vertices, Vector3F position, Vector3F rotation) : base(position, rotation)
    {
        int triangleCount = vertices.Length / 3;
        _triangles = new Triangle[triangleCount];
        for (int i = 0; i < triangleCount; i++)
        {
            Vertex[] arr = new Vertex[3];
            Array.Copy(vertices, i * 3, arr, 0, 3);
            _triangles[i] = new Triangle(arr[0], arr[1], arr[2]);
        }
    }
    public Mesh(Triangle[] triangles) : this(triangles, new Vector3F(0), new Vector3F(0)) {}
    public Mesh(Vertex[] vertices) : this(vertices, new Vector3F(0), new Vector3F(0)) {}
    public Intersection? GetIntersection(in Ray ray)
    {
        Intersection? intersection = null;

        foreach(Triangle triangle in _triangles)
        {
            intersection = triangle.GetIntersection(ray);
            if (intersection != null)
                break;
        }

        if (intersection is not null)
        {
            return new Intersection(
                Transformation.ApplyTo(intersection.Value.Point), 
                intersection.Value.Object);
        }

        return intersection;
    }

    public Vector3F GetNormalAt(Vector3F point)
    {
        throw new NotSupportedException();
    }

    public TransformationMatrix3D Transformation { get => new TransformationMatrix3D().TranslatedBy(Position).RotatedBy(Rotation); }

    private readonly Triangle[] _triangles;
}
