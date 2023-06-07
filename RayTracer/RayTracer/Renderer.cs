using Common;
using RayTracer.Abstractions;
using RayTracer.Utility;
using System.Diagnostics;

namespace RayTracer;
public sealed class Renderer : IRenderer
{
    public int Width { get; set; }
    public int Height { get; set; }

    public Renderer(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public Image Render(Scene scene)
    {
        var image = new Image(Width, Height);

        var verticalScale = MathF.Tan(CGMath.DegToRad(scene.Camera.VerticalFieldOfView / 2));
        var heightToWidthCoefficient = Width / (float)Height;
        var planeHeight = 1 * verticalScale * 2;
        var planeWidth = planeHeight * heightToWidthCoefficient;

        scene.Camera.RightCorrection = heightToWidthCoefficient;

        var stepDown = planeHeight / Height;
        var stepRight = planeWidth / Width;

        var upperLeftPixelCoords = new Vector3F(-(planeWidth / 2) + stepRight / 2, planeHeight / 2 - stepDown / 2, -70);
        //var currentScreenPosition = camera.ScreenCenter + camera.Up * verticalScale - camera.Right;

        Stopwatch stopwatch = new();
        stopwatch.Start();
        Parallel.For(0, Height, i =>
        {
            for (var j = 0; j < Width; j++)
            {
                var ray = new Ray(scene.Camera.Position, upperLeftPixelCoords + new Vector3F(stepRight * j, -stepDown * i, 0));
                var nearestIntersection = GetIntersectionWithClosestObject(ray, scene.Objects);

                image[i, j] = Color.Black;

                if (nearestIntersection is null)
                {
                    continue;
                }

                foreach (var lightSource in scene.LightSources)
                {
                    image[i, j] += lightSource.GetLightCoefficient(nearestIntersection.Value, scene.Objects);
                }
            }
        });
        stopwatch.Stop();
        Console.WriteLine("Render Execution Time: " + stopwatch.Elapsed);

        return image;
    }

    internal static Intersection? GetIntersectionWithClosestObject(Ray ray, IEnumerable<ISceneObject> sceneObjects)
    {
        var (hitDistance, closestIntersection) = (float.PositiveInfinity, (Intersection?)null);

        foreach (var sceneObject in sceneObjects)
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
