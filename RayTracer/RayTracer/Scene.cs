using System.Diagnostics;
using RayTracer.SceneObjects;
using RayTracer.Utility;
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

        var upperLeftPixelCoords = new Vector3F(-(planeWidth / 2) + stepRight / 2, planeHeight / 2 - stepDown / 2, -70);
        //var currentScreenPosition = camera.ScreenCenter + camera.Up * verticalScale - camera.Right;
        
        Stopwatch stopwatch = new();
        stopwatch.Start();
        Parallel.For(0, _height, i =>
        {
            for (var j = 0; j < _width; j++)
            {
                var ray = new Ray(camera.Position, upperLeftPixelCoords + new Vector3F(stepRight * j, -stepDown * i, 0));
                var nearestIntersection = GetIntersectionWithClosestObject(ray);

                matrix[i,j] = GetLightCoefficient(nearestIntersection, light); 
            }
        });
        stopwatch.Stop();
        Console.WriteLine("Render Execution Time: " + stopwatch.Elapsed);

        return matrix;
    }

    internal Intersection? GetIntersectionWithClosestObject(Ray ray)
    {
        var (hitDistance, closestIntersection) = (float.PositiveInfinity, (Intersection?)null);

        foreach (var sceneObject in _sceneObjects)
        {
            var intersection = sceneObject.GetIntersection(ray);

            if (intersection is null)
            {
                continue;
            }

            Vector3F intersectionVector = intersection.Value.Point - ray.StartPoint;
            var distance = intersectionVector.Length;

            if (distance < hitDistance)
            {
                hitDistance = distance;
                closestIntersection = intersection;
            }
        }

        return closestIntersection;
    }

    private float GetLightCoefficient(Intersection? intersection, Vector3F? light)
    {
        if (intersection is null)
        {
            return 0;
        }

        if (light is null)
        {
            return 1;
        }

        var lightNormalized = light.Value.Normalized;

        var intersectionPoint = intersection.Value.Point;
        var reversedLightRay = new Ray(intersectionPoint, intersectionPoint - lightNormalized);
        if (HasIntersectionWithAnyObject(reversedLightRay))
        {
            return 0;
        }

        var intersectionVectorNormalized = intersection.Value.Object.GetNormalAt(intersectionPoint);
        var lightCoefficient = (-lightNormalized).DotProduct(intersectionVectorNormalized);
        return lightCoefficient;
    }

    internal bool HasIntersectionWithAnyObject(Ray ray)
    {
        foreach (var sceneObject in _sceneObjects)
        {
            var intersectionPoint = sceneObject.GetIntersection(ray);

            if (intersectionPoint is not null)
            {
                return true;
            }
        }

        return false;
    }

    public void AddObject(ISceneObject obj)
    {
        _sceneObjects.Add(obj);
    }
}
