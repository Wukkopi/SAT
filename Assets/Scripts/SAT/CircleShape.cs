using System;
using UnityEngine;

public class CircleShape : Shape
{
    public float radius;

    public override Projection GetProjection(Vector2 axis)
    {
        var d = Vector2.Dot(axis, Position);
        return new Projection(d - radius, d + radius);
    }

    public override Vector2 GetCenter() => Position;
    public override bool ComputeOverlap(IShape other, ref float distance, ref Vector2 direction)
    {
        if (other is CircleShape cShape)
        {
            var axis = (Position - cShape.Position).normalized;
            var p1 = GetProjection(axis);
            var p2 = other.GetProjection(axis);
            var overlaps = p1.Overlaps(p2, out distance);
            if (overlaps)
            {
                direction = axis;
            }
            return overlaps;
        }

        if (other is VertexShape vShape)
        {
            var axis = GetAxisForClosestVertex(vShape.Vertices, vShape.Position).normalized;

            var p1 = GetProjection(axis);
            var p2 = other.GetProjection(axis);

            if (p1.Overlaps(p2, out var d))
            {
                if (d < distance)
                {
                    distance = d;
                    direction = -axis;
                }
                return true;
            }
            return false;
        }
        throw new NotImplementedException("Unrecognized shape received");
    }
    private Vector2 GetAxisForClosestVertex(Vector2[] vertices, Vector2 verticeOffset)
    {
        var result = Vector2.zero;
        var closest = float.MaxValue;
        foreach (var v in vertices)
        {
            var axis = v + verticeOffset - Position;
            var distance = axis.sqrMagnitude;
            if (distance < closest)
            {
                closest = distance;
                result = axis;
            }
        }
        return result;
    }

    public void Update()
    {
        for(var i = 0 ; i < 16; i++)
        {
            var r = 360/16*Mathf.Deg2Rad;
            var v1 = new Vector2(Mathf.Cos(r * i), Mathf.Sin(r * i)) * radius;
            var v2 = new Vector2(Mathf.Cos(r * (i + 1)), Mathf.Sin(r * (i + 1))) * radius;

            Debug.DrawLine( Position + v1, Position + v2);
        }
    }
}