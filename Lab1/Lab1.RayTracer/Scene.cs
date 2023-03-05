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
    public Scene(int width, int height, float scale = 1)
    {
        this.width = width;
        this.height = height;
        this.resolutionScale = scale;
        this.sceneObjects = new Lab1.RayTracer.Array<ISceneObject>(5);
    }

    public void TestRender()
    {
        Camera camera = new Camera(new Vector3f(0, 0, -25), new Vector3f(0));
        DirectionLight light = new DirectionLight(new Vector3f(0, 5, -5), new Vector3f(0));
        
        this.camera = camera;
        this.directionLight = light;
        
        Plane plane = new Plane(new Vector3f(0), new Vector3f(0, 1, 0), new Vector3f(5, 5, 0));
        this.AddObject(plane);
        //Disk disk = new Disk(new Vector3f(0), new Vector3f(0, 0, 0), 10);
        //this.AddObject(disk);
        
        Render();
    }

    private void Render()
    {
        if (camera is null)
            return;

        int scaledSize = this.width * this.height;
        string result = "";
    //    CGArray<Vector3f> result = new CGArray<Vector3f>(width * height);
        int startX = (int)MathF.Ceiling(-this.width / 2f);
        int startY = (int)MathF.Ceiling(-this.height / 2f);
        int endX = (int)MathF.Ceiling(this.width / 2f);
        int endY = (int)MathF.Ceiling(this.height / 2f);

        for (int y = startY; y < endY; y++)
        {
            for (int x = startX; x < endX; x++)
            {
                for (ulong i = 0; i < this.sceneObjects.size; i++)
                {
                    Ray ray = new Ray(this.camera.Position, new Vector3f(x / resolutionScale, y / resolutionScale, 0));
                    
                    if (this.sceneObjects[i].IsIntersectedBy(ray))
                        result += " ";
                    else
                        result += "0";
                }
            }
            result += "\n";
        }
        
        Console.WriteLine(result);
    }

    public void AddObject(ISceneObject obj)
    {
        this.sceneObjects.append(obj);
        obj.ObjectWasPlaced();
    }

    private int width;
    private int height;
    private float resolutionScale = 1;

    private Camera? camera;
    private DirectionLight? directionLight;
    private Lab1.RayTracer.Array<ISceneObject> sceneObjects;
}