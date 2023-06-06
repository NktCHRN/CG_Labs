﻿// See https://aka.ms/new-console-template for more information
using Common;
using ConsoleApp.Abstractions;
using ConsoleApp.OutputPrinters;
using ImageConverter.Core;
using RayTracer;
using RayTracer.LightSources;
using RayTracer.SceneObjects;
using System.Globalization;

Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

//scene.AddObject(new Rectangle(new Vector3F(0, 0.22F, 6), Vector3F.Zero, new Vector3F(2, 2, 2)));

//scene.AddObject(new Sphere(new Vector3F(1.5F, 0, 5), new Vector3F(0, 0, 0), 1.5F));
//scene.AddObject(new Sphere(new Vector3F(-0.5F, 0, 5), new Vector3F(0, 0, 0), 0.5F));

var vertices = OBJHelper.Read("Input/guurl.obj");
var mesh = new Mesh(vertices);
//var triangle = new Triangle(new Vertex { Position = new Vector3F(1, -1, -1) }, new Vertex { Position = new Vector3F(1, -1, 1) }, new Vertex { Position = new Vector3F(-1, -1, 1) });

//scene.AddObject(new Triangle(new Vertex { Position = new Vector3F(1.5F, 0, 5) }, new Vertex { Position = new Vector3F(-0.5F, 0, 5) }, new Vertex { Position = new Vector3F(0.5F, 1F, 5) }));

//scene.AddObject(new Sphere(new Vector3F(0, -1, 0), new Vector3F(0, 0, 0), 3));
//scene.AddObject(new Disk(new Vector3F(2, 2F, 0), new Vector3F(90, 6, 0), 7));

var camera = new Camera(new Vector3F(0, 0, -75F), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(0), 30);

var scene = new Scene(camera);

scene.AddObject(mesh);

//var light = new DirectionalLightSource(new Vector3F(1, 0, 0), 1, Color.White);
var light = new AmbientLightSource(1, Color.White);
scene.AddLightSource(light);
var light2 = new DirectionalLightSource(new Vector3F(-1, 0, 0), 1, Color.White);
scene.AddLightSource(light2);
var light3 = new DirectionalLightSource(new Vector3F(0, 1, 0), 1, Color.White);
scene.AddLightSource(light3);
var light4 = new DirectionalLightSource(new Vector3F(0, -1, 0), 1, Color.White);
scene.AddLightSource(light4);

var light5 = new DirectionalLightSource(new Vector3F(1, 1, 0), 1, Color.White);
scene.AddLightSource(light5);
var light6 = new DirectionalLightSource(new Vector3F(-1, -1, 0), 1, Color.White);
scene.AddLightSource(light6);
var light7 = new DirectionalLightSource(new Vector3F(-1, 1, 0), 1, Color.White);
scene.AddLightSource(light7);
var light8 = new DirectionalLightSource(new Vector3F(1, -1, 0), 1, Color.White);
scene.AddLightSource(light8);

var light9 = new DirectionalLightSource(new Vector3F(0, 0, 1), 1, Color.White);
scene.AddLightSource(light9);

var light10 = new DirectionalLightSource(new Vector3F(0, -0.75F, 0.75F), 1, Color.White);
scene.AddLightSource(light10);


var renderingResult = new Renderer(scene).Render(600, 400);
IOutputWriter consoleWriter = new ConsoleOutputWriter();
IOutputWriter imageWriter = new ImageOutputWriter(new PluginManager());
consoleWriter.Write(renderingResult);
imageWriter.Write(renderingResult);
