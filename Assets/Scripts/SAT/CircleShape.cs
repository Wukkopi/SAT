using UnityEngine;
public class CircleShape : Shape
{
    public float radius;

    public override Projection GetProjection(Vector2 axis)
    {
        var d = Vector2.Dot(axis,  new Vector2(transform.position.x, transform.position.y));
        return new Projection(d - radius, d + radius);
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