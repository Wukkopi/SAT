
using System.Collections.Generic;
using UnityEngine;

public class SATSystem : MonoBehaviour
{
    public List<Shape>  objects;

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < objects.Count; i++)
        {
            for (var j = i + 1; j < objects.Count; j++)
            {
                if (SAT.Overlap(objects[i], objects[j], out var distance, out var direction))
                {
                    Debug.DrawLine(objects[i].Position, objects[i].Position + direction * distance, Color.red);
                }
            }
        }
    }
}
