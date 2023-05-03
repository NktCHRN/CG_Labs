using RayTracer.Utility;

namespace RayTracer.Tests;
public class SceneTests
{
    [Fact]
    public void Render_ReturnsMatrixWithAtLeastOneRenderedObject()
    {
        // Arrange
        var scene = new Scene(60, 40);

        var camera = new Camera(new Vector3F(0, -3.5F, 0), new Vector3F(0, 1, 0), new Vector3F(0, 0, 3), new Vector3F(0), 90);

        var object1 = new Rectangle(new Vector3F(0, 0.22F, 6), Vector3F.Zero, Vector3F.One);
        var object2 = new Sphere(new Vector3F(1.5F, 0, 0), new Vector3F(0, 0, 0), 3);
        var object3 = new Disk(new Vector3F(2, 2F, 0), new Vector3F(90, 6, 0), 1);
        scene.AddObject(object1);
        scene.AddObject(object2);
        scene.AddObject(object3);

        // Act
        var matrix = scene.Render(camera, null);

        // Assert
        Assert.Equal(0, matrix[39, 0]);
        Assert.Equal(1, matrix[19, 29]);
    }

    [Fact]
    public void GetIntersectionWithClosestObject_ReturnsIntersectionWithClosest_MultipleObjects()
    {
        // Arrange
        var scene = new Scene(1, 1);

        var ray = new Ray(new Vector3F(0, -5, 0), new Vector3F(0, 0, 0));

        var expectedNearestObject = new Sphere(new Vector3F(0, 1, 0), new Vector3F(0, 0, 0), 2);      // closer
        var anotherObject = new Sphere(new Vector3F(0, 5, 0), new Vector3F(0, 0, 0), 3);
        scene.AddObject(expectedNearestObject);
        scene.AddObject(anotherObject);

        var expectedPoint = expectedNearestObject.Position;
        expectedPoint.Y -= expectedNearestObject.Radius;
        var expected = new Intersection(expectedPoint, expectedNearestObject);

        // Act
        var actual = scene.GetIntersectionWithClosestObject(ray);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void HasIntersectionWithAnyObject_ReturnsTrue_HasIntersection()
    {
        // Arrange
        var scene = new Scene(1, 1);
        var intersectedObject = new Sphere(new Vector3F(3F, 0, 5), Vector3F.Zero, 1);
        scene.AddObject(intersectedObject);

        var initialIntersectionPoint = new Vector3F(1F, 0, 5);
        var light = new Vector3F(-1, 0, 0);

        var ray = new Ray(initialIntersectionPoint, initialIntersectionPoint - light);

        // Act
        var actual = scene.HasIntersectionWithAnyObject(ray);

        // Assert
        Assert.True(actual);
    }
}
