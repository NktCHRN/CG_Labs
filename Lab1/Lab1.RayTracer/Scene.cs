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

        var resolutionScale = 1;        // for further refactoring

        var scaledSize = _width * _height;
        var resultBuilder = new StringBuilder(scaledSize);

        var startX = (int)MathF.Ceiling(-_width / 2f);
        var startY = (int)MathF.Ceiling(-_height / 2f);
        var endX = (int)MathF.Ceiling(_width / 2f);
        var endY = (int)MathF.Ceiling(_height / 2f);

        for (var y = startY; y < endY; y++)
        {
            for (var x = startX; x < endX; x++)
            {
                for (var i = 0; i < _sceneObjects.Count; i++)
                {
                    var ray = new Ray(camera.Position, new Vector3F(x / resolutionScale, y / resolutionScale, 0));
                    
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
