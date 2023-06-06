using Common;
using RayTracer.LightSources;
using RayTracer.Utility;

namespace RayTracer.Tests;
public class RendererTests
{
    [Fact]
    public void Render_ReturnsMatrixWithAtLeastOneRenderedObject()
    {
        // Arrange
        var camera = new Camera(new Vector3F(0, 0, -75F), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(0), 90);
        var scene = new Scene(camera);

        var object1 = new Rectangle(new Vector3F(0, 0.22F, 6), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), Vector3F.Zero, Vector3F.One);
        var object2 = new Sphere(new Vector3F(1.5F, 0, 0), new Vector3F(0, 0, 0), 10);
        var object3 = new Disk(new Vector3F(2, 2F, 0), new Vector3F(0, 0, 1), new Vector3F(0, 1, 0), new Vector3F(90, 6, 0), 1);
        scene.AddObject(object1);
        scene.AddObject(object2);
        scene.AddObject(object3);

        var light = new AmbientLightSource(1, Color.White);
        scene.AddLightSource(light);

        var sut = new Renderer(scene);

        // Act
        var image = sut.Render(60, 40);

        // Assert
        Assert.NotNull(image);
        Assert.Equal(Color.Black, image[39, 0]);
        Assert.Equal(Color.White, image[19, 29]);
    }

    [Fact]
    public void GetIntersectionWithClosestObject_ReturnsIntersectionWithClosest_MultipleObjects()
    {
        // Arrange
        var scene = new Scene(new Camera());

        var ray = new Ray(new Vector3F(0, -5, 0), new Vector3F(0, 0, 0));

        var expectedNearestObject = new Sphere(new Vector3F(0, 1, 0), new Vector3F(0, 0, 0), 2);      // closer
        var anotherObject = new Sphere(new Vector3F(0, 5, 0), new Vector3F(0, 0, 0), 3);
        scene.AddObject(expectedNearestObject);
        scene.AddObject(anotherObject);

        var expectedPoint = expectedNearestObject.Position;
        expectedPoint.Y -= expectedNearestObject.Radius;
        var expected = new Intersection(expectedPoint, expectedNearestObject);

        var sut = new Renderer(scene);

        // Act
        var actual = sut.GetIntersectionWithClosestObject(ray);

        // Assert
        Assert.Equal(expected, actual);
    }
}
