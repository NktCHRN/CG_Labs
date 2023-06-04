namespace ImageConverter.Common;
public readonly struct Color
{
    public byte Alpha { get; private init; }
    public byte Red { get; private init; }
    public byte Green { get; private init; }
    public byte Blue { get; private init; }

    public static Color FromRgb(byte red, byte green, byte blue) => FromArgb(byte.MaxValue, red, green, blue);

    public static Color FromArgb(byte alpha, byte red, byte green, byte blue)
        => new()
        {
            Red = red,
            Green = green,
            Blue = blue,
            Alpha = alpha
        };
}
