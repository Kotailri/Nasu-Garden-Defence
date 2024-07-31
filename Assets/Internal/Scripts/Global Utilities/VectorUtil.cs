using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorUtil
{
    /// <summary>
    /// Calculates the normalized direction vector from one point to another.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static Vector2 DirectionTo(Vector2 from, Vector2 to)
    {
        return (to - from).normalized;
    }

    /// <summary>
    /// Calculates the normalized direction vector from one point to the player position.
    /// </summary>
    /// <param name="from"></param>
    /// <returns></returns>
    public static Vector2 DirectionToPlayer(Vector2 from)
    {
        return DirectionTo(from, Global.playerTransform.position);
    }

    /// <summary>
    /// Calculates the signed angle in degrees between two vectors.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static float AngleBetween(Vector2 from, Vector2 to)
    {
        return Vector2.SignedAngle(from, to);
    }

    /// <summary>
    /// Gets a random vector2 within ranges.
    /// </summary>
    /// <param name="minX"></param>
    /// <param name="maxX"></param>
    /// <param name="minY"></param>
    /// <param name="maxY"></param>
    /// <returns></returns>
    public static Vector2 RandomVector2(float minX, float maxX, float minY, float maxY)
    {
        return new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    /// <summary>
    /// Clamps a vector's components to be within the specified minimum and maximum values.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static Vector2 ClampVector(Vector2 value, Vector2 min, Vector2 max)
    {
        return new Vector2(Mathf.Clamp(value.x, min.x, max.x), Mathf.Clamp(value.y, min.y, max.y));
    }

    /// <summary>
    /// Rotates a vector by a specified angle in degrees.
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static Vector2 RotateVector(Vector2 vector, float angle)
    {
        float radians = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(cos * vector.x - sin * vector.y, sin * vector.x + cos * vector.y);
    }

    /// <summary>
    /// Divides Vector3 elements by another vector's elements.
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <returns></returns>
    public static Vector2 DivideVector2(Vector2 numerator, Vector2 denominator)
    {
        return new Vector2(numerator.x / denominator.x, numerator.y / denominator.y);
    }

    /// <summary>
    /// Divides Vector2 elements by another vector's elements.
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <returns></returns>
    public static Vector3 DivideVector3(Vector3 numerator, Vector3 denominator)
    {
        return new Vector3(numerator.x / denominator.x, numerator.y / denominator.y, numerator.z / denominator.z);
    }

    /// <summary>
    /// Determines if a point is within a specified radius of another point.
    /// </summary>
    /// <param name="center">The center point to check against.</param>
    /// <param name="point">The point to check.</param>
    /// <param name="radius">The radius within which to check.</param>
    /// <returns>True if the point is within the radius of the center point; otherwise, false.</returns>
    public static bool IsPointWithinRadius(Vector2 center, Vector2 point, float radius)
    {
        return Vector2.Distance(center, point) <= radius;
    }

}
