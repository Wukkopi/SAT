using UnityEngine;
public abstract class Shape : MonoBehaviour, IShape
{
    public bool IsStatic;
    public Vector2 Position => new Vector2(transform.position.x, transform.position.y);

    public abstract Vector2 GetCenter();

    public abstract Projection GetProjection(Vector2 axis);
    public abstract bool ComputeOverlap(IShape other, ref float distance, ref Vector2 direction);
}