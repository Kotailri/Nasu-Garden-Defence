using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStat
{
    private int    StatLevel = 0;
    private float  StatBase = 0;
    private float  StatIncrease = 0;
    private string StatName = "Stat Name";
    private string StatDescription = "Stat Description";
    private PlayerStatEnum StatEnum;
    private bool ShowsInUI = true;

    public Dictionary<int, float> statMultipliers = new() { { 0, 1f } };
    private int uniqueMutliplierID = 1;
    private float currentStatBoostMultiplicative = 1f;
    private float currentStatBoostAdditive = 0f;

    public float GetStatMultiplier()
    {
        return currentStatBoostMultiplicative;
    }

    public float GetStatAdditive()
    {
        return currentStatBoostAdditive;
    }

    public void AddStatAdditive(float boost)
    {
        currentStatBoostAdditive += boost;
    }

    public void RemoveStatAdditive(float boost)
    {
        currentStatBoostAdditive -= boost;
    }

    public void AddSharedStatMultiplier(float boost)
    {
        if (statMultipliers.ContainsKey(0))
        {
            statMultipliers[0] += boost;
        }
        else
        {
            statMultipliers[0] = boost;
        }
            
        RecalculateStatMultiplier();
    }

    public void RemoveSharedStatMultiplier(float boost)
    {
        if (statMultipliers.ContainsKey(0))
        {
            statMultipliers[0] -= boost;
            RecalculateStatMultiplier();
        }
    }

    public float GetUniqueStatMultiplier(int id)
    {
        if (statMultipliers.ContainsKey(id))
        {
            return statMultipliers[id];
        }

        return 1;
    }

    public void SetUniqueStatMultiplier(float boost, int id)
    {
        if (statMultipliers.ContainsKey(id))
        {
            statMultipliers[id] = boost;
            RecalculateStatMultiplier();
        }
    }

    public int AddStatMultiplier(float boost)
    {
        int returnId = uniqueMutliplierID;
        statMultipliers[uniqueMutliplierID] = boost;
        uniqueMutliplierID++;
        RecalculateStatMultiplier();
        return returnId;
    }

    public void RemoveStatMultiplier(int boostID)
    {
        if (statMultipliers.ContainsKey(boostID))
        {
            statMultipliers.Remove(boostID);
            RecalculateStatMultiplier();

            if (statMultipliers.Count == 0)
                uniqueMutliplierID = 1;
        }
    }

    public void RecalculateStatMultiplier()
    {
        float newCurrentMultiplier = 1f;

        foreach (var s in statMultipliers)
        {
            if (s.Key == 0)
            {
                continue;
            }

            else if (s.Value > newCurrentMultiplier)
            {
                newCurrentMultiplier = s.Value;
            }
        }

        currentStatBoostMultiplicative = newCurrentMultiplier * statMultipliers[0];
    }

    public void ResetMultiplier()
    {
        statMultipliers.Clear();
        statMultipliers.Add(0,1f);
        currentStatBoostMultiplicative = 1;
    }

    public int GetLevel()
    {
        return StatLevel;
    }

    public bool DoesShowInUI()
    {
        return ShowsInUI;
    }

    public PlayerStatEnum GetStatEnum()
    {
        return StatEnum;
    }

    public string GetStatDescription()
    {
        return StatDescription;
    }

    protected float GetStatBase()
    {
        return StatBase;
    }

    protected float GetStatIncrease()
    {
        return StatIncrease;
    }

    public string GetStatName()
    { 
        return StatName;
    }

    public void SetLevel(int increase, bool isRelative)
    {
        if (isRelative)
        {
            StatLevel += increase;
        }
        else
        {
            StatLevel = increase;
        }
        EventManager.TriggerEvent(EventStrings.STATS_UPDATED, null);
    }

    protected PlayerStat(string statName, PlayerStatEnum statEnum, string statDescription, float statBase, float statIncrease, bool showsInUI)
    {
        StatLevel = 0;
        StatIncrease = statIncrease;
        StatName = statName;
        StatBase = statBase;
        StatEnum = statEnum;
        StatDescription = statDescription;
        ShowsInUI = showsInUI;
    }

    public abstract float GetStat();
}

public enum PlayerStatGrowthType
{
    Linear,
    Compound,
    Exponential
}

public class PlayerStatLinear : PlayerStat
{
    public PlayerStatLinear(string statName, PlayerStatEnum statEnum, string statDescription, float statBase, float statIncrease, bool showsInUI) : base(statName, statEnum, statDescription, statBase, statIncrease, showsInUI)
    {
    }

    public override float GetStat()
    {
        return ((GetStatBase() + (GetStatIncrease() * GetLevel())) * GetStatMultiplier()) + GetStatAdditive();
    }
}

public class PlayerStatCompound : PlayerStat
{
    public PlayerStatCompound(string statName, PlayerStatEnum statEnum, string statDescription, float statBase, float statIncrease, bool showsInUI) : base(statName, statEnum, statDescription, statBase, statIncrease, showsInUI)
    {
    }

    public override float GetStat()
    {
        return ((GetStatBase() * Mathf.Pow(1 + GetStatIncrease(), GetLevel())) * GetStatMultiplier()) + GetStatAdditive();
    }
}

public class PlayerStatExponential : PlayerStat
{
    public PlayerStatExponential(string statName, PlayerStatEnum statEnum, string statDescription, float statBase, float statIncrease, bool showsInUI) : base(statName, statEnum, statDescription, statBase, statIncrease, showsInUI)
    {
    }

    public override float GetStat()
    {
        return ((Mathf.Pow(GetStatBase(), GetStatIncrease() * GetLevel())) * GetStatMultiplier()) + GetStatAdditive();
    }
}

public static class PlayerStatFactory
{
    public static PlayerStat CreatePlayerStat(string statName, PlayerStatEnum statEnum, string statDescription, float statBase, float statIncrease, PlayerStatGrowthType growthType, bool showsInUI)
    {
        switch (growthType)
        {
            case PlayerStatGrowthType.Compound:
                return new PlayerStatCompound(statName, statEnum, statDescription, statBase, statIncrease, showsInUI);
            case PlayerStatGrowthType.Exponential:
                return new PlayerStatExponential(statName, statEnum, statDescription, statBase, statIncrease, showsInUI);
            case PlayerStatGrowthType.Linear:
                return new PlayerStatLinear(statName, statEnum, statDescription, statBase, statIncrease, showsInUI);
        }
        return null;
    }
}

