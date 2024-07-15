using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropChanceBuff : GardenBuff
{
    [Header("Coin Drop Chance")]
    public float DefaultDropChance = 0.1f;
    public List<float> DropChanceAtEachLevel = new();

    private void Awake()
    {
        GlobalGarden.CoinDropChance = DefaultDropChance;
    }

    public override void LevelUp()
    {
        GlobalGarden.CoinDropChance = DropChanceAtEachLevel[CurrentLevel-1];

        if (GlobalGarden.CoinDropChanceLevel == MaxLevel)
        {
            GlobalGarden.CoinDropChanceLevel = CurrentLevel-1;
        }
        else
        {
            GlobalGarden.CoinDropChanceLevel = CurrentLevel;
        }
        
    }

    public override void SetStartingLevel()
    {
        SetLevel(GlobalGarden.CoinDropChanceLevel);
    }
}
