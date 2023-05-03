using RayTracer.Utility;

namespace RayTracer.SceneObjects;

public class Triangle : ISceneObject
{
    private Triangle(Vertex[] vertices) => _vertices = vertices;
    public Triangle(Vertex v1, Vertex v2, Vertex v3) : this(new Vertex[]{ v1, v2, v3 }) { }

    public Intersection? GetIntersection(in Ray ray)
    {
        //find vector for two edges sharing vertex1
        Vector3F edge1 = _vertices[1].Position - _vertices[0].Position;
        Vector3F edge2 = _vertices[2].Position - _vertices[0].Position;

        Vector3F IntersectionPoint;

        //begin calculating determinant - also used to calculate u-parameter
        Vector3F pvec = ray.Direction.CrossProduct(edge2);

        //if determinant is near zero, ray lies in plane of triangle 
        float det = edge1.DotProduct(pvec);

        if (det > -float.Epsilon && det < float.Epsilon)
        {
            return null;
        }

        float invDet = 1.0F / det;

        //calculate distance from vertex1 to ray origin 
        Vector3F tvec = ray.StartPoint - _vertices[0].Position;

        // calculate u-parameter and test bounds 
        float u = invDet * tvec.DotProduct(pvec);
        if (u < 0.0f || u > 1.0f)
        {
            return null;
        }

        //prepare to test v-parameter
        Vector3F qvec = tvec.CrossProduct(edge1);

        // calculate v-parameter and test bounds
        float v = invDet * ray.Direction.DotProduct(qvec);
        if (v < 0f || u + v > 1.0f)
        {
            return null;
        }

        // calculate t, ray intersects triangle 
        var distance = invDet * edge2.DotProduct(qvec);
        IntersectionPoint = ray.StartPoint + ray.Direction * distance;
        return new Intersection(IntersectionPoint, this);
    }

    public Vector3F GetNormalAt(Vector3F point)
    {
        var sideX = _vertices[0].Position - _vertices[1].Position;
        var sideY = _vertices[2].Position - _vertices[1].Position;
        return sideX.CrossProduct(sideY).Normalized;
    }

    private readonly Vertex[] _vertices = new Vertex[3];
}
