namespace RayTracer.Tests;
public class SphereTests
{
    [Theory]
    [MemberData(nameof(GetIntersection_IntersectsInOnePoint_Data))]
    public void GetIntersection_ReturnsNotNull_WhenIntersectsInOnePoint(Sphere sut, Ray ray)
    {
        // Act
        var actual = sut.GetIntersection(ray);

        // Assert
        Assert.NotNull(actual);
    }

    public static IEnumerable<object[]> GetIntersection_IntersectsInOnePoint_Data =>
        new List<object[]>
    {
        new object[] { new Sphere(new Vector3F(10, -1, 0), Vector3F.Zero, 1), new Ray(Vector3F.Zero, new Vector3F(1, 0, 0)) },
        new object[] { new Sphere(new Vector3F(0, 10, -3), Vector3F.Zero, 3), new Ray(Vector3F.Zero, new Vector3F(0, 1, 0)) },
        new object[] { new Sphere(new Vector3F(-5, 0, 10), Vector3F.Zero, 5), new Ray(Vector3F.Zero, new Vector3F(0, 0, 1)) },
    };

    [Theory]
    [MemberData(nameof(GetIntersection_IntersectsInTwoPoints_Data))]
    public void IsIntersectedBy_ReturnsNotNull_WhenIntersectsInTwoPoints(Sphere sut, Ray ray)
    {
        // Act
        var actual = sut.GetIntersection(ray);

        // Assert
        Assert.NotNull(actual);
    }

    public static IEnumerable<object[]> GetIntersection_IntersectsInTwoPoints_Data =>
        new List<object[]>
    {
        new object[] { new Sphere(new Vector3F(10, 0, 0), Vector3F.Zero, 1), new Ray(Vector3F.Zero, new Vector3F(1, 0, 0)) },
        new object[] { new Sphere(new Vector3F(0, 10, -2), Vector3F.Zero, 3), new Ray(Vector3F.Zero, new Vector3F(0, 1, 0)) },
        new object[] { new Sphere(new Vector3F(-4.5F, 0, 10), Vector3F.Zero, 5), new Ray(Vector3F.Zero, new Vector3F(0, 0, 1)) },
    };

    [Theory]
    [MemberData(nameof(GetIntersection_DoesNotIntersect_Data))]
    public void GetIntersection_ReturnsNull_WhenDoesNotIntersect(Sphere sut, Ray ray)
    {
        // Act
        var actual = sut.GetIntersection(ray);

        // Assert
        Assert.Null(actual);
    }

    public static IEnumerable<object[]> GetIntersection_DoesNotIntersect_Data =>
    new List<object[]>
    {
        new object[] { new Sphere(new Vector3F(10, -2, 0), Vector3F.Zero, 1), new Ray(Vector3F.Zero, new Vector3F(1, 0, 0)) },
        new object[] { new Sphere(new Vector3F(0, 10, -3.1F), Vector3F.Zero, 3), new Ray(Vector3F.Zero, new Vector3F(0, 1, 0)) },
        new object[] { new Sphere(new Vector3F(-5.5F, 0, 10), Vector3F.Zero, 5), new Ray(Vector3F.Zero, new Vector3F(0, 0, 1)) },
    };
}