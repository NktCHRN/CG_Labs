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

        var scale = MathF.Tan(CGMath.DegToRad(camera.VerticalFieldOfView / 2));

        var screenSize = _width * _height;
        var resultBuilder = new StringBuilder(screenSize);

        var startX = (int)MathF.Ceiling(-_width / 2F);
        var startY = (int)MathF.Ceiling(-_height / 2F);
        var endX = (int)MathF.Ceiling(_width / 2F);
        var endY = (int)MathF.Ceiling(_height / 2F);

        for (var y = startY; y < endY; y++)
        {
            for (var x = startX; x < endX; x++)
            {
                for (var i = 0; i < _sceneObjects.Count; i++)
                {
                    var ray = new Ray(camera.Position, camera.Direction + new Vector3F(x, y, 0));       // fix for another camera position
                    
                    if (_sceneObjects[i].IsIntersectedBy(ray))
                        resultBuilder.Append(' ');
                    else
                        resultBuilder.Append('0');
                }
            }
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
