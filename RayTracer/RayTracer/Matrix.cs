using System.Text;

namespace RayTracer;

public class Matrix
{
    public Matrix(int width, int height)
    {
        _values = new float[height, width];
    }

    public Matrix(int x, bool isIdentity)
    {
        _values = new float[x, x];

        if (isIdentity)
            for (int i = 0; i < x; i++)
                _values[i, i] = 1;
    }

    public Matrix(float[,] values)
    {
        _values = values;
    }

    public float this[int x, int y]
    {
        get => _values[x, y];
        set => _values[x, y] = value;
    }

    public static Matrix operator *(Matrix a, Matrix b)
    {
        if (a.Width != b.Height)
            throw new ArgumentException("Invalid matrix dimensions for multiplication.");

        Matrix result = new (b.Width, a.Height);

        for (int y = 0; y < a.Height; y++)
        {
            for (int x = 0; x < b.Width; x++)
            {
                float sum = 0;
                for (int k = 0; k < a.Width; k++)
                    sum += a[y, k] * b[k, x];
                result[y, x] = sum;
            }
        }

        return result;
    }

    public void MultiplyBy(Matrix b)
    {
        if (Width != b.Width && Height != b.Height)
            throw new ArgumentException("Invalid matrix dimensions for multiplication.");

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < b.Width; x++)
            {
                float sum = 0;
                for (int k = 0; k < Width; k++)
                {
                    sum += this[y, k] * b[k, x];
                }
                this[y, x] = sum;
            }
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new ();

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                sb.Append(this[y, x]);
                sb.Append(' ');
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public int Width { get => _values.GetLength(1); }
    public int Height { get => _values.GetLength(0); }

    protected readonly float[,] _values;
}
