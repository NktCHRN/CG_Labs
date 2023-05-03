using RayTracer.Utility;

namespace RayTracer.SceneObjects;

public class Triangle : ISceneObject
{
    private Triangle(Vertex[] vertices) => _vertices = vertices;
    public Triangle(Vertex v1, Vertex v2, Vertex v3) : this(new Vertex[]{ v1, v2, v3 }) {}

    public Intersection? GetIntersection(in Ray ray)
    {
        throw new NotImplementedException();
    }

    public Vector3F GetNormalAt(Vector3F point)
    {
        var sideX = _vertices[0].Position - _vertices[1].Position;
        var sideY = _vertices[2].Position - _vertices[1].Position;
        return sideX.CrossProduct(sideY).Normalized;
    }

    private readonly Vertex[] _vertices = new Vertex[3];
}
