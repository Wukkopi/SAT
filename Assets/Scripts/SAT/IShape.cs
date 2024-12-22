using UnityEngine;

public interface IShape
{
    Vector2 Position { get; }
    bool ComputeOverlap(IShape other, ref float distance, ref Vector2 direction);
    Projection GetProjection(Vector2 axis);
    Vector2 GetCenter();

}
