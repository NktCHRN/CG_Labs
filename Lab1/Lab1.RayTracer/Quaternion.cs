namespace Lab1.RayTracer;
public struct Quaternion {
    public float W { get; set; }
    public Vector3f V { get; set; }

    public Quaternion(float x, float y, float z)
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

        W = cx * cy * cz + sx * sy * sz;

        float qx = sx * cy * cz - cx * sy * sz;
        float qy = cx * sy * cz + sx * cy * sz;
        float qz = cx * cy * sz - sx * sy * cz;
        V = new Vector3f(qx, qy, qz);
    }
    public Quaternion(Vector3f rotation) : this(rotation.X, rotation.Y, rotation.Z) {}

    Vector3f ToEulerAngles(Quaternion q)
    {
        // roll (x-axis rotation)
        float sinr_cosp = 2 * (q.W * q.V.X + q.V.Y * q.V.Z);
        float cosr_cosp = 1 - 2 * (q.V.X * q.V.X + q.V.Y * q.V.Y);
        float ax = MathF.Atan2(sinr_cosp, cosr_cosp);

        // pitch (y-axis rotation)
        float sinp = MathF.Sqrt(1 + 2 * (q.W * q.V.Y - q.V.X * q.V.Z));
        float cosp = MathF.Sqrt(1 - 2 * (q.W * q.V.Y - q.V.X * q.V.Z));
        float ay = 2 * MathF.Atan2(sinp, cosp) - MathF.PI / 2;

        // yaw (z-axis rotation)
        float siny_cosp = 2 * (q.W * q.V.Z + q.V.X * q.V.Y);
        float cosy_cosp = 1 - 2 * (q.V.Y * q.V.Y + q.V.Z * q.V.Z);
        float az = MathF.Atan2(siny_cosp, cosy_cosp);

        return new Vector3f(ax, ay, az);;
    }
}
