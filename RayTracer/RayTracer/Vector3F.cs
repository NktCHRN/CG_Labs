namespace RayTracer;
public struct Vector3F
{
    public float X { get; set; }

    public float Y { get; set; }

    public float Z { get; set; }

    #region Constructor
    public Vector3F(float x, float y, float z) { X = x; Y = y; Z = z; }
    public Vector3F(float v) : this(v, v, v) {}
    public Vector3F() : this(0) {}

    public static Vector3F Zero => new();
    public static Vector3F One => new(1, 1, 1);
    #endregion

    #region Vector Math
    public float Length => MathF.Sqrt(X * X + Y * Y + Z * Z);
    public float DotProduct(in Vector3F b) => X * b.X + Y * b.Y + Z * b.Z;
    public Vector3F CrossProduct(in Vector3F b) => new (Y * b.Z - Z * b.Y, Z * b.X - X * b.Z, X * b.Y - Y * b.X);
    public Vector3F Normalized => Length == 0 ? new Vector3F(0) : this / Length;
    public Vector3F Abs() => new (Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
    public Vector3F RotatedBy(Vector3F rotation)
    {
        var radRot = CGMath.DegToRad(rotation);
        var radX = radRot.X;
        var radY = radRot.Y;
        var radZ = radRot.Z;
        
        var vX = new Vector3F(1, 0, 0);
        var vY = new Vector3F(0, 1, 0);
        var vZ = new Vector3F(0, 0, 1);

        var result = this;

        var cosX = MathF.Cos(radX);
        var sinX = MathF.Sin(radX);
        
        float cosY = MathF.Cos(radY);
        float sinY = MathF.Sin(radY);

        var cosZ = MathF.Cos(radZ);
        var sinZ = MathF.Sin(radZ);
        
        if (radX != 0)
            result = ((result * cosX + vX.CrossProduct(result) * sinX + vX * vX.DotProduct(result)) * (1 - cosX)).Normalized;
        
        if (radY != 0)
            result = ((result * cosY + vY.CrossProduct(result) * sinY + vY * vY.DotProduct(result)) * (1 - cosY)).Normalized;
        
        if (radZ != 0)
            result = ((result * cosZ + vZ.CrossProduct(result) * sinZ + vZ * vZ.DotProduct(result)) * (1 - cosZ)).Normalized;
        
        return result;
    }
    #endregion

    #region Operator Overloading
    public static Vector3F operator +(Vector3F a) => a;
    public static Vector3F operator -(Vector3F a) => new (-a.X, -a.Y, -a.Z);
    public static Vector3F operator +(Vector3F a, Vector3F b) => new (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3F operator -(Vector3F a, Vector3F b) => a + (-b);

    public static Vector3F operator *(Vector3F a, Vector3F b) => new (a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    public static Vector3F operator *(Vector3F a, float b) => new (a.X * b, a.Y * b, a.Z * b);
    public static Vector3F operator /(Vector3F a, Vector3F b)
    {
        if (b.X == 0 || b.Y == 0 || b.Z == 0)
            throw new DivideByZeroException();

        return new Vector3F(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
    }
    public static Vector3F operator /(Vector3F a, float b)
    {
        if (b == 0)
            throw new DivideByZeroException();

        return new Vector3F(a.X / b, a.Y / b, a.Z / b);
    }

    public static bool operator ==(Vector3F a, Vector3F b) => Equals(a, b);
    public static bool operator !=(Vector3F a, Vector3F b) => !Equals(a, b);
    #endregion

    #region Overridings
    public override bool Equals(object? obj) => obj is Vector3F b && X == b.X && Y == b.Y && Z == b.Z;
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
    #endregion
}
