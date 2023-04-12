// See https://aka.ms/new-console-template for more information
using Lab1.RayTracer;
using Lab1.RayTracer.SceneObjects;

var scene = new Scene(60, 40);

//scene.AddObject(new Rectangle(new Vector3F(0, 0.22F, 6), Vector3F.Zero, new Vector3F(2, 2, 2)));
scene.AddObject(new Sphere(new Vector3F(1.5F, 0, 5), new Vector3F(0, 0, 0), 1.5F));
scene.AddObject(new Sphere(new Vector3F(0, 0, 10), new Vector3F(0, 0, 0), 1.5F));

//scene.AddObject(new Sphere(new Vector3F(0, -1, 0), new Vector3F(0, 0, 0), 3));
//scene.AddObject(new Disk(new Vector3F(2, 2F, 0), new Vector3F(90, 6, 0), 7));

var camera = new Camera(new Vector3F(0, 0, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(0), 30);
var light = new Vector3F(-1, 0, 0);

var renderingResult = scene.Render(camera, light);
Console.WriteLine(renderingResult);
