using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStatEnum
{
    //attackAmount         = 0,
    attackspeed = 1,
    //dodge                = 2,
    critchance = 21,
    explosionDamage = 3,
    explosionRadius = 4,
    gardenHealth = 5,
    //gardenRegen          = 6,
    //gardenResist         = 7,
    invincDuration = 8,
    meleeDamage = 9,
    //meleeAttackSize      = 18,
    movespeed = 10,
    damage = 11,
    playerHealth = 12,
    //playerRegen          = 13,
    //playerResist         = 14,
    projectileDamage = 15,
    projectileSpeed = 16,
    //projectileSize       = 19,
    slowReduction = 17,
    attackSize = 20
}

public static class GlobalStats
{
    public static Dictionary<PlayerStatEnum, PlayerStat> PlayerStatDict = new();

    public static Dictionary<PlayerStatEnum, PlayerStat> GetVisiblePlayerStatDict()
    {
        Dictionary<PlayerStatEnum, PlayerStat> pickableStats = new();
        foreach (var stat in PlayerStatDict)
        {
            if (stat.Value.DoesShowInUI())
            {
                pickableStats.Add(stat.Key, stat.Value);
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
}
