namespace Lab1.RayTracer;
public static class CGMath
{
    public static float Clamp(float low, float high, float value) => Math.Max(low, Math.Min(high, value));

    public static float DegToRad(float deg) => deg * MathF.PI / 180;
    public static Vector3f DegToRad(Vector3f deg) => deg * MathF.PI / 180;

    public static float RadToDeg(float rad) => rad / MathF.PI * 180;
    public static Vector3f RadToDeg(Vector3f rad) => rad / MathF.PI * 180;
}
