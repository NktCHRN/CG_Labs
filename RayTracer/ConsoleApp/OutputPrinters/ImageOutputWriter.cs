using ImageConverter;
using ConsoleApp.Abstractions;

namespace ConsoleApp.OutputPrinters;
public sealed class ImageOutputWriter : IOutputWriter
{
    public void Write(float[,] matrix)
    {
        int width = matrix.GetLength(1);
        int height = matrix.GetLength(0);

        Byte[] data = new Byte[width * height * 3];

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                float val = matrix[y, x] * 255;

                byte byteValue = 0;
                if (val > 0)
                    byteValue = Convert.ToByte(val);

                int index = (width * y + x) * 3;
                data[index    ] = byteValue;
                data[index + 1] = byteValue;
                data[index + 2] = byteValue;
            }
        }

        string containerPath = "../../../../ImageConverter/build/";
        var mat = new ImageReaderWrapper(containerPath).ReadData(data, width, height);
        new ImageWriterWrapper(containerPath).Write(mat, "../../../../test", ".bmp");
    }
}