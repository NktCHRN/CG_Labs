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

    public Color this[int i, int j]
    {
        get
        {
            if (i < 0 || i >= Height)
                throw new ArgumentOutOfRangeException(nameof(i));
            if (j < 0 || j >= Width)
                throw new ArgumentOutOfRangeException(nameof(j));

            return _map[i][j];
        }
        set
        {
            if (i < 0 || i >= Height)
                throw new ArgumentOutOfRangeException(nameof(i));
            if (j < 0 || j >= Width)
                throw new ArgumentOutOfRangeException(nameof(j));

            _map[i][j] = value;
        }
    }
}
