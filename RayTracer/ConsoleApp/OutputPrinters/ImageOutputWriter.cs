using Common;
using ConsoleApp.Abstractions;
using ImageConverter.Common;

namespace ConsoleApp.OutputPrinters;
public sealed class ImageOutputWriter : IOutputWriter
{
    private readonly IImageWriter _imageWriter;

    public string OutputFolder {get; set;} = "Output";

    public string FileName { get; set; }

    public ImageOutputWriter(IImageWriter imageWriter, string fileName)
    {
        _imageWriter = imageWriter;
        FileName = fileName;
    }

    public void Write(Image image)
    {
        _ = Directory.CreateDirectory(OutputFolder);

        var byteArray = _imageWriter.Write(image);

        var filePath = Path.GetFileName(FileName) == FileName ? Path.Combine(OutputFolder, FileName) : FileName;
        File.WriteAllBytes(filePath, byteArray);

        var fileInfo = new FileInfo(filePath);

        Console.WriteLine($"Your output file is located in {Environment.NewLine}{fileInfo.FullName}");
    }
}
