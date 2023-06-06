using Common;
using RayTracer.Utility;
using System.Diagnostics;

namespace RayTracer;
public sealed class Renderer
{
    private Scene _scene;
    public Scene Scene
    {
        get => _scene;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _scene = value;
        }
    }

    public Renderer(Scene scene)
    {
        _scene = scene;
    }

    public Image Render(int width, int height)
    {
        var image = new Image(width, height);

        var verticalScale = MathF.Tan(CGMath.DegToRad(_scene.Camera.VerticalFieldOfView / 2));
        var heightToWidthCoefficient = width / (float)height;
        var planeHeight = 1 * verticalScale * 2;
        var planeWidth = planeHeight * heightToWidthCoefficient;

        _scene.Camera.RightCorrection = heightToWidthCoefficient;

        var stepDown = planeHeight / height;
        var stepRight = planeWidth / width;

        var upperLeftPixelCoords = new Vector3F(-(planeWidth / 2) + stepRight / 2, planeHeight / 2 - stepDown / 2, -70);
        //var currentScreenPosition = camera.ScreenCenter + camera.Up * verticalScale - camera.Right;

        Stopwatch stopwatch = new();
        stopwatch.Start();
        Parallel.For(0, height, i =>
        {
            for (var j = 0; j < width; j++)
            {
                var ray = new Ray(_scene.Camera.Position, upperLeftPixelCoords + new Vector3F(stepRight * j, -stepDown * i, 0));
                var nearestIntersection = GetIntersectionWithClosestObject(ray);

                image[i, j] = Color.Black;

                if (nearestIntersection is null)
                {
                    continue;
                }

                foreach (var lightSource in _scene.LightSources)
                {
                    image[i, j] += lightSource.GetLightCoefficient(nearestIntersection.Value, _scene.Objects);
                }
            }
        });
        stopwatch.Stop();
        Console.WriteLine("Render Execution Time: " + stopwatch.Elapsed);

        return image;
    }

    internal Intersection? GetIntersectionWithClosestObject(Ray ray)
    {
        var (hitDistance, closestIntersection) = (float.PositiveInfinity, (Intersection?)null);

        foreach (var sceneObject in _scene.Objects)
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
}
