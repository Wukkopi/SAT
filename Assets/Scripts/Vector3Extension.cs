using System.Numerics;

public static class Vector3Extension
{
    public static Vector2 ToVector2(ref this Vector3 v)
    {
        return new Vector2(v.X, v.Y);
    }

}