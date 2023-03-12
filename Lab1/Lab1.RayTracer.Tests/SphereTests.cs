using Lab1.RayTracer.SceneObjects;

namespace Lab1.RayTracer.Tests;
public class SphereTests
{
    [Theory]
    [MemberData(nameof(IsIntersectedBy_IntersectsInOnePoint_Data))]
    public void IsIntersectedBy_ReturnsTrue_WhenIntersectsInOnePoint(Sphere sut, Ray ray)
    {
        // Act
        var actual = sut.IsIntersectedBy(ray);

        // Assert
        Assert.True(actual);
    }

    public static IEnumerable<object[]> IsIntersectedBy_IntersectsInOnePoint_Data =>
        new List<object[]>
    {
        new object[] { new Sphere(new Vector3f(10, -1, 0), Vector3f.Zero, 1), new Ray(Vector3f.Zero, new Vector3f(1, 0, 0)) },
        new object[] { new Sphere(new Vector3f(0, 10, -3), Vector3f.Zero, 3), new Ray(Vector3f.Zero, new Vector3f(0, 1, 0)) },
        new object[] { new Sphere(new Vector3f(-5, 0, 10), Vector3f.Zero, 5), new Ray(Vector3f.Zero, new Vector3f(0, 0, 1)) },
    };

    [Theory]
    [MemberData(nameof(IsIntersectedBy_IntersectsInTwoPoints_Data))]
    public void IsIntersectedBy_ReturnsTrue_WhenIntersectsInTwoPoints(Sphere sut, Ray ray)
    {
        // Act
        var actual = sut.IsIntersectedBy(ray);

        // Assert
        Assert.True(actual);
    }

    public static IEnumerable<object[]> IsIntersectedBy_IntersectsInTwoPoints_Data =>
        new List<object[]>
    {
        new object[] { new Sphere(new Vector3f(10, 0, 0), Vector3f.Zero, 1), new Ray(Vector3f.Zero, new Vector3f(1, 0, 0)) },
        new object[] { new Sphere(new Vector3f(0, 10, -2), Vector3f.Zero, 3), new Ray(Vector3f.Zero, new Vector3f(0, 1, 0)) },
        new object[] { new Sphere(new Vector3f(-4.5F, 0, 10), Vector3f.Zero, 5), new Ray(Vector3f.Zero, new Vector3f(0, 0, 1)) },
    };

    [Theory]
    [MemberData(nameof(IsIntersectedBy_DoesNotIntersect_Data))]
    public void IsIntersectedBy_ReturnsFalse_WhenDoesNotIntersect(Sphere sut, Ray ray)
    {
        // Act
        var actual = sut.IsIntersectedBy(ray);

        // Assert
        Assert.False(actual);
    }

    public static IEnumerable<object[]> IsIntersectedBy_DoesNotIntersect_Data =>
    new List<object[]>
    {
        new object[] { new Sphere(new Vector3f(10, -2, 0), Vector3f.Zero, 1), new Ray(Vector3f.Zero, new Vector3f(1, 0, 0)) },
        new object[] { new Sphere(new Vector3f(0, 10, -3.1F), Vector3f.Zero, 3), new Ray(Vector3f.Zero, new Vector3f(0, 1, 0)) },
        new object[] { new Sphere(new Vector3f(-5.5F, 0, 10), Vector3f.Zero, 5), new Ray(Vector3f.Zero, new Vector3f(0, 0, 1)) },
    };
}