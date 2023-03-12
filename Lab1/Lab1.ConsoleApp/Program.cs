// See https://aka.ms/new-console-template for more information
using Lab1.RayTracer;

var scene = new Scene(60, 40);

var plane = new Plane(new Vector3F(10, 10, 0), new Vector3F(0, 1, 0), new Vector3F(5, 5, 0));
scene.AddObject(plane);

var camera = new Camera(new Vector3F(0, 0, -25), new Vector3F(0));
var light = new DirectionLight(new Vector3F(0, 5, -5), new Vector3F(0));

var renderingResult = scene.Render(camera, light);
Console.WriteLine(renderingResult);
