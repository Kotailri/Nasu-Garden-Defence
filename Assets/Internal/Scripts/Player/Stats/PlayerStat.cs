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

    public Dictionary<int, float> statMultipliers = new();
    private int uniqueMutliplierID = 0;
    private float currentStatBoost = 1f;

    public float GetStatMultiplier()
    {
        return currentStatBoost;
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
                uniqueMutliplierID = 0;
        }
    }

    public void RecalculateStatMultiplier()
    {
        float newCurrentMultiplier = 1f;
        foreach (float s in statMultipliers.Values)
        {
            if (s > newCurrentMultiplier)
                newCurrentMultiplier = s;
        }
        currentStatBoost = newCurrentMultiplier;
    }

    public void ResetMultiplier()
    {
        statMultipliers.Clear();
        currentStatBoost = 1;
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
        return (GetStatBase() + (GetStatIncrease() * GetLevel())) * GetStatMultiplier();
    }
}

public class PlayerStatCompound : PlayerStat
{
    public PlayerStatCompound(string statName, PlayerStatEnum statEnum, string statDescription, float statBase, float statIncrease, bool showsInUI) : base(statName, statEnum, statDescription, statBase, statIncrease, showsInUI)
    {
    }

    public override float GetStat()
    {
        return (GetStatBase() * Mathf.Pow(1 + GetStatIncrease(), GetLevel())) * GetStatMultiplier();
    }
}

public class PlayerStatExponential : PlayerStat
{
    public PlayerStatExponential(string statName, PlayerStatEnum statEnum, string statDescription, float statBase, float statIncrease, bool showsInUI) : base(statName, statEnum, statDescription, statBase, statIncrease, showsInUI)
    {
    }

    public override float GetStat()
    {
        return (Mathf.Pow(GetStatBase(), GetStatIncrease() * GetLevel())) * GetStatMultiplier();
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

