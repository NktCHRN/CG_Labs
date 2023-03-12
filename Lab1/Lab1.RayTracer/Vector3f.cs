namespace Lab1.RayTracer;
public struct Vector3f
{
    public float X { get; set; }

    public float Y { get; set; }

    public float Z { get; set; }

    #region Constructor
    public Vector3f(float x, float y, float z) { X = x; Y = y; Z = z; }
    public Vector3f(float v) : this(v, v, v) {}
    public Vector3f() : this(0) {}

    public static Vector3f Zero { get { return new Vector3f(); } }
    public static Vector3f One { get { return new Vector3f(1.0f, 1.0f, 1.0f); } }
    #endregion

    #region Vector Math
    public float Length() => MathF.Sqrt(X * X + Y * Y + Z * Z);
    public float DotProduct(in Vector3f b) => X * b.X + Y * b.Y + Z * b.Z;
    public Vector3f CrossProduct(in Vector3f b) => new (Y * b.Z - Z * b.Y, Z * b.X - X * b.Z, X * b.Y - Y * b.X);
    public Vector3f Normalized() => this.Length() == 0 ? new Vector3f(0): this / this.Length();
    public Vector3f Abs() => new (Math.Abs(X), Math.Abs(Y), Math.Abs(Z));
    public Vector3f Rotation(Quaternion rotation)
    {
        Vector3f qv = new Vector3f(rotation.X, rotation.Y, rotation.Z);
        Vector3f cv = this;

        return (qv * 2.0f * qv.DotProduct(cv)) + (cv * (rotation.W * rotation.W - qv.DotProduct(qv))) + (qv.CrossProduct(cv) * 2.0f * rotation.W);
    }
    void Rotate(Quaternion rotation) {
		Vector3f qv = new Vector3f(rotation.X, rotation.Y, rotation.Z);
		this = (qv * 2.0f * qv.DotProduct(this)) + (this * (rotation.W * rotation.W - qv.DotProduct(qv))) + (qv.CrossProduct(this) * 2.0f * rotation.W);
	}
    #endregion

    #region Operator Overloading
    public static Vector3f operator +(Vector3f a) => a;
    public static Vector3f operator -(Vector3f a) => new (-a.X, -a.Y, -a.Z);
    public static Vector3f operator +(Vector3f a, Vector3f b) => new (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3f operator -(Vector3f a, Vector3f b) => a + (-b);

    public static Vector3f operator *(Vector3f a, Vector3f b) => new (a.X * b.X, a.Y * b.Y, a.Z * b.Z);
    public static Vector3f operator *(Vector3f a, float b) => new (a.X * b, a.Y * b, a.Z * b);
    public static Vector3f operator /(Vector3f a, Vector3f b)
    {
        if (b.X == 0 || b.Y == 0 || b.Z == 0)
            throw new DivideByZeroException();

        return new Vector3f(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
    }
    public static Vector3f operator /(Vector3f a, float b)
    {
        if (b == 0)
            throw new DivideByZeroException();

        return new Vector3f(a.X / b, a.Y / b, a.Z / b);
    }

    public static bool operator ==(Vector3f a, Vector3f b) => Equals(a, b);
    public static bool operator !=(Vector3f a, Vector3f b) => !Equals(a, b);
    #endregion

    #region Overridings
    public override bool Equals(object? obj) => obj is Vector3f b && X == b.X && Y == b.Y && Z == b.Z;
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
    #endregion
}
