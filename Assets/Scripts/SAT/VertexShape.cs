using UnityEngine;

public class VertexShape : MonoBehaviour, IShape
{
    public Vector2[] vertices;

    public Vector2[] GetNormals()
    {
        Vector2[] axes = new Vector2[vertices.Length];

        for (var i = 0; i < axes.Length; i++)
        {
            axes[i] = i > 0 ? vertices[i] - vertices[i - 1] : vertices[^1] - vertices[0];   
            axes[i] = new Vector2(-axes[i].y, axes[i].x).normalized;
        }
        return axes;
    }

    public Projection GetProjection(Vector2 axis)
    {
        float min = Vector2.Dot(vertices[0], axis);
        float max = min;
        for(var i = 1; i < vertices.Length; i++)
        {
            var d = Vector2.Dot(vertices[i], axis);
            min = Mathf.Min(min, d);
            max = Mathf.Max(max, d);
        }
        return new Projection(min, max);
    }
}
