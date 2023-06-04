// See https://aka.ms/new-console-template for more information
using ImageConverter.Reader.PPM;
using ImageConverter.Writer.PPM;

Console.WriteLine("Hello, World!");
var reader = new PpmReader();
var ppm = reader.Read("Input/monument.ppm");
var writer = new PpmWriter();
writer.Write("Output/mn.ppm", ppm!.Value!);
