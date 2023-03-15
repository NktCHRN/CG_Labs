namespace Lab1.RayTracer.Tests;
public class PlaneTests
{
    [Theory]
    [MemberData(nameof(GetIntersection_IntersectsPlane_Data))]
    public void GetIntersection_ReturnsNotNull_WhenIntersects(Plane sut)
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
        new object[] { new Plane(new Vector3F(0, 0, 0), Vector3F.Zero, Vector3F.One) },
        new object[] { new Plane(new Vector3F(0, 0.5F, 0), Vector3F.Zero, Vector3F.One) },
        new object[] { new Plane(new Vector3F(0, -0.5F, 0), Vector3F.Zero, Vector3F.One) }
    };

    [Theory]
    [MemberData(nameof(GetIntersection_DoesNotIntersectsPlane_Data))]
    public void GetIntersection_ReturnsNull_WhenDoesNotIntersect(Plane sut)
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
        new object[] { new Plane(new Vector3F(0, 0.55F, 0), Vector3F.Zero, Vector3F.One) },
        new object[] { new Plane(new Vector3F(0, -0.55F, 0), Vector3F.Zero, Vector3F.One) }
    };
}


