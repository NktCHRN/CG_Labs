using Common;
using RayTracer.Abstractions;
using RayTracer.Utility;
using System.Diagnostics;

namespace RayTracer;
public sealed class Renderer
{
    public Image Render(Scene scene, int width, int height)
    {
        var matrix = new float[height, width];

        var verticalScale = MathF.Tan(CGMath.DegToRad(camera.VerticalFieldOfView / 2));
        var heightToWidthCoefficient = width / (float)height;
        var planeHeight = 1 * verticalScale * 2;
        var planeWidth = planeHeight * heightToWidthCoefficient;

        scene.Camera.RightCorrection = heightToWidthCoefficient;

        var stepDown = planeHeight / height;
        var stepRight = planeWidth / width;

        var upperLeftPixelCoords = new Vector3F(-(planeWidth / 2) + stepRight / 2, planeHeight / 2 - stepDown / 2, -70);
        //var currentScreenPosition = camera.ScreenCenter + camera.Up * verticalScale - camera.Right;

        Stopwatch stopwatch = new();
        stopwatch.Start();
        Parallel.For(0, height, i =>
        {
            for (var j = 0; j < _width; j++)
            {
                var ray = new Ray(camera.Position, upperLeftPixelCoords + new Vector3F(stepRight * j, -stepDown * i, 0));
                var nearestIntersection = GetIntersectionWithClosestObject(ray);

                matrix[i, j] = GetLightCoefficient(nearestIntersection, light);
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
}
