namespace Lab1.RayTracer;
public struct Vector3f
{
    public float x, y, z;

    #region Constructor
    public Vector3f(float _x, float _y, float _z) { x = _x; y = _y; z = _z; }
    public Vector3f(float v) : this(v, v, v) {}
    public Vector3f() : this(0) {}

    public static Vector3f Zero { get { return new Vector3f(); } }
    public static Vector3f One { get { return new Vector3f(1.0f, 1.0f, 1.0f); } }
    #endregion

    #region Vector Math
    public float Length() => MathF.Sqrt(x * x + y * y + z * z);
    public float DotProduct(in Vector3f b) => x * b.x + y * b.y + z * b.z;
    public Vector3f CrossProduct(in Vector3f b) => new Vector3f(y * b.z - z * b.y, z * b.x - x * b.z, x * b.y - y * b.x);
    public Vector3f Normalized() => this.Length() == 0 ? new Vector3f(0): this / this.Length();
    public Vector3f Abs() => new Vector3f(Math.Abs(x), Math.Abs(y), Math.Abs(z));
    public Vector3f RotatedBy(Vector3f rotation)
    {
        Vector3f radRot = CGMath.deg2rad(rotation);
        float radX = radRot.x;
        float radY = radRot.y;
        float radZ = radRot.z;
        
        Vector3f vX = new Vector3f(1, 0, 0);
        Vector3f vY = new Vector3f(0, 1, 0);
        Vector3f vZ = new Vector3f(0, 0, 1);
        
        Vector3f result = this;
        
        float cosX = MathF.Cos(radX);
        float sinX = MathF.Sin(radX);
        
        float cosY = MathF.Cos(radY);
        float sinY = MathF.Sin(radY);
        
        float cosZ = MathF.Cos(radZ);
        float sinZ = MathF.Sin(radZ);
        
        if (radX != 0)
            result = ((result * cosX + vX.CrossProduct(result) * sinX + vX * vX.DotProduct(result)) * (1 - cosX)).Normalized();
        
        if (radY != 0)
            result = ((result * cosY + vY.CrossProduct(result) * sinY + vY * vY.DotProduct(result)) * (1 - cosY)).Normalized();
        
        if (radZ != 0)
            result = ((result * cosZ + vZ.CrossProduct(result) * sinZ + vZ * vZ.DotProduct(result)) * (1 - cosZ)).Normalized();
        
        return result;
    }
    #endregion

    #region Operator Overloading
    public static Vector3f operator +(Vector3f a) => a;
    public static Vector3f operator -(Vector3f a) => new Vector3f(-a.x, -a.y, -a.z);
    public static Vector3f operator +(Vector3f a, Vector3f b) => new Vector3f(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Vector3f operator -(Vector3f a, Vector3f b) => a + (-b);

    public static Vector3f operator *(Vector3f a, Vector3f b) => new Vector3f(a.x * b.x, a.y * b.y, a.z * b.z);
    public static Vector3f operator *(Vector3f a, float b) => new Vector3f(a.x * b, a.y * b, a.z * b);
    public static Vector3f operator /(Vector3f a, Vector3f b)
    {
        if (b.x == 0 || b.y == 0 || b.z == 0)
            throw new DivideByZeroException();

        return new Vector3f(a.x / b.x, a.y / b.y, a.z / b.z);
    }
    public static Vector3f operator /(Vector3f a, float b)
    {
        if (b == 0)
            throw new DivideByZeroException();

        return new Vector3f(a.x / b, a.y / b, a.z / b);
    }

    public static bool operator ==(Vector3f a, Vector3f b) => a.x == b.x && a.y == b.y && a.z == b.z;
    public static bool operator !=(Vector3f a, Vector3f b) => a.x != b.x || a.y != b.y || a.z != b.z;
    #endregion

    #region Overridings
    public override bool Equals(object? obj) => obj is Vector3f v && x == v.x && y == v.y && z == v.z;
    public override int GetHashCode() => HashCode.Combine(x, y, z);
    #endregion
}