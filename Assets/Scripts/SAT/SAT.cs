
using System.Collections.Generic;
using UnityEngine;

public class SAT : MonoBehaviour
{
    public List<Shape> objects;

    public bool ResolveCollisions;

    // Update is called once per frame
    void FixedUpdate()
    {
        for (var i = 0; i < objects.Count; i++)
        {
            for (var j = i + 1; j < objects.Count; j++)
            {
                if (Overlap(objects[i], objects[j], out var distance, out var direction))
                {
                    var center = objects[i].GetCenter();
                    Debug.DrawLine(center, center + direction * distance, Color.red);
                    if (ResolveCollisions)
                    {
                        ResolveCollision(objects[i], objects[j], direction, distance);
                    }
                }
            }
        }
    }
    
    private static void ResolveCollision(Shape a, Shape b, Vector2 direction, float distance)
    {
        if (a.IsStatic && b.IsStatic)
        {
            return;
        }

        var v3 = new Vector3(direction.x, direction.y) * distance;
        if (a.IsStatic)
        {
            b.transform.position += v3;
        }
        else if (b.IsStatic)
        {
            a.transform.position += v3;
        }
        else
        {
            a.transform.position -= v3 / 2;
            b.transform.position += v3 / 2;
        }

    }

    private static bool Overlap(IShape a, IShape b, out float distance, out Vector2 direction)
    {
        direction = Vector2.zero;
        distance = float.MaxValue;

        var overlapA = a.ComputeOverlap(b, ref distance, ref direction);
        var overlapB = b.ComputeOverlap(a, ref distance, ref direction);
        return overlapA && overlapB;
    }

}
