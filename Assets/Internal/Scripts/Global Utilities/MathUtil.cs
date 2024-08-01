using System;
using UnityEngine;

public static class MathUtil
{
    /// <summary>
    /// Determines if a number falls between a range [min, max]
    /// </summary>
    /// <param name="toCheck"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static bool IsBetweenFloatsInclusive(float toCheck, float min, float max)
    {
        return (toCheck >= min) && (toCheck <= max);
    }

    /// <summary>
    /// Generate a random quaternion rotation around the X, Y, and Z axes
    /// </summary>
    /// <param name="randomX"></param>
    /// <param name="randomY"></param>
    /// <param name="randomZ"></param>
    /// <returns></returns>
    public static Quaternion GetRandomRotation(bool randomX, bool randomY, bool randomZ)
    {
        return Quaternion.Euler(randomX ? UnityEngine.Random.Range(0f, 360f) : 0, randomY ? UnityEngine.Random.Range(0f, 360f) : 0, randomZ ? UnityEngine.Random.Range(0f, 360f) : 0);
    }

    /// <summary>
    /// Clamps a float to a max value
    /// </summary>
    /// <param name="val"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static float ClampMaxf(float val, float max)
    {
        if (val > max)
            return max;

        return val;
    }

    /// <summary>
    /// Clamps a float to a min value
    /// </summary>
    /// <param name="val"></param>
    /// <param name="min"></param>
    /// <returns></returns>
    public static float ClampMinf(float val, float min)
    {
        if (val < min)
            return min;

        return val;
    }

    /// <summary>
    /// Clamps an int to a max value
    /// </summary>
    /// <param name="val"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int ClampMaxInt(int val, int max)
    {
        if (val > max)
            return max;

        return val;
    }

    /// <summary>
    /// Clamps an int to a min value
    /// </summary>
    /// <param name="val"></param>
    /// <param name="min"></param>
    /// <returns></returns>
    public static int ClampMinInt(int val, int min)
    {
        if (val < min)
            return min;

        return val;
    }

    /// <summary>
    /// Wraps a float value to ensure it falls within a specified range [min, max).
    /// </summary>
    public static float Wrap(float value, float min, float max)
    {
        float range = max - min;
        return min + ((value - min) % range + range) % range;
    }

    /// <summary>
    /// Wraps a float value to ensure it falls between [0, 360).
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float WrapAngleDeg(float angle)
    {
        angle = angle % 360;
        if (angle < 0) angle += 360;
        return angle;
    }

    /// <summary>
    /// Determines if two floating-point values are approximately equal within a specified tolerance.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="tolerance"></param>
    /// <returns></returns>
    public static bool ApproximatelyEqual(float a, float b, float tolerance = 0.0001f)
    {
        return Mathf.Abs(a - b) < tolerance;
    }

    /// <summary>
    /// Gets the sign of a float.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int Sign(float value)
    {
        if (value < 0) return -1;
        return 1;
    }
}
