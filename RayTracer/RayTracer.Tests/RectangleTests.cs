namespace RayTracer.Tests;
public class RectangleTests
{
    [Theory]
    [MemberData(nameof(GetIntersection_IntersectsPlane_Data))]
    public void GetIntersection_ReturnsNotNull_WhenIntersects(Rectangle sut)
    {
        // Arrange
        var ray = new Ray(new Vector3F(0, 0, -25), Vector3F.Zero);

        // Act
        var actual = sut.GetIntersection(ray);

        // Assert
        Assert.NotNull(actual);
    }

    public static IEnumerable<object[]> GetIntersection_IntersectsPlane_Data =>
        new List<object[]>
    {
        new object[] { new Rectangle(new Vector3F(0, 0, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Vector3F.Zero, Vector3F.One) },
        new object[] { new Rectangle(new Vector3F(0, 0.5F, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Vector3F.Zero, Vector3F.One) },
        new object[] { new Rectangle(new Vector3F(0, -0.5F, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Vector3F.Zero, Vector3F.One) }
    };

    [Theory]
    [MemberData(nameof(GetIntersection_DoesNotIntersectsPlane_Data))]
    public void GetIntersection_ReturnsNull_WhenDoesNotIntersect(Rectangle sut)
    {
        // Arrange
        var ray = new Ray(new Vector3F(0, 0, -25), Vector3F.Zero);

        // Act
        var actual = sut.GetIntersection(ray);

        // Assert
        Assert.Null(actual);
    }

    public static IEnumerable<object[]> GetIntersection_DoesNotIntersectsPlane_Data =>
    new List<object[]>
    {
        new object[] { new Rectangle(new Vector3F(0, 0.55F, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Vector3F.Zero, Vector3F.One) },
        new object[] { new Rectangle(new Vector3F(0, -0.55F, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Vector3F.Zero, Vector3F.One) }
    };

    [Theory]
    [MemberData(nameof(GetNormalTestCases))]
    public void GetNormalAt_ReturnsNormal(Rectangle rectangle, Vector3F expected)
    {
        // Act
        var actual = rectangle.GetNormalAt(expected);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> GetNormalTestCases()
    {
        yield return new object[] { new Rectangle(Vector3F.Zero, new Vector3F(0, 1, 0), new Vector3F(0, 0, 1), Vector3F.Zero), new Vector3F(1, 0, 0) };
        yield return new object[] { new Rectangle(Vector3F.Zero, new Vector3F(0, 0, 1), new Vector3F(1, 0, 0), Vector3F.Zero), new Vector3F(0, 1, 0) };
        yield return new object[] { new Rectangle(Vector3F.Zero, new Vector3F(1, 0, 0), new Vector3F(0, 1, 0), Vector3F.Zero), new Vector3F(0, 0, 1) };
    }
}


