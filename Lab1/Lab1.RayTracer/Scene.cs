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

                if (_sceneObjects[0].IsIntersectedBy(ray))      // hardcoded for now
                    resultBuilder.Append(' ');
                else
                    resultBuilder.Append('0');

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
