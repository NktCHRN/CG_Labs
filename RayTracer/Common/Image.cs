namespace Common;
public sealed class Image
{
    private readonly Color[][] _map;

    public int Width { get; }
    public int Height { get; }

    public bool HasAlpha { get; }

    public Image(int width, int height, bool hasAlpha = false)
    {
        _map = new Color[height][];
        for (var i = 0; i < height; i++)
        {
            _map[i] = new Color[width];
        }

        Width = width;
        Height = height;
        HasAlpha = hasAlpha;
    }

    public Color this[int row, int column]
    {
        get
        {
            if (row < 0 || row >= Height)
                throw new ArgumentOutOfRangeException(nameof(row));
            if (column < 0 || column >= Width)
                throw new ArgumentOutOfRangeException(nameof(column));

            return _map[row][column];
        }
        set
        {
            if (row < 0 || row >= Height)
                throw new ArgumentOutOfRangeException(nameof(row));
            if (column < 0 || column >= Width)
                throw new ArgumentOutOfRangeException(nameof(column));

            _map[row][column] = value;
        }
    }
}
