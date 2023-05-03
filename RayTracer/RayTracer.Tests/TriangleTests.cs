namespace RayTracer.Tests;
public class TriangleTests
{
    [Theory]
    [MemberData(nameof(GetNormalTestCases))]
    public void GetNormalAt_ReturnsNormal(Triangle triangle, Vector3F expected)
    {
        // Act
        var actual = triangle.GetNormalAt(expected);
        
        // Assert
        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> GetNormalTestCases()
    {
        yield return new object[] { new Triangle(new Vertex(), new Vertex { Position = new Vector3F(2, 1, 0)}, new Vertex { Position = new Vector3F(2, -1, 0) }), new Vector3F(0, 0, 1) };
        yield return new object[] { new Triangle(new Vertex(), new Vertex { Position = new Vector3F(2, 0, 1) }, new Vertex { Position = new Vector3F(-1, 0, 2) }), new Vector3F(0, 1, 0) };
        yield return new object[] { new Triangle(new Vertex(), new Vertex { Position = new Vector3F(0, 1, 2) }, new Vertex { Position = new Vector3F(0, 2, -1) }), new Vector3F(1, 0, 0) };
    }
}
