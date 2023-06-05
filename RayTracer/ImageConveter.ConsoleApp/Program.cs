using ImageConverter.Core;
using Microsoft.Extensions.Configuration;
using static ImageConverter.Core.ValidationMethods;

IConfiguration configuration = new ConfigurationBuilder()
    .AddCommandLine(args)
    .Build();
var source = configuration["source"];
var goalFormat = configuration["goal-format"];

var pluginManager = new PluginManager();
var imageConverter = new ImageConverter.Core.ImageConverter(pluginManager);
try
{
    ValidateFileName(source);
    ValidateFileExtension(goalFormat);
    var fileInfo = imageConverter.Convert(source, goalFormat);
    Console.WriteLine($"Your file was converted successfully. Currently it is located in {Environment.NewLine}{fileInfo.FullName}");
}
catch (Exception exc)
{
    Console.WriteLine($"Error: {exc.Message}");
}
