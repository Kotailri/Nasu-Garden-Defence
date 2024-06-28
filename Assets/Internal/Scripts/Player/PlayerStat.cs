using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStat
{
    private int    StatLevel = 0;
    private float  StatBase = 0;
    private float  StatIncrease = 0;
    private string StatName = "";

    public int GetLevel()
    {
        return StatLevel;
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
    }

    protected PlayerStat(string statName, float statBase, float statIncrease)
    {
        StatLevel = 0;
        StatIncrease = statIncrease;
        StatName = statName;
        StatBase = statBase;
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
    public PlayerStatLinear(string statName, float statBase, float statIncrease) : base(statName, statBase, statIncrease) { }

    public override float GetStat()
    {
        return GetStatBase() + (GetStatIncrease() * GetLevel());
    }
}

public class PlayerStatCompound : PlayerStat
{
    public PlayerStatCompound(string statName, float statBase, float statIncrease) : base(statName, statBase, statIncrease) { }

    public override float GetStat()
    {
        return GetStatBase() * Mathf.Pow(1 + GetStatIncrease(), GetLevel());
    }
}

public class PlayerStatExponential : PlayerStat
{
    public PlayerStatExponential(string statName, float statBase, float statIncrease) : base(statName, statBase, statIncrease) { }

    public override float GetStat()
    {
        return Mathf.Pow(GetStatBase(), GetStatIncrease() * GetLevel());
    }
}

public static class PlayerStatFactory
{
    public static PlayerStat CreatePlayerStat(string statName, float statBase, float statIncrease, PlayerStatGrowthType growthType)
    {
        switch (growthType)
        {
            case PlayerStatGrowthType.Compound:
                return new PlayerStatCompound(statName, statBase, statIncrease);
            case PlayerStatGrowthType.Exponential:
                return new PlayerStatExponential(statName, statBase, statIncrease);
            case PlayerStatGrowthType.Linear:
                return new PlayerStatLinear(statName, statBase, statIncrease);
        }
        return null;
    }
}

