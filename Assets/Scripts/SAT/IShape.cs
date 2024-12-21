using UnityEngine;

public interface IShape
{
    Projection GetProjection(Vector2 axis);
    Vector2 Position { get; }
}
