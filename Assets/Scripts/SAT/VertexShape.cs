using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class VertexShape : Shape
{
    [SerializeField]
    private Vector2[] vertices;

    public Vector2[] Vertices => vertices;

    public override Vector2 GetCenter()
    {
        var result = Vector2.zero;
        foreach (var v in vertices)
        {
            result += v;   
        }
        return result / vertices.Length + Position;
    }

    public Vector2[] GetEdgeNormals()
    {
        Vector2[] axes = new Vector2[Vertices.Length];

        for (var i = 0; i < axes.Length; i++)
        {
            axes[i] = i > 0 ? Vertices[i] - Vertices[i - 1] : Vertices[^1] - Vertices[0];   
            axes[i] = new Vector2(-axes[i].y, axes[i].x).normalized;
        }
        return axes;
    }

    public override bool ComputeOverlap(IShape other, ref float distance, ref Vector2 direction)
    {
        var normals = GetEdgeNormals();
        foreach (var n in normals)
        {
            var p1 = GetProjection(n);
            var p2 = other.GetProjection(n);
            if (p1.Overlaps(p2, out var d))
            {
                if (d < distance)
                {
                    distance = d;
                    direction = n;
                }
            }
            else
            {
                distance = 0f;
                direction = Vector2.zero;
                return false;
            }
        }
        return true;
    }

    public override Projection GetProjection(Vector2 axis)
    {
        float min = Vector2.Dot(Vertices[0] + Position, axis);
        float max = min;
        for(var i = 1; i < Vertices.Length; i++)
        {
            var d = Vector2.Dot(Vertices[i] + Position, axis);
            min = Mathf.Min(min, d);
            max = Mathf.Max(max, d);
        }
        return new Projection(min, max);
    }

    public void Update()
    {
        for (var i = 0; i < Vertices.Length; i++)
        {
            var v1 = i > 0 ? Vertices[i - 1] : Vertices[0];
            var v2 = i > 0 ? Vertices[i] : Vertices[^1];
            Debug.DrawLine(Position + v1, Position + v2);
        }
    }
}
