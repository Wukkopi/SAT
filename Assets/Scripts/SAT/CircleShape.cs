using UnityEngine;
public class CircleShape : MonoBehaviour, IShape
{
    public float radius;
    public Vector2 position;

    public Projection GetProjection(Vector2 axis)
    {
        var d = Vector2.Dot(axis, position);
        return new Projection(d - radius, d + radius);
    }
}