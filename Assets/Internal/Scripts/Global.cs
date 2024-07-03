using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Global
{
    public static bool gameplayStarted = false;

    public static Transform playerTransform;
    public static Transform cursorTransform;

    // Mangagers
    public static TextSpawner damageTextSpawner;

    public static PlayerControls playerControls;
    public static StatManager statManager;

    public static GameOverManager gameOverManager;
    public static WaveManager waveManager;

    public static KeystoneItemManager keystoneItemManager;
    public static ItemUI itemUI;
    public static ItemSelectManager itemSelectManager;
    public static ItemInventoryManager itemInventoryManager;

    

    public static float MaxX = 18.47f;

    public static (float min, float max) XRange = (-14.13f, 18.31f);
    public static (float min, float max) YRange = (-7f, 6.95f);

    public static void GameOver()
    {
        if (gameOverManager == null) return;
        gameOverManager.DoGameOver();
    }

    public static List<GameObject> GetActiveEnemies()
    {
        List<GameObject> activeEnemies = new();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (MathHelper.IsBetweenFloatsInclusive(enemy.transform.position.x, XRange.min, XRange.max)
             && MathHelper.IsBetweenFloatsInclusive(enemy.transform.position.y, YRange.min, YRange.max))
            {
                activeEnemies.Add(enemy);
            }
        }
        return activeEnemies;
    }

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

    public static TValue GetRandomDictionaryValue<TKey, TValue>(Dictionary<TKey, TValue> dictionary)
    {
        if (dictionary.Count == 0)
        {
            throw new System.InvalidOperationException("Cannot get a random value from an empty dictionary.");
        }

        List<TValue> values = new List<TValue>(dictionary.Values);
        int randomIndex = UnityEngine.Random.Range(0, values.Count);
        return values[randomIndex];
    }

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
        count = Math.Min(count, copyList.Count);

        return copyList.GetRange(0, count);
    }
}

public static class MathHelper
{
    public static bool IsBetweenFloatsInclusive(float toCheck, float min, float max)
    {
        return (toCheck >= min) && (toCheck <= max);
    }
}

public static class Config
{
    public static double ControllerDeadZone = 0.5f;
    public static float CursorSpeed = 12f;
}

public enum PlayerStatEnum
{
    //attackAmount         = 0,
    attackspeed          = 1,
    dodge                = 2,
    //explosionDamage      = 3,
    //explosionRadius      = 4,
    gardenHealth         = 5,
    gardenRegen          = 6,
    gardenResist         = 7,
    invincDuration       = 8,
    meleeDamage          = 9,
    meleeAttackSize      = 18,
    movespeed = 10,
    damage               = 11,
    playerHealth         = 12,
    playerRegen          = 13,
    playerResist         = 14,
    projectileDamage     = 15,
    projectileSpeed      = 16,    
    projectileSize       = 19,
    slowReduction = 17,
}

public static class GlobalPlayer
{
    public static float ContactSlowAmount = 0.5f;
    public static float ContactSlowTime = 1f;

    public static Dictionary<PlayerStatEnum, PlayerStat> PlayerStatDict = new();
    public static float GetStatValue(PlayerStatEnum stat)
    {
        if (PlayerStatDict.ContainsKey(stat))
        {
            return PlayerStatDict[stat].GetStat();
        }
        else
        {
            Debug.LogWarning("Stat val for key " + stat + " not found");
            return 0;
        }
    }
    public static PlayerStat GetStat(PlayerStatEnum stat)
    {
        if (PlayerStatDict.ContainsKey(stat))
        {
            return PlayerStatDict[stat];
        }
        else
        {
            Debug.LogWarning("Stat val for key " + stat + " not found");
            return null;
        }
    }

    public static float CurrentPlayerDamageMultiplier = 1f;
}

public static class Player
{
    public static GameObject playerGameObject;

    public static void TogglePlayerMoveInput(bool IsOn)
    {
        playerGameObject.GetComponent<PlayerMovement>().ToggleMoveInput(IsOn);
    }
}
