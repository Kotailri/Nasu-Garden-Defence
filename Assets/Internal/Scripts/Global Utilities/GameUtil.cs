using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameUtil
{
    /// <summary>
    /// Shakes the camera.
    /// </summary>
    /// <param name="shakeDuration"></param>
    /// <param name="shakeMagnitude"></param>
    /// <param name="dampingSpeed"></param>
    public static void ShakeCamera(float shakeDuration = 0.5f, float shakeMagnitude = 0.5f, float dampingSpeed = 1.0f)
    {
        Camera.main.GetComponent<CameraShake>().ShakeCamera(shakeDuration, shakeMagnitude, dampingSpeed);
    }

    /// <summary>
    /// Get all enemies active on the field.
    /// </summary>
    /// <returns></returns>
    public static List<GameObject> GetActiveEnemies()
    {
        List<GameObject> activeEnemies = new();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (MathUtil.IsBetweenFloatsInclusive(enemy.transform.position.x, Global.XRange.min, Global.XRange.max)
             && MathUtil.IsBetweenFloatsInclusive(enemy.transform.position.y, Global.YRange.min, Global.YRange.max))
            {
                activeEnemies.Add(enemy);
            }
        }
        return activeEnemies;
    }

    /// <summary>
    /// Returns the current animator's frame index.
    /// </summary>
    /// <param name="animator"></param>
    /// <returns></returns>
    public static int GetCurrentAnimationFrame(Animator animator)
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is null.");
            return -1;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        if (clipInfo.Length > 0)
        {
            AnimationClip currentClip = clipInfo[0].clip;
            float normalizedTime = stateInfo.normalizedTime % 1;
            int currentFrame = Mathf.FloorToInt(normalizedTime * currentClip.frameRate * currentClip.length);
            return currentFrame;
        }
        else
        {
            Debug.LogWarning("No animation clip found in the base layer.");
            return -1;
        }
    }

    /// <summary>
    /// Checks if an object is active on the field and within bounds.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="checkX"></param>
    /// <param name="checkY"></param>
    /// <returns></returns>
    public static bool IsObjectActiveOnField(GameObject obj, bool checkX, bool checkY)
    {
        if (checkX && checkY)
        {
            return (MathUtil.IsBetweenFloatsInclusive(obj.transform.position.x, Global.XRange.min, Global.XRange.max)
             && MathUtil.IsBetweenFloatsInclusive(obj.transform.position.y, Global.YRange.min, Global.YRange.max));
        }
        else if (checkX)
        {
            return MathUtil.IsBetweenFloatsInclusive(obj.transform.position.x, Global.XRange.min, Global.XRange.max);
        }
        else if (checkY)
        {
            return MathUtil.IsBetweenFloatsInclusive(obj.transform.position.y, Global.YRange.min, Global.YRange.max);
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// Returns a certain component reference found from a list of gameobjects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="gameObjects"></param>
    /// <returns></returns>
    public static T FindComponentInList<T>(this List<GameObject> gameObjects) where T : Component
    {
        foreach (GameObject obj in gameObjects)
        {
            T component = obj.GetComponent<T>();
            if (component != null)
            {
                return component;
            }
        }
        return null;
    }

    /// <summary>
    /// Returns the left edge position of an object's sprite.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static float GetLeftEdgeXPosition(GameObject obj)
    {
        return obj.transform.position.x - (GetSpriteHalfWidth(obj) / 2f);
    }

    /// <summary>
    /// Returns the right edge position of an object's sprite.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static float GetRightEdgeXPosition(GameObject obj)
    {
        return obj.transform.position.x + (GetSpriteHalfWidth(obj) / 2f);
    }

    /// <summary>
    /// Returns half the width of a gameobject's sprite.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static float GetSpriteHalfWidth(GameObject obj)
    {
        return obj.GetComponent<SpriteRenderer>().bounds.size.x * obj.transform.localScale.x;
    }

    /// <summary>
    /// Returns a quaternion with a random z rotation.
    /// </summary>
    /// <returns></returns>
    public static Quaternion GetRandom2DRotation()
    {
        return Quaternion.Euler(0, 0, Random.Range(0f, 360f));
    }

    /// <summary>
    /// Gets a reference to the nearest enemy to a position.
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public static Transform GetNearestEnemy(Vector2 point)
    {
        Transform closestTransform = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject g in GetActiveEnemies())
        {
            float currentDistance = Vector2.Distance(point, g.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closestTransform = g.transform;
            }
        }

        return closestTransform;
    }

    /// <summary>
    /// Returns a reference to the cloest enemy to the garden.
    /// </summary>
    /// <returns></returns>
    public static Transform GetNearestEnemyToGarden()
    {
        Transform closestTransform = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject g in GetActiveEnemies())
        {
            if (g.transform.position.x < closestDistance)
            {
                closestDistance = g.transform.position.x;
                closestTransform = g.transform;
            }
        }

        return closestTransform;
    }

    /// <summary>
    /// Returns a reference to the farthest enemy from the garden.
    /// </summary>
    /// <returns></returns>
    public static Transform GetFarthestEnemyToGarden()
    {
        Transform closestTransform = null;
        float closestDistance = Mathf.NegativeInfinity;
        foreach (GameObject g in GetActiveEnemies())
        {
            if (g.transform.position.x > closestDistance)
            {
                closestDistance = g.transform.position.x;
                closestTransform = g.transform;
            }
        }

        return closestTransform;
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

    /// <summary>
    /// Determines if a wave is currently active.
    /// </summary>
    /// <returns></returns>
    public static bool IsWaveOngoing()
    {
        return Managers.Instance.Resolve<IWaveMng>().IsWaveOngoing();
    }

    /// <summary>
    /// Resets game state.
    /// </summary>
    public static void ResetGame()
    {
        Global.RemainingRerolls = GlobalGarden.ItemRerolls;

        EventManager.TriggerEvent(EventStrings.GAME_RESET, null);
        Managers.Instance.Resolve<IGardenBuffMng>().SaveBuffs();

        // Global
        Global.EnemySpeedMultiplier = 1f;
        Global.WaveSpeed = 1f;
        Global.isGameOver = false;
        Global.gameplayStarted = false;

        // GlobalPlayer
        GlobalStats.ResetStats();

        // Global Keystone
        GlobalItemToggles.HasWinterCoat = false;
        GlobalItemToggles.HasApexStride = false;
        GlobalItemToggles.HasAmplifier = false;
        GlobalItemToggles.HasBwo = false;
        GlobalItemToggles.HasPurpleShed = false;
        GlobalItemToggles.HasImmortalHarmony = false;
    }
}
