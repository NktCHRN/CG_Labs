using RayTracer.SceneObjects;

namespace RayTracer.Utility;
public record struct Intersection(Vector3F Point, ISceneObject Object);
