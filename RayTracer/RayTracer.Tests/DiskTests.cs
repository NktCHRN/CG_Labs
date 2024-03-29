﻿namespace RayTracer.Tests;
public class DiskTests
{
    [Theory]
    [MemberData(nameof(GetIntersection_IntersectsDisk_Data))]
    public void GetIntersection_ReturnsNotNull_WhenIntersects(Disk sut)
    {
        // Arrange
        var ray = new Ray(new Vector3F(0, 0, -25), Vector3F.Zero);

        // Act
        var actual = sut.GetIntersection(ray);

        // Assert
        Assert.NotNull(actual);
    }

    public static IEnumerable<object[]> GetIntersection_IntersectsDisk_Data =>
        new List<object[]>
    {
        new object[] { new Disk(new Vector3F(0, 0, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Vector3F.Zero, 3) },
        new object[] { new Disk(new Vector3F(0, 3F, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Vector3F.Zero, 3) },
        new object[] { new Disk(new Vector3F(-3F, 0, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Vector3F.Zero, 3) },
        new object[] { new Disk(new Vector3F(0, 0, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(70, 0, 0), 3) },
        new object[] { new Disk(new Vector3F(0, -1F, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(70, 0, 0), 3) },
    };

    [Theory]
    [MemberData(nameof(GetIntersection_DoesNotIntersect_Data))]
    public void GetIntersection_ReturnsNull_WhenDoesNotIntersect(Disk sut)
    {
        // Arrange
        var ray = new Ray(new Vector3F(0, 0, -25), Vector3F.Zero);

        // Act
        var actual = sut.GetIntersection(ray);

        // Assert
        Assert.Null(actual);
    }

    public static IEnumerable<object[]> GetIntersection_DoesNotIntersect_Data =>
    new List<object[]>
    {
        new object[] { new Disk(new Vector3F(0, 3.1F, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Vector3F.Zero, 3) },
        new object[] { new Disk(new Vector3F(-4F, 0, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Vector3F.Zero, 3) },
        new object[] { new Disk(new Vector3F(0, 1.5F, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(70, 0, 0), 3) },
        new object[] { new Disk(new Vector3F(0, -1.5F, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(70, 0, 0), 3) },
    };

    [Theory]
    [MemberData(nameof(GetNormalTestCases))]
    public void GetNormalAt_ReturnsNormal(Disk disk, Vector3F expected)
    {
        // Act
        var actual = disk.GetNormalAt(expected);

        // Assert
        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> GetNormalTestCases()
    {
        yield return new object[] { new Disk(Vector3F.Zero, new Vector3F(0, 1, 0), new Vector3F(0, 0, 1), Vector3F.Zero), new Vector3F(1, 0, 0) };
        yield return new object[] { new Disk(Vector3F.Zero, new Vector3F(0, 0, 1), new Vector3F(1, 0, 0), Vector3F.Zero), new Vector3F(0, 1, 0) };
        yield return new object[] { new Disk(Vector3F.Zero, new Vector3F(1, 0, 0), new Vector3F(0, 1, 0), Vector3F.Zero), new Vector3F(0, 0, 1) };
    }
}
