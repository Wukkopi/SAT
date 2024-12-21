using System;
using UnityEngine;

public readonly struct Projection
{
    public Projection(float min, float max)
    {
        Min = min;
        Max = max;
    }

    public bool Overlaps(Projection other, out float distance)
    {
        var overlaps = Max > other.Min && Min < other.Max;
        distance = 0f;
        if (overlaps)
        {
            distance = Mathf.Abs(Mathf.Min(Max - other.Min, other.Max - Min));
        }
        return overlaps;
    } 

    public readonly float Min;
    public readonly float Max;
}
