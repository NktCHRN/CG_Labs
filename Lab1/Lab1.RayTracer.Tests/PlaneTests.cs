namespace Lab1.RayTracer.Tests;
public class PlaneTests
{
    [Theory]
    [MemberData(nameof(IsIntersectedBy_IntersectsPlane_Data))]
    public void IsIntersectedBy_ReturnsTrue_WhenIntersects(Plane sut)
    {
        // Arrange
        var ray = new Ray(new Vector3F(0, 0, -25), Vector3F.Zero);

        // Act
        var actual = sut.IsIntersectedBy(ray);

        // Assert
        Assert.True(actual);
    }

    public static IEnumerable<object[]> IsIntersectedBy_IntersectsPlane_Data =>
        new List<object[]>
    {
        new object[] { new Plane(new Vector3F(0, 0, 0), Vector3F.Zero, Vector3F.One) },
        new object[] { new Plane(new Vector3F(0, 0.5F, 0), Vector3F.Zero, Vector3F.One) },
        new object[] { new Plane(new Vector3F(0, -0.5F, 0), Vector3F.Zero, Vector3F.One) }
    };

    [Theory]
    [MemberData(nameof(IsIntersectedBy_DoesNotIntersect_Data))]
    public void IsIntersectedBy_ReturnsFalse_WhenDoesNotIntersect(Plane sut)
    {
        // Arrange
        var ray = new Ray(new Vector3F(0, 0, -25), Vector3F.Zero);

        // Act
        var actual = sut.IsIntersectedBy(ray);

        // Assert
        Assert.False(actual);
    }

    public static IEnumerable<object[]> IsIntersectedBy_DoesNotIntersect_Data =>
    new List<object[]>
    {
        new object[] { new Plane(new Vector3F(0, 0.55F, 0), Vector3F.Zero, Vector3F.One) },
        new object[] { new Plane(new Vector3F(0, -0.55F, 0), Vector3F.Zero, Vector3F.One) }
    };
}
