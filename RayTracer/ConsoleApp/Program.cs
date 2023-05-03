// See https://aka.ms/new-console-template for more information
using ConsoleApp.Abstractions;
using ConsoleApp.OutputPrinters;
using RayTracer;
using RayTracer.SceneObjects;

var scene = new Scene(60, 40);

//scene.AddObject(new Rectangle(new Vector3F(0, 0.22F, 6), Vector3F.Zero, new Vector3F(2, 2, 2)));
//scene.AddObject(new Sphere(new Vector3F(1.5F, 0, 5), new Vector3F(0, 0, 0), 1.5F));
//scene.AddObject(new Sphere(new Vector3F(0, 0, 10), new Vector3F(0, 0, 0), 1.5F));

//scene.AddObject(new Sphere(new Vector3F(0, -1, 0), new Vector3F(0, 0, 0), 3));
//scene.AddObject(new Disk(new Vector3F(2, 2F, 0), new Vector3F(90, 6, 0), 7));

var camera = new Camera(new Vector3F(0, 0, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(0), 30);
var light = new Vector3F(-1, 0, 0);

//Vector3F
/* intersects */
/*Vector3F rayOrigin = new Vector3F(0.5f, 0.5f, 1.0f);
Vector3F rayDir = new Vector3F(0.0f, 1.0f, 0.0f);
Vector3F v0 = new Vector3F(0f, 0.0f, 0.0f);
Vector3F v1 = new Vector3F(1.0f, 0.0f, 0.0f);
Vector3F v2 = new Vector3F(0.0f, 1.0f, 0.0f);
float distance = 6;*/

/* does not intersect */
Vector3F rayOrigin = new Vector3F(0.5f, 0.5f, 1.0f);
Vector3F rayDir = new Vector3F(0.0f, 0.0f, 1.0f);
Vector3F v0 = new Vector3F(0.0f, 0.0f, 0.0f);
Vector3F v1 = new Vector3F(1.0f, 0.0f, 0.0f);
Vector3F v2 = new Vector3F(0.0f, 1.0f, 0.0f);
float distance;

/*test*/

Console.WriteLine(scene.IsTriangleIntersected(rayOrigin, rayDir, v0, v1, v2, out distance)
    ? "intersection"
    : "not intersected");

var renderingResult = scene.Render(camera, light);
IOutputWriter consoleWriter = new ConsoleOutputWriter();
IOutputWriter imageWriter = new ImageOutputWriter();
consoleWriter.Write(renderingResult);
imageWriter.Write(renderingResult);
