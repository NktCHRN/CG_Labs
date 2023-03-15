using System.Text;

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

    public string Render(Camera camera, Vector3F? light = null)
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
                var nearestIntersectionVector = GetIntersectionVectorWithNearestObject(ray);

                var character = GetResultCharacter(nearestIntersectionVector, light);
                resultBuilder.Append(character);

                currentScreenPosition += stepRight;
            }
            currentScreenPosition = rowStart + stepDown;
            resultBuilder.AppendLine();
        }

        return resultBuilder.ToString();
    }

    internal Vector3F? GetIntersectionVectorWithNearestObject(Ray ray)
    {
        var (hitDistance, nearestIntersectionVector) = (float.PositiveInfinity, null as Vector3F?);

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
                nearestIntersectionVector = intersectionVector;
            }
        }

        return nearestIntersectionVector;
    }

    private static char GetResultCharacter(Vector3F? intersectionVector, Vector3F? light)
    {
        if (intersectionVector is null)
        {
            return ' ';
        }

        if (light is null)
        {
            return '#';
        }

        var intersectionNormal = intersectionVector.Value.Normalized;
        var lightNormal = light.Value.Normalized;
        var lightCoefficient = lightNormal.DotProduct(intersectionNormal);

        return lightCoefficient switch
        {
            < 0 => ' ',
            < 0.2F => '.',
            < 0.5F => '*',
            < 0.8F => 'O',
            _ => '#'
        };
    }

    public void AddObject(ISceneObject obj)
    {
        _sceneObjects.Add(obj);
    }
}
