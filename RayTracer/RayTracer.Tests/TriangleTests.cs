namespace RayTracer.Tests;

public class TriangleTests
{
    [Theory]
    [MemberData(nameof(GetIntersection_IntersectsTriangle))]
    public void GetIntersection_ReturnsNotNull_WhenIntersects(Triangle sut)
    {
        // Arrange
        var ray = new Ray(new Vector3F(0.5f, 0.5f, 1), new Vector3F(0f, 0f, -1));

        // Act
        var actual = sut.GetIntersection(ray);

        // Assert
        Assert.NotNull(actual);
    }

    public static IEnumerable<object[]> GetIntersection_IntersectsTriangle =>
        new List<object[]>
    {
    new object[] { new Triangle(new Vertex { Position = new Vector3F(0.0F, 0.0F, 0.0F) }, new Vertex { Position = new Vector3F(1.0F, 0.0F, 0.0F) }, new Vertex { Position = new Vector3F(0.0F, 1.0F, 0.0F) }) },
    new object[] { new Triangle(new Vertex { Position = new Vector3F(0.0F, 0.0F, 0.0F) }, new Vertex { Position = new Vector3F(1.0F, 0.0F, 0.0F) }, new Vertex { Position = new Vector3F(0.0F, 1.0F, 0.0F) }) },
    new object[] { new Triangle(new Vertex { Position = new Vector3F(-1.0F, -1.0F, 0.0F) }, new Vertex { Position = new Vector3F(1.0F, -1.0F, 0.0F) }, new Vertex { Position = new Vector3F(0.0F, 1.0F, 0.0F) }) },
    new object[] { new Triangle(new Vertex { Position = new Vector3F(-1.0F, 0.0F, 0.0F) }, new Vertex { Position = new Vector3F(1.0F, 0.0F, 0.0F) }, new Vertex { Position = new Vector3F(0.0F, 1.0F, 0.0F) }) }
    };

    [Theory]
    [MemberData(nameof(GetIntersection_DoesNotIntersect_Data))]
    public void GetIntersection_ReturnsNull_WhenDoesNotIntersect(Triangle sut)
    {
        var ray = new Ray(new Vector3F(0.5f, 0.5f, 1.0f), new Vector3F(0.0f, 0.0f, -1.0f));

        // Act
        var actual = sut.GetIntersection(ray);

        // Assert
        Assert.Null(actual);
    }

    public static IEnumerable<object[]> GetIntersection_DoesNotIntersect_Data =>
        new List<object[]>
        {
            new object[] { new Triangle(new Vertex { Position = new Vector3F(-1.0F, -1.0F, 0.0F) }, new Vertex { Position = new Vector3F(1.0F, -1.0F, 0.0F) }, new Vertex { Position = new Vector3F(0.0F, -1.5F, 0.0F) }) },
            new object[] { new Triangle(new Vertex { Position = new Vector3F(-1.0F, -1.0F, 0.0F) }, new Vertex { Position = new Vector3F(1.0F, -1.0F, 0.0F) }, new Vertex { Position = new Vector3F(0.0F, -0.5F, 0.0F) }) },
            new object[] { new Triangle(new Vertex { Position = new Vector3F(-1.0F, -1.0F, 0.0F) }, new Vertex { Position = new Vector3F(1.0F, -1.0F, 0.0F) }, new Vertex { Position = new Vector3F(0.0F, 0.5F, 0.0F) }) }

        };

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
        yield return new object[] { new Triangle(new Vertex(), new Vertex { Position = new Vector3F(2, -1, 0) }, new Vertex { Position = new Vector3F(2, 1, 0) }), new Vector3F(0, 0, 1) };
        yield return new object[] { new Triangle(new Vertex(), new Vertex { Position = new Vector3F(-1, 0, 2) }, new Vertex { Position = new Vector3F(2, 0, 1) }), new Vector3F(0, 1, 0) };
        yield return new object[] { new Triangle(new Vertex(), new Vertex { Position = new Vector3F(0, 1, 2) }, new Vertex { Position = new Vector3F(0, -1, 2) }), new Vector3F(1, 0, 0) };
    }
}
