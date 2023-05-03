namespace RayTracer.SceneObjects;

public class Triangle : BaseSceneObject
{
    public Triangle(Vector3F position, Vector3F rotation) : base(position, rotation) { }
    public Triangle(Vertex[] vertices) : this(new Vector3F(0), new Vector3F(0)) => _vertices = vertices;
    public Triangle(Vertex v1, Vertex v2, Vertex v3) : this(new Vertex[]{ v1, v2, v3 }) {}

    public override Vector3F? GetIntersection(in Ray ray)
    {
        float distance = float.MaxValue;
        //find vector for two edges sharing vertex1
        Vector3F edge1 = _vertices[1].Position - _vertices[0].Position;
        Vector3F edge2 = _vertices[2].Position - _vertices[0].Position;

        //Vector3F rayDirection = ray.Direction;
        //Vector3F rayOrigin = ray.StartPoint;
        Vector3F IntersectionPoint;

        //begin calculating determinant - also used to calculate u-parameter
        Vector3F pvec = ray.Direction.CrossProduct(edge2);

        //if determinant is near zero, ray lies in plane of triangle 
        float det = edge1.DotProduct(pvec);

        if (det > -float.Epsilon && det < float.Epsilon)
        {
            return null;
        }

        float inv_det = 1.0f / det;

        //calculate distance from vertex1 to ray origin 
        Vector3F tvec = ray.StartPoint - _vertices[0].Position;

        // calculate u-parameter and test bounds 
        float u = inv_det * tvec.DotProduct(pvec);
        if (u < 0.0f || u > 1.0f)
        {
            return null;
        }

        //prepare to test v-parameter
        Vector3F qvec = tvec.CrossProduct(edge1);

        // calculate v-parameter and test bounds
        float v = inv_det * ray.Direction.DotProduct(qvec);
        if (v < 0f || u + v > 1.0f)
        {
            return null;
        }

        // calculate t, ray intersects triangle 
        distance = inv_det * edge2.DotProduct(qvec);
        IntersectionPoint = ray.StartPoint + ray.Direction * distance;
        return IntersectionPoint;

    }
    private readonly Vertex[] _vertices = new Vertex[3];
}