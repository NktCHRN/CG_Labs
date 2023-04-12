namespace Lab1.RayTracer.Tests;
public class SceneTests
{
    [Fact]
    public void Render_ReturnsStringWithAtLeastOneRenderedObject()
    {
        // Arrange
        var scene = new Scene(60, 40);

        var camera = new Camera(new Vector3F(0, -3.5F, 0), new Vector3F(0, 1, 0), new Vector3F(0, 0, 3), new Vector3F(0), 90);

        var object1 = new Rectangle(new Vector3F(0, 0.22F, 6), Vector3F.Zero, Vector3F.One);
        var object2 = new Sphere(new Vector3F(1.5F, 0, 0), new Vector3F(0, 0, 0), 3);
        var object3 = new Disk(new Vector3F(2, 2F, 0), new Vector3F(90, 6, 0), 7);
        scene.AddObject(object1);
        scene.AddObject(object2);
        scene.AddObject(object3);

        // Act
        var render = scene.Render(camera, null);

        // Assert
        Assert.Contains('#', render); 
        Assert.Contains(' ', render); 
    }

    [Fact]
    public void GetIntersectionVectorWithNearestObject_ReturnsIntersectionWithClosest_WhenMultipleObjects()
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

        var expectedVector = expectedPoint - ray.StartPoint;

        // Act
        var actualVector = scene.GetIntersectionNormalWithNearestObject(ray);

        // Assert
        Assert.Equal(expectedVector, actualVector);
    }
}
