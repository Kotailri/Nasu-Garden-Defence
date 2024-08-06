using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtraUtil
{
    /// <summary>
    /// Creates a list from merging other lists.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lists"></param>
    /// <returns></returns>
    public static List<T> MergeLists<T>(params List<T>[] lists)
    {
        List<T> returnList = new();
        foreach (List<T> list in lists)
        {
            returnList.AddRange(list);
        }
        return returnList;
    }

    /// <summary>
    /// Gets a random value from a dictionary.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static TValue GetRandomDictionaryValue<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        if (dictionary.Count == 0)
        {
            throw new System.InvalidOperationException("Cannot get a random value from an empty dictionary.");
        }

        List<TValue> values = new List<TValue>(dictionary.Values);
        int randomIndex = Random.Range(0, values.Count);
        return values[randomIndex];
    }

    /// <summary>
    /// Gets n random elements from a list with no repeats.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static List<T> GetRandomElements<T>(List<T> list, int count)
    {
        if (list == null || list.Count == 0)
        {
            return new List<T>(); // Return an empty list if the input list is null or empty
        }

        List<T> copyList = new List<T>(list);
        System.Random rng = new System.Random();

        int n = copyList.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = copyList[k];
            copyList[k] = copyList[n];
            copyList[n] = value;
        }

        // Adjust count to be the minimum of the requested count and the list's length
        count = System.Math.Min(count, copyList.Count);

        return copyList.GetRange(0, count);
    }
}
