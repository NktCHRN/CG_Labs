using Common;

namespace RayTracer.Abstractions;
public interface IRenderer
{
    Image Render(Scene scene);
}
