using UnityEngine;
public class SAT : MonoBehaviour
{
    public static bool Overlap(IShape a, IShape b, out float distance, out Vector2 direction)
    {
        direction = Vector2.zero;
        distance = float.MaxValue;
        if (a is CircleShape shape1 && b is CircleShape shape2)
        {
            var axis = shape1.position - shape2.position;
            var p1 = a.GetProjection(axis);
            var p2 = b.GetProjection(axis);
            var overlaps = p1.Overlaps(p2, out distance);
            if (overlaps)
            {
                direction = axis;
            }
            return overlaps;
        }

        if (a is VertexShape shape)
        {
            var normals = shape.GetNormals();
            if (!GetOverlap(a, b, normals, ref distance, ref direction))
            {
                return false;
            }
        }

        if (b is VertexShape shape3)
        {
            var normals = shape3.GetNormals();
            if (!GetOverlap(a, b, normals, ref distance, ref direction))
            {
                return false;
            }
        }
        return true;
    }

    private static bool GetOverlap(IShape a, IShape b, Vector2[] normals, ref float distance, ref Vector2 direction)
    {
        foreach (var n in normals)
        {
            var p1 = a.GetProjection(n);
            var p2 = b.GetProjection(n);
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
}