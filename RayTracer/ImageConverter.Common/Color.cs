namespace ImageConverter.Common;
public readonly struct Color
{
    public readonly byte Alpha { get; }
    public readonly byte Red { get; }
    public readonly byte Green { get; }
    public readonly byte Blue { get; }

    private Color(byte alpha, byte red, byte green, byte blue)
    {
        Alpha = alpha;
        Red = red;
        Green = green;
        Blue = blue;
    }

    public static Color FromRgb(byte red, byte green, byte blue) => FromArgb(byte.MaxValue, red, green, blue);

    public static Color FromArgb(byte alpha, byte red, byte green, byte blue) => new(alpha, red, green, blue);
}
