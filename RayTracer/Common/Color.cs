namespace Common;
public readonly struct Color
{
    public readonly byte Alpha { get; private init; } = byte.MaxValue;
    public readonly byte R { get; private init; }
    public readonly byte G { get; private init; }
    public readonly byte B { get; private init; }

    public readonly float LightCoefficient { get; private init; } = 1.0F;

    public static Color White => FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue);
    public static Color Black => FromRgb(0, 0, 0);

    public static Color FromShadowedRgb(float lightCoefficient, byte red, byte green, byte blue)
    {
        if (lightCoefficient is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(lightCoefficient), "Light coefficient must be in range [0;1]");
        }

        return new()
        {
            R = red,
            G = green,
            B = blue,
            LightCoefficient = lightCoefficient
        };
    }

    public static Color FromRgb(byte red, byte green, byte blue) => FromArgb(byte.MaxValue, red, green, blue);

    public static Color FromArgb(byte alpha, byte red, byte green, byte blue)
        => new()
        {
            Alpha = alpha,
            R = red,
            G = green,
            B = blue,
        };


    public static Color operator +(Color left, Color right)
    {
        return new()
        {
            R = (byte)Math.Min(left.R + right.R, byte.MaxValue),
            G = (byte)Math.Min(left.G + right.G, byte.MaxValue),
            B = (byte)Math.Min(left.B + right.B, byte.MaxValue),
            LightCoefficient = Math.Max(left.LightCoefficient, right.LightCoefficient)
        };
    }
}
