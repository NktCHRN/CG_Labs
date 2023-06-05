using ConsoleApp.Abstractions;

namespace ConsoleApp.OutputPrinters;
public sealed class ConsoleOutputWriter : IOutputWriter
{
    public void Write(float[,] matrix)
    {
        for (var i = 0; i < matrix.GetLength(0); i++)
        {
            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                var character = matrix[i,j] switch
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
