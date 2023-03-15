using System.ComponentModel;

namespace Lab1.RayTracer.Tests;
public class ThreeObjectsTests
{
    [Theory]
    [MemberData(nameof(ThreeDifferentObjects_Data))]
    public void CheckIfAtLeastOneObjectIsRendered(Camera camera)
    {
        // Arrange
        var scene = new Scene(60, 40);

        var object1 = new Plane(new Vector3F(0, 0.22F, 6), Vector3F.Zero, Vector3F.One);
        var object2 = new Sphere(new Vector3F(1.5F, 0, 0), new Vector3F(0, 0, 0), 3);
        var object3 = new Disk(new Vector3F(2, 2F, 0), new Vector3F(90, 6, 0), 7);

        // Act
        scene.AddObject(object1);
        scene.AddObject(object2);
        scene.AddObject(object3);
        var render = scene.Render(camera, null);

        // Assert
        Assert.Contains('#', render); 
        Assert.Contains(' ', render); 

    }

    public static IEnumerable<object[]> ThreeDifferentObjects_Data =>
        new List<object[]>
    {
        new object[] { new Sphere(new Vector3F(10, -1, 0), Vector3F.Zero, 1), new Ray(Vector3F.Zero, new Vector3F(1, 0, 0)) },
        new object[] { new Plane(new Vector3F(0, 0.5F, 0), Vector3F.Zero, Vector3F.One) },
        new object[] { new Disk(new Vector3F(0, 0, 0), new Vector3F(70, 0, 0), 3) },
    };
}