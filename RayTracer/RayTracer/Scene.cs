namespace RayTracer;

//On Scene coordinates are:
/*
y
^
|
|   z
|  /
| /
|/
0-------->x
*/
//Coordinates 0 0 0 is screen center

public class Scene
{
    private readonly int _width;
    private readonly int _height;

    private readonly IList<ISceneObject> _sceneObjects;

    public Scene(int width, int height)
    {
        _width = width;
        _height = height;
        _sceneObjects = new List<ISceneObject>();
    }

    public float[,] Render(Camera camera, Vector3F? light = null)
    {
        ArgumentNullException.ThrowIfNull(camera);

        var matrix = new float[_height, _width];

        var verticalScale = MathF.Tan(CGMath.DegToRad(camera.VerticalFieldOfView / 2));
        var heightToWidthCoefficient = _width / (float)_height;
        var planeHeight = 1 * verticalScale * 2;
        var planeWidth = planeHeight * heightToWidthCoefficient;

        camera.RightCorrection = heightToWidthCoefficient;

        var stepDown = planeHeight / _height;
        var stepRight = planeWidth / _width;

        var upperLeftPixelCoords = new Vector3F(-(planeWidth / 2) + stepRight / 2, planeHeight / 2 - stepDown / 2, 1);
        //var currentScreenPosition = camera.ScreenCenter + camera.Up * verticalScale - camera.Right;

        for (var i = 0; i < _height; i++)
        {
            for (var j = 0; j < _width; j++)
            {
                var ray = new Ray(camera.Position, upperLeftPixelCoords + new Vector3F(stepRight * j, -stepDown * i, 0));
                var nearestIntersectionNormal = GetIntersectionNormalWithNearestObject(ray);

                var character = GetCoefficient(nearestIntersectionNormal, light);
                matrix[i,j] = character;
            }
        }

        return matrix;
    }

    internal Vector3F? GetIntersectionNormalWithNearestObject(Ray ray)
    {
        var (hitDistance, nearestIntersectionNormal) = (float.PositiveInfinity, null as Vector3F?);

        foreach (var sceneObject in _sceneObjects)
        {
            var intersectionPoint = sceneObject.GetIntersection(ray);

            if (intersectionPoint is null)
            {
                continue;
            }

            Vector3F intersectionVector = intersectionPoint.Value - ray.StartPoint;
            var distance = intersectionVector.Length;

            if (distance < hitDistance)
            {
                hitDistance = distance;
                nearestIntersectionNormal = (intersectionPoint.Value - sceneObject.Position).Normalized;
            }
        }

        return nearestIntersectionNormal;
    }

    private static float GetCoefficient(Vector3F? intersectionNormal, Vector3F? light)
    {
        if (intersectionNormal is null)
        {
            return 0;
        }

        if (light is null)
        {
            return 1;
        }

        var lightNormalized = light.Value.Normalized;
        var lightCoefficient = lightNormalized.DotProduct(intersectionNormal.Value);
        return lightCoefficient;
    }

    public bool IsTriangleIntersected(Vector3F rayOrigin, Vector3F rayDirection, Vector3F vertex1, Vector3F vertex2, Vector3F vertex3, out float distance)
    {
        distance = float.MaxValue;
        //find vector for two edges sharing vertex1
        Vector3F edge1 = vertex2 - vertex1;
        Vector3F edge2 = vertex3 - vertex1;

        //begin calculating determinant - also used to calculate u-parameter
        Vector3F pvec = rayDirection.CrossProduct(edge2);

        //if determinant is near zero, ray lies in plane of triangle 
        float det = edge1.DotProduct(pvec);

        if (det > -float.Epsilon && det < float.Epsilon)
        {
            return false;
        }

        float inv_det = 1.0f / det;

        //calculate distance from vert1 to ray origin 
        Vector3F tvec = rayOrigin - vertex1;

        // calculate u-parameter and test bounds 
        float u = inv_det * tvec.DotProduct(pvec);
        if (u < 0.0f || u > 1.0f)
        {
            return false;
        }

        //prepare to test v-parameter
        Vector3F qvec = tvec.CrossProduct(edge1);

        // calculate v-parameter and test bounds
        float v = inv_det * rayDirection.DotProduct(qvec);
        if (v < 0f || u + v > 1.0f)
        {
            return false;
        }

        // calculate t, ray intersects triangle 
        distance = inv_det * edge2.DotProduct(qvec) * inv_det;
        return true;
    }

    public void AddObject(ISceneObject obj)
    {
        _sceneObjects.Add(obj);
    }
}
