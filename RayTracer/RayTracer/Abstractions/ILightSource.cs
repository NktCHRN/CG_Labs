using RayTracer.Utility;

namespace RayTracer.Abstractions;
public interface ILightSource
{
    float GetLightCoefficient(Intersection? intersection);
}
