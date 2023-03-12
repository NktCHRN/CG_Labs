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
    private readonly float _resolutionScale = 1;

    private Camera? _camera;
    private DirectionLight? _directionLight;
    private readonly IList<ISceneObject> _sceneObjects;

    public Scene(int width, int height, float scale = 1)
    {
        _width = width;
        _height = height;
        _resolutionScale = scale;
        _sceneObjects = new List<ISceneObject>();
    }

    public void TestRender()
    {
        var camera = new Camera(new Vector3F(0, 0, -25), new Vector3F(0));
        var light = new DirectionLight(new Vector3F(0, 5, -5), new Vector3F(0));
        
        _camera = camera;
        _directionLight = light;
        
        var plane = new Plane(new Vector3F(0), new Vector3F(0, 1, 0), new Vector3F(5, 5, 0));
        AddObject(plane);
        //Disk disk = new Disk(new Vector3f(0), new Vector3f(0, 0, 0), 10);
        //this.AddObject(disk);
        
        Render();
    }

    private void Render()
    {
        if (_camera is null)
            return;

        var scaledSize = _width * _height;
        var result = string.Empty;
        //    CGArray<Vector3f> result = new CGArray<Vector3f>(width * height);
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
                    var ray = new Ray(_camera.Position, new Vector3F(x / _resolutionScale, y / _resolutionScale, 0));
                    
                    if (_sceneObjects[i].IsIntersectedBy(ray))
                        result += " ";
                    else
                        result += "0";
                }
            }
            result += Environment.NewLine;
        }
        
        Console.WriteLine(result);
    }

    public void AddObject(ISceneObject obj)
    {
        _sceneObjects.Add(obj);
        obj.ObjectWasPlaced();
    }
}
