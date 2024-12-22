using UnityEngine;

public interface IShape
{
    bool ComputeOverlap(IShape other, ref float distance, ref Vector2 direction);
    Projection GetProjection(Vector2 axis);
    Vector2 GetCenter();
    Vector2 Position { get; }
}
