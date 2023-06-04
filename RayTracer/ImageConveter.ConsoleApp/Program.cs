// See https://aka.ms/new-console-template for more information
using ImageConverter.Reader.BMP;
using ImageConverter.Reader.PPM;
using ImageConverter.Writer.BMP;
using ImageConverter.Writer.PPM;

Console.WriteLine("Hello, World!");
var reader = new BmpReader();
var image = reader.Read("Input/greenland_grid_velo.bmp");
var writer = new BmpWriter();
writer.Write("Output/greenland_grid_velo3.bmp", image!.Value!);
