// See https://aka.ms/new-console-template for more information
using ImageConverter.Core;
using ImageConverter.Reader.BMP;
using ImageConverter.Reader.PPM;
using ImageConverter.Writer.BMP;
using ImageConverter.Writer.PPM;

Console.WriteLine("Hello, World!");
var pluginManager = new PluginManager();
pluginManager.UpdatePlugins();