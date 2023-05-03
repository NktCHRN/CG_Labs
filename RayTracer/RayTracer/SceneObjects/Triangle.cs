namespace RayTracer.SceneObjects;

public class Triangle : BaseSceneObject
{
    public Triangle(Vector3F position, Vector3F rotation) : base(position, rotation) { }
    public Triangle(Vertex[] vertices) : this(new Vector3F(0), new Vector3F(0)) => _vertices = vertices;
    public Triangle(Vertex v1, Vertex v2, Vertex v3) : this(new Vertex[]{ v1, v2, v3 }) {}

    public override Vector3F? GetIntersection(in Ray ray)
    {
        throw new NotImplementedException();
    }

    private readonly Vertex[] _vertices = new Vertex[3];
}