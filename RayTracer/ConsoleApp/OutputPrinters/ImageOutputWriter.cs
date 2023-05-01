using ConsoleApp.Abstractions;
using System.Runtime.InteropServices;

namespace ConsoleApp.OutputPrinters;
public sealed class ImageOutputWriter : IOutputWriter
{
    private const string ContainerPath = "../../../../ImageConverter/build/Debug/";
    private const string ImageLibPath = ContainerPath + "ImageConverter_LIB.dll";

    [DllImport(ImageLibPath, CharSet = CharSet.Ansi)]
    private static extern bool Write(byte[] data, int rows, int cols, string path, string ext);
    [DllImport(ImageLibPath, CharSet = CharSet.Ansi)]
    private static extern void InitWriterWithPath(string path);

    public ImageOutputWriter() => InitWriterWithPath(ContainerPath);

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
                data[index] = byteValue;
                data[index + 1] = byteValue;
                data[index + 2] = byteValue;
            }
        }

        Write(data, width, height, "../../../../test", ImageTypes.BMP);
    }
}