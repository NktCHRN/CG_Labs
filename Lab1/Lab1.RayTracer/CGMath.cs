namespace Lab1.RayTracer;
public static class CGMath
{
    public static float clamp(float low, float high, float value) => Math.Max(low, Math.Min(high, value));

    public static float deg2rad(float deg) => deg * MathF.PI / 180;
    public static Vector3f deg2rad(Vector3f deg) => deg * MathF.PI / 180;

    public static float rad2deg(float rad) => rad / MathF.PI * 180;
    public static Vector3f rad2deg(Vector3f rad) => rad / MathF.PI * 180;
}