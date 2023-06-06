using Common;
using ConsoleApp.Abstractions;

namespace ConsoleApp.OutputPrinters;
public sealed class ConsoleOutputWriter : IOutputWriter
{
    public void Write(Image image)
    {
        for (var i = 0; i < image.Width; i++)
        {
            for (var j = 0; j < image.Height; j++)
            {
                var character = image[j, i].LightCoefficient switch
                {
                    <= 0 => ' ',
                    <= 0.2F => '.',
                    <= 0.5F => '*',
                    <= 0.8F => 'O',
                    _ => '#'
                };
                Console.Write(character);
            }
            Console.WriteLine();
        }
    }
}
