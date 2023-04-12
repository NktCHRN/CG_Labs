namespace Lab1.RayTracer;
public struct Quaternion {
    private Vector3F V;
    public float X { get => V.X; }
    public float Y { get => V.Y; }
    public float Z { get => V.Z; }
    public float W { get; set; }

    public Quaternion() : this(new Vector3F(0), 1) {}
	public Quaternion(Vector3F V, float W)
    {
        this.V = V;
        this.W = W;
    }

    public static Quaternion operator *(Quaternion a, Quaternion b)
    {
        return new Quaternion
        (
            new Vector3F
            (
                a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z,
                a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y,
                a.W * b.Y - a.X * b.Z + a.Y * b.W + a.Z * b.X
            ),
			a.W * b.Z + a.X * b.Y - a.Y * b.X + a.Z * b.W
		);
    }

    public static Quaternion FromEulerAngles(Vector3F v) => FromEulerAnglesRad(CGMath.DegToRad(v));
    public static Quaternion FromEulerAngles(float x, float y, float z) => FromEulerAngles(CGMath.DegToRad(x), CGMath.DegToRad(y), CGMath.DegToRad(z));
    public static Quaternion FromEulerAnglesRad(Vector3F v) => FromEulerAnglesRad(v.X, v.Y, v.Z);
    public static Quaternion FromEulerAnglesRad(float x, float y, float z)
    {
        // roll (x-axis rotation)
        float cosX = MathF.Cos(x * 0.5f);
        float sinX = MathF.Sin(x * 0.5f);

        // pitch (y-axis rotation)
        float cosY = MathF.Cos(y * 0.5f);
        float sinY = MathF.Sin(y * 0.5f);

        // yaw (z-axis rotation)
        float cosZ = MathF.Cos(z * 0.5f);
        float sinZ = MathF.Sin(z * 0.5f);

        float qX = sinX * cosY * cosZ - cosX * sinY * sinZ;
        float qY = cosX * sinY * cosZ + sinX * cosY * sinZ;
        float qZ = cosX * cosY * sinZ - sinX * sinY * cosZ;

        float w = cosX * cosY * cosZ + sinX * sinY * sinZ;

        return new Quaternion(new Vector3F(qX, qY, qZ), w);
    }

    public static Vector3F ToEulerAngles(Quaternion q)
    {
        // roll (x-axis rotation)
        float sinRCosP = 2 * (q.W * q.X + q.Y * q.Z);
        float cosRcosP = 1 - 2 * (q.X * q.X + q.Y * q.Y);
        float atanX = MathF.Atan2(sinRCosP, cosRcosP);

        // pitch (y-axis rotation)
        float sinP = MathF.Sqrt(1 + 2 * (q.W * q.Y - q.X * q.Z));
        float cosP = MathF.Sqrt(1 - 2 * (q.W * q.Y - q.X * q.Z));
        float atanY = 2 * MathF.Atan2(sinP, cosP) - MathF.PI / 2;

        // yaw (z-axis rotation)
        float sinYCosP = 2 * (q.W * q.Z + q.X * q.Y);
        float cosYCosP = 1 - 2 * (q.Y * q.Y + q.Z * q.Z);
        float atanZ = MathF.Atan2(sinYCosP, cosYCosP);

        return new Vector3F(atanX, atanY, atanZ);;
    }
}
