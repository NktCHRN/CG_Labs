using System.Diagnostics;
using RayTracer.SceneObjects;
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
        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Parallel.For(0, _height, i =>
        {
            for (var j = 0; j < _width; j++)
            {
                var ray = new Ray(camera.Position, upperLeftPixelCoords + new Vector3F(stepRight * j, -stepDown * i, 0));
                var nearestIntersectionNormal = GetIntersectionNormalWithNearestObject(ray);

                var character = GetCoefficient(nearestIntersectionNormal, light);
                matrix[i, j] = character;
            }
        });
        stopwatch.Stop();
        Console.WriteLine("Render Execution Time: " + stopwatch.Elapsed);

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

    public void AddObject(ISceneObject obj)
    {
        _sceneObjects.Add(obj);
    }
}
