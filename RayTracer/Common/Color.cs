namespace Common;
public readonly record struct Color
{
    public Color()
    {
    }

    public readonly byte Alpha { get; private init; } = byte.MaxValue;
    public readonly byte R { get; private init; } = 0;
    public readonly byte G { get; private init; } = 0;
    public readonly byte B { get; private init; } = 0;

    public readonly float LightCoefficient { get; private init; } = 1.0F;

    public static Color White
        => new()
        {
            LightCoefficient = 1,
            R = byte.MaxValue,
            G = byte.MaxValue,
            B = byte.MaxValue,
        };
    public static Color Black 
        => new()
        {
            LightCoefficient = 0,
            R = 0,
            G = 0,
            B = 0,
        };

    public static Color FromShadowedColor(float lightCoefficient, Color color)
    {
        if (lightCoefficient is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(lightCoefficient), "Light coefficient must be in range [0;1]");
        }

        return new()
        {
            Alpha = color.Alpha,
            R = (byte)Math.Round(color.R * lightCoefficient, MidpointRounding.AwayFromZero),
            G = (byte)Math.Round(color.G * lightCoefficient, MidpointRounding.AwayFromZero),
            B = (byte)Math.Round(color.B * lightCoefficient, MidpointRounding.AwayFromZero),
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
