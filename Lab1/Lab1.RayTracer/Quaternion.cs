namespace Lab1.RayTracer;
public struct Quaternion {
    private Vector3f V;
    public float X { get => V.X; }
    public float Y { get => V.Y; }
    public float Z { get => V.Z; }
    public float W { get; set; }

    public Quaternion() : this(new Vector3f(0), 1) {}
	public Quaternion(Vector3f V, float W)
    {
        this.V = V;
        this.W = W;
    }

    public static Quaternion operator *(Quaternion a, Quaternion b)
    {
        return new Quaternion
        (
            new Vector3f
            (
                a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z,
                a.W * b.X + a.X * b.W + a.Y * b.Z - a.Z * b.Y,
                a.W * b.Y - a.X * b.Z + a.Y * b.W + a.Z * b.X
            ),
			a.W * b.Z + a.X * b.Y - a.Y * b.X + a.Z * b.W
		);
    }

    public static Quaternion FromEulerAngles(Vector3f v) => Quaternion.FromEulerAngles(v.X, v.Y, v.Z);
    public static Quaternion FromEulerAngles(float x, float y, float z)
    {
        // roll (x-axis rotation)
        float cx = MathF.Cos(x * 0.5f);
        float sx = MathF.Sin(x * 0.5f);

        // pitch (y-axis rotation)
        float cy = MathF.Cos(y * 0.5f);
        float sy = MathF.Sin(y * 0.5f);

        // yaw (z-axis rotation)
        float cz = MathF.Cos(z * 0.5f);
        float sz = MathF.Sin(z * 0.5f);

        float qx = sx * cy * cz - cx * sy * sz;
        float qy = cx * sy * cz + sx * cy * sz;
        float qz = cx * cy * sz - sx * sy * cz;

        float w = cx * cy * cz + sx * sy * sz;

        return new Quaternion(new Vector3f(qx, qy, qz), w);
    }

    public static Vector3f ToEulerAngles(Quaternion q)
    {
        // roll (x-axis rotation)
        float sinr_cosp = 2 * (q.W * q.X + q.Y * q.Z);
        float cosr_cosp = 1 - 2 * (q.X * q.X + q.Y * q.Y);
        float ax = MathF.Atan2(sinr_cosp, cosr_cosp);

        // pitch (y-axis rotation)
        float sinp = MathF.Sqrt(1 + 2 * (q.W * q.Y - q.X * q.Z));
        float cosp = MathF.Sqrt(1 - 2 * (q.W * q.Y - q.X * q.Z));
        float ay = 2 * MathF.Atan2(sinp, cosp) - MathF.PI / 2;

        // yaw (z-axis rotation)
        float siny_cosp = 2 * (q.W * q.Z + q.X * q.Y);
        float cosy_cosp = 1 - 2 * (q.Y * q.Y + q.Z * q.Z);
        float az = MathF.Atan2(siny_cosp, cosy_cosp);

        return new Vector3f(ax, ay, az);;
    }
}
