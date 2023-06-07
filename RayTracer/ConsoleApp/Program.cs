// See https://aka.ms/new-console-template for more information
using ConsoleApp;
using ImageConverter.Core;
using Microsoft.Extensions.Configuration;
using RayTracer;
using System.Globalization;

Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");

IConfiguration configuration = new ConfigurationBuilder()
    .AddCommandLine(args)
    .Build();
var source = configuration["source"];
var scene = configuration["scene"];
var output = configuration["output"];

var (width, height) = (600, 400);

var printer = new ProgramPrinter(
    new SceneLoaderFactory(),
    new Renderer(width, height),
    new OutputWriterFactory(new PluginManager()));
printer.Print(source, scene, output);
