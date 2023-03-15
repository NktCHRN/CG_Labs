using System;
using System.Text;
using Lab1.RayTracer.SceneObjects;

namespace Lab1.RayTracer;

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

    public string Render(Camera camera, DirectionLight? directionLight = null)
    {
        ArgumentNullException.ThrowIfNull(camera);

        var verticalScale = MathF.Tan(CGMath.DegToRad(camera.VerticalFieldOfView / 2));

        var heightToWidthCoefficient = _width / (float)_height;
        camera.RightCorrection = heightToWidthCoefficient;

        var stepDown = -(camera.Up * verticalScale / (_height / 2));
        var stepRight = camera.Right / (_width / 2);

        var currentScreenPosition = camera.ScreenCenter + camera.Up * verticalScale - camera.Right;

        var screenSize = _width * _height;
        var resultBuilder = new StringBuilder(screenSize + _height * Environment.NewLine.Length);

        for (var i = 0; i < _height; i++)
        {
            var rowStart = currentScreenPosition;
            for (var j = 0; j < _width; j++)
            {
                var ray = new Ray(camera.Position, currentScreenPosition);
                float? hitDistance = float.MaxValue;
                foreach (var sceneObject in _sceneObjects)
                {
                    
                        Vector3F? a1 = ((sceneObject.GetIntersection(ray)) - camera.Position);
                        Vector3F? a2 = a1;
                        a2 = a1 * a2;
                        if (a2 == null)
                        {
                            continue;
                        }
                        var distance = a2.Value.Length;
                    if (distance != null && distance < hitDistance)
                    {
                        hitDistance = distance;
                    }
                }

                if (hitDistance == float.MaxValue)
                {
                    resultBuilder.Append(' '); // Render the background
                }
                else
                {
                    resultBuilder.Append('#'); // Render the sphere
                }

                currentScreenPosition += stepRight;
            }
            currentScreenPosition = rowStart + stepDown;
            resultBuilder.AppendLine();
        }

        return resultBuilder.ToString();
    }

    public void AddObject(ISceneObject obj)
    {
        _sceneObjects.Add(obj);
        obj.ObjectWasPlaced();
    }
}
