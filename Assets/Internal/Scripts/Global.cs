using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Global
{
    public static bool gameplayStarted = false;

    public static Transform playerTransform;
    public static Transform cursorTransform;
    public static Vector2 playerMoveVector = Vector2.zero;

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
    public static ItemPassiveManager itemPassiveManager;

    public static BossHealthBarManager bossHealthBarManager;
    public static GardenBuffManager gardenBuffManager;
    public static GardenHealth gardenHealth;
    public static PrefabManager prefabManager;
    public static AlertManager alertManager;

    public static bool IsInEditorMode = false;

    public static float EnemySpeedMultiplier = 1f;
    public static float EnemyHealthMultiplier = 1f;
    public static float WaveSpeed = 1f;

    public static float CoinValueMultiplier = 1f;

    public static float DamageFlashTimer = 0.25f;
    public static float DamageFlashAlpha = 0.25f;

    public static float MaxX = 16.5f;

    public static (float min, float max) XRange = (-14.67f, 17.31f);
    public static (float min, float max) YRange = (-7.5f, 7f);

    public static int RemainingRerolls = 0;

    public static void GameOver(DeathCondition deathCondition)
    {
        if (gameOverManager == null) return;
        isGameOver = true;
        
        gameOverManager.DoGameOver(deathCondition);
    }

    public static bool isGameOver = false;

    public static void ResetGame()
    {
        RemainingRerolls = GlobalGarden.ItemRerolls;

        EventManager.TriggerEvent(EventStrings.GAME_RESET, null);
        gardenBuffManager.saver.SaveBuffs();

        // Global
        EnemySpeedMultiplier = 1f;
        WaveSpeed = 1f;
        isGameOver = false;
        gameplayStarted = false;

        // GlobalPlayer
        GlobalPlayer.CurrentPlayerDamageMultiplier = 1f;
        GlobalPlayer.ResetStats();

        // Global Keystone
        GlobalItemToggles.HasWinterCoat = false;
        GlobalItemToggles.HasApexStride = false;
        GlobalItemToggles.HasAmplifier = false;
        GlobalItemToggles.HasBwo = false;
        GlobalItemToggles.HasPurpleShed = false;
        GlobalItemToggles.HasImmortalHarmony = false;
}

    public static void ShakeCamera(float shakeDuration=0.5f, float shakeMagnitude = 0.5f, float dampingSpeed=1.0f)
    {
        Camera.main.GetComponent<CameraShake>().ShakeCamera(shakeDuration, shakeMagnitude, dampingSpeed);
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

    public static bool IsObjectActiveOnField(GameObject obj, bool checkX, bool checkY)
    {
        if (checkX && checkY)
        {
            return (MathHelper.IsBetweenFloatsInclusive(obj.transform.position.x, XRange.min, XRange.max)
             && MathHelper.IsBetweenFloatsInclusive(obj.transform.position.y, YRange.min, YRange.max));
        }
        else if (checkX)
        {
            return MathHelper.IsBetweenFloatsInclusive(obj.transform.position.x, XRange.min, XRange.max);
        }
        else if (checkY)
        {
            return MathHelper.IsBetweenFloatsInclusive(obj.transform.position.y, YRange.min, YRange.max);
        }
        else
        {
            return false;
        }
    }

    public static Quaternion GetRandomRotation(bool randomX, bool randomY, bool randomZ)
    {
        // Generate a random rotation around the X, Y, and Z axes
        float x = Random.Range(0f, 360f);
        float y = Random.Range(0f, 360f);
        float z = Random.Range(0f, 360f);

        // Create and return the Quaternion rotation
        return Quaternion.Euler(randomX ? x : 0, randomY ? y : 0, randomZ ? z : 0);
    }


    public static float GetLeftEdgeXPosition(GameObject obj)
    {
        return obj.transform.position.x - (GetSpriteHalfWidth(obj) / 2f);
    }

    public static float GetRightEdgeXPosition(GameObject obj)
    {
        return obj.transform.position.x + (GetSpriteHalfWidth(obj) / 2f);
    }

    public static float GetSpriteHalfWidth(GameObject obj)
    {
        return obj.GetComponent<SpriteRenderer>().bounds.size.x * obj.transform.localScale.x;
    }

    public static Quaternion GetRandom2DRotation()
    {
        return Quaternion.Euler(0, 0, Random.Range(0f, 360f));
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

public static class GlobalAudio
{
    public static float MusicVolume = 0.5f;
    public static float SoundVolume = 0.5f;

}

public static class GlobalGarden
{
    public static int Coins = 100;

    public static float CoinDropChance = 0f;
    public static int CoinDropChanceLevel = 0;

    public static float CoinMagnetDistance = 0f;
    public static int CoinMagnetDistanceLevel = 0;

    public static float PlayerRegeneration = 0f;
    public static int PlayerRegenerationLevel = 0;

    public static int GardenHealAfterWave = 0;
    public static int GardenHealAfterWaveLevel = 0;

    public static int LevelsToSkip = 0;
    public static int LevelsToSkipLevel = 0;

    public static float PlayerPercentHealAfterWave = 0f;
    public static int PlayerPercentHealAfterWaveLevel = 0;

    public static int ItemRerolls = 0;
    public static int ItemRerollLevel = 0;

}
public static class MathHelper
{
    public static bool IsBetweenFloatsInclusive(float toCheck, float min, float max)
    {
        return (toCheck >= min) && (toCheck <= max);
    }

    public static Vector2 DivideVector2(Vector2 numerator, Vector2 denominator)
    {
        return new Vector2(numerator.x / denominator.x, numerator.y / denominator.y);
    }

    public static Vector3 DivideVector3(Vector3 numerator, Vector3 denominator)
    {
        return new Vector3(numerator.x / denominator.x, numerator.y / denominator.y, numerator.z / denominator.z);
    }
}

public static class Config
{
    public static double ControllerDeadZone = 0.5f;
    public static float CursorSpeed = 12f;
}

public enum DeathCondition
{
    PlayerDeath,
    GardenDeath
}

public enum PlayerStatEnum
{
    //attackAmount         = 0,
    attackspeed          = 1,
    //dodge                = 2,
    critchance = 21,
    explosionDamage      = 3,
    explosionRadius      = 4,
    gardenHealth         = 5,
    //gardenRegen          = 6,
    //gardenResist         = 7,
    invincDuration       = 8,
    meleeDamage          = 9,
    //meleeAttackSize      = 18,
    movespeed = 10,
    damage               = 11,
    playerHealth         = 12,
    //playerRegen          = 13,
    //playerResist         = 14,
    projectileDamage     = 15,
    projectileSpeed      = 16,    
    //projectileSize       = 19,
    slowReduction = 17,
    attackSize = 20
}

public static class GlobalPlayer
{
    public static float ContactSlowAmount = 0.5f;
    public static float ContactSlowTime = 1f;

    public static Dictionary<PlayerStatEnum, PlayerStat> PlayerStatDict = new();

    public static Dictionary<PlayerStatEnum, PlayerStat> GetVisiblePlayerStatDict()
    {
        Dictionary<PlayerStatEnum, PlayerStat> pickableStats = PlayerStatDict;
        foreach (var stat in PlayerStatDict)
        {
            if (!stat.Value.DoesShowInUI())
            {
                pickableStats.Remove(stat.Key);
            }
        }
        return pickableStats;
    }

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

    public static void ResetStats()
    {
        foreach (var stat in PlayerStatDict)
        {
            PlayerStatDict[stat.Key].SetLevel(0, false);
            PlayerStatDict[stat.Key].ResetMultiplier();
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
