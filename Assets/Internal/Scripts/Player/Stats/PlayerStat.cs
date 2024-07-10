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

    public int GetLevel()
    {
        return StatLevel;
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

    protected PlayerStat(string statName, PlayerStatEnum statEnum, string statDescription, float statBase, float statIncrease)
    {
        StatLevel = 0;
        StatIncrease = statIncrease;
        StatName = statName;
        StatBase = statBase;
        StatEnum = statEnum;
        StatDescription = statDescription;
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
    public PlayerStatLinear(string statName, PlayerStatEnum statEnum, string statDescription, float statBase, float statIncrease) : base(statName, statEnum, statDescription, statBase, statIncrease)
    {
    }

    public override float GetStat()
    {
        return GetStatBase() + (GetStatIncrease() * GetLevel());
    }
}

public class PlayerStatCompound : PlayerStat
{
    public PlayerStatCompound(string statName, PlayerStatEnum statEnum, string statDescription, float statBase, float statIncrease) : base(statName, statEnum, statDescription, statBase, statIncrease)
    {
    }

    public override float GetStat()
    {
        return GetStatBase() * Mathf.Pow(1 + GetStatIncrease(), GetLevel());
    }
}

public class PlayerStatExponential : PlayerStat
{
    public PlayerStatExponential(string statName, PlayerStatEnum statEnum, string statDescription, float statBase, float statIncrease) : base(statName, statEnum, statDescription, statBase, statIncrease)
    {
    }

    public override float GetStat()
    {
        return Mathf.Pow(GetStatBase(), GetStatIncrease() * GetLevel());
    }
}

public static class PlayerStatFactory
{
    public static PlayerStat CreatePlayerStat(string statName, PlayerStatEnum statEnum, string statDescription, float statBase, float statIncrease, PlayerStatGrowthType growthType)
    {
        switch (growthType)
        {
            case PlayerStatGrowthType.Compound:
                return new PlayerStatCompound(statName, statEnum, statDescription, statBase, statIncrease);
            case PlayerStatGrowthType.Exponential:
                return new PlayerStatExponential(statName, statEnum, statDescription, statBase, statIncrease);
            case PlayerStatGrowthType.Linear:
                return new PlayerStatLinear(statName, statEnum, statDescription, statBase, statIncrease);
        }
        return null;
    }
}

