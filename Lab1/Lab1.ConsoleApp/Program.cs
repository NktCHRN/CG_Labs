// See https://aka.ms/new-console-template for more information
using Lab1.RayTracer;
using Lab1.RayTracer.SceneObjects;

var scene = new Scene(60, 40);

var @object = new Sphere(new Vector3F(1.5F, 0, 0), new Vector3F(0, 0, 0), 3);
scene.AddObject(@object);

var camera = new Camera(new Vector3F(0, -3.5F, 0), new Vector3F(0, 1, 0), new Vector3F(0, 0, 3), new Vector3F(0), 90);
var light = new DirectionLight(new Vector3F(0, 5, -5), new Vector3F(0));

var renderingResult = scene.Render(camera, null);
Console.WriteLine(renderingResult);
