using UnityEngine;
public abstract class Shape : MonoBehaviour, IShape
{
    public Vector2 Position => new Vector2(transform.position.x, transform.position.y);

    public abstract Projection GetProjection(Vector2 axis);
}