using UnityEngine;

public class VertexShape : Shape
{
    public Vector2[] Vertices;
    private Vector2[] normals = null;
    public override Vector2 GetCenter()
    {
        var result = Vector2.zero;
        foreach (var v in Vertices)
        {
            result += v;
        }
        return result / Vertices.Length + Position;
    }

    public Vector2[] GetEdgeNormals()
    {
        if (normals != null)
        {
            return normals;
        }

        normals = new Vector2[Vertices.Length];

        // Assume clockwise polygon winding
        for (var i = 0; i < normals.Length; i++)
        {
            var v1 = i > 0 ? Vertices[i - 1] : Vertices[^1];
            var v2 = i > 0 ? Vertices[i] : Vertices[0];

            var v = (v2 - v1).normalized;
            normals[i] = new Vector2(-v.y, v.x);
        }
        return normals;
    }

    public override bool ComputeOverlap(IShape other, ref float distance, ref Vector2 direction)
    {
        var checkAxis = other.GetCenter() - GetCenter();
        var normals = GetEdgeNormals();
        foreach (var n in normals)
        {
            // Filter out "away facing" normals
            if (Vector2.Dot(n, checkAxis) <= 0)
            {
                continue;
            }

            var p1 = GetProjection(n);
            var p2 = other.GetProjection(n);
            if (p1.Overlaps(p2, out var d))
            {
                if (d < distance)
                {
                    distance = d;
                    direction = -n;
                }
                continue;
            }
            return false;
        }
        return true;
    }

    public override Projection GetProjection(Vector2 axis)
    {
        float min = float.MaxValue;
        float max = float.MinValue;
        for(var i = 0; i < Vertices.Length; i++)
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
            var v1 = i > 0 ? Vertices[i - 1] : Vertices[^1];
            var v2 = i > 0 ? Vertices[i] : Vertices[0];
            Debug.DrawLine(Position + v1, Position + v2);
        }
    }
}
