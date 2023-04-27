namespace RayTracer;

public class TransformationMatrix3D : Matrix
{
    public TransformationMatrix3D() : base(4, true) {}

    public Vector3F ApplyTo(Vector3F vec)
    {
        Matrix result = this * new Matrix(new float[,] { { vec.X }, { vec.Y }, { vec.Z }, { 1 } });
        return new Vector3F(result[0, 0], result[1, 0], result[2, 0]);
    }

    public void TranslateBy(Vector3F vec)
    {
        MultiplyBy(new Matrix(new float[,]
            {
                {1, 0, 0, vec.X},
                {0, 1, 0, vec.Y},
                {0, 0, 1, vec.Z},
                {0, 0, 0,     1},
            }
        ));
    }
    public void ScaleBy(Vector3F vec)
    {
        MultiplyBy(new Matrix(new float[,]
        {
            {vec.X, 0    , 0    , 0},
            {0    , vec.Y, 0    , 0},
            {0    , 0    , vec.Z, 0},
            {0    , 0    , 0    , 1},
        }));
    }
    public void RotateBy(Vector3F vec)
    {
        RotateAroundX(vec.X);
        RotateAroundY(vec.Y);
        RotateAroundZ(vec.Z);
    }

    private void RotateAroundX(float angle)
    {
        float sin = MathF.Sin(angle);
        float cos = MathF.Cos(angle);

        MultiplyBy(new Matrix(new float[,]
        {
            {1,    0,    0, 0},
            {0,  cos, -sin, 0},
            {0, -sin,  cos, 0},
            {0,    0,    0, 1},
        }));
    }

    private void RotateAroundY(float angle)
    {
        float sin = MathF.Sin(angle);
        float cos = MathF.Cos(angle);

        MultiplyBy(new Matrix(new float[,]
        {
            { cos, 0,  sin, 0},
            {  0 , 1,    0, 0},
            {-sin, 0,  cos, 0},
            {   0, 0,    0, 1},
        }));
    }

    private void RotateAroundZ(float angle)
    {
        float sin = MathF.Sin(angle);
        float cos = MathF.Cos(angle);

        MultiplyBy(new Matrix(new float[,]
        {
            { cos, -sin, 0, 0},
            {-sin,  cos, 0, 0},
            {   0,    0, 1, 0},
            {   0,    0, 0, 1},
        }));
    }
}
