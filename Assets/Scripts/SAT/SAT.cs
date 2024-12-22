
using System.Collections.Generic;
using UnityEngine;

public class SAT : MonoBehaviour
{
    public List<Shape>  objects;

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < objects.Count; i++)
        {
            for (var j = i + 1; j < objects.Count; j++)
            {
                if (Overlap(objects[i], objects[j], out var distance, out var direction))
                {
                    var center = objects[i].GetCenter();
                    Debug.DrawLine(center, center + direction * distance, Color.red);
                }
            }
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
