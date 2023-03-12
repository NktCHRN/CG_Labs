namespace Lab1.RayTracer.Tests;
public class DiskTests
{
    [Theory]
    [MemberData(nameof(IsIntersectedBy_IntersectsDisk_Data))]
    public void IsIntersectedBy_ReturnsTrue_WhenIntersects(Disk sut)
    {
        // Arrange
        var ray = new Ray(new Vector3F(0, 0, -25), Vector3F.Zero);

        // Act
        var actual = sut.IsIntersectedBy(ray);

        // Assert
        Assert.True(actual);
    }

    public static IEnumerable<object[]> IsIntersectedBy_IntersectsDisk_Data =>
        new List<object[]>
    {
        new object[] { new Disk(new Vector3F(0, 0, 0), Vector3F.Zero, 3) },
        new object[] { new Disk(new Vector3F(0, 3F, 0), Vector3F.Zero, 3) },
        new object[] { new Disk(new Vector3F(-3F, 0, 0), Vector3F.Zero, 3) },
        new object[] { new Disk(new Vector3F(0, 0, 0), new Vector3F(70, 0, 0), 3) },
        new object[] { new Disk(new Vector3F(0, -1F, 0), new Vector3F(70, 0, 0), 3) },
    };

    [Theory]
    [MemberData(nameof(IsIntersectedBy_DoesNotIntersect_Data))]
    public void IsIntersectedBy_ReturnsFalse_WhenDoesNotIntersect(Disk sut)
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
        new object[] { new Disk(new Vector3F(0, 3.1F, 0), Vector3F.Zero, 3) },
        new object[] { new Disk(new Vector3F(-4F, 0, 0), Vector3F.Zero, 3) },
        new object[] { new Disk(new Vector3F(0, 1.5F, 0), new Vector3F(70, 0, 0), 3) },
        new object[] { new Disk(new Vector3F(0, -1.5F, 0), new Vector3F(70, 0, 0), 3) },
    };
}
