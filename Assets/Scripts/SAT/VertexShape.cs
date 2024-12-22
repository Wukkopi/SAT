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

        // Assume clockwise polygon winding
        for (var i = 0; i < axes.Length; i++)
        {
            var v1 = i > 0 ? vertices[i - 1] : vertices[^1];
            var v2 = i > 0 ? vertices[i] : vertices[0];
            var v = (v2 - v1).normalized;
            //var v = i > 0 ? Vertices[i - 1] - Vertices[i] : Vertices[^1] - Vertices[0];   
            axes[i] = new Vector2(-v.y, v.x);

            var f = (v1+v2) / 2;

            Debug.DrawLine(Position + f, Position + f + axes[i] * 0.2f, Color.cyan);
        }
        return axes;
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
            }
            else
            {
                return false;
            }
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
            var v1 = i > 0 ? Vertices[i - 1] : Vertices[0];
            var v2 = i > 0 ? Vertices[i] : Vertices[^1];
            Debug.DrawLine(Position + v1, Position + v2);
        }
    }
}
