using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDropChanceBuff : GardenBuff
{
    [Header("Coin Drop Chance")]
    public List<float> DropChanceAtEachLevel = new();

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

    public override void UpdateLevel()
    {
        if (CurrentLevel < MaxLevel)
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + DropChanceAtEachLevel[CurrentLevel] * 100 + "% > " + DropChanceAtEachLevel[CurrentLevel + 1] * 100 + "%]";
        }
        else
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + DropChanceAtEachLevel[CurrentLevel] * 100 + "%]";
        }

        
        CheckLevel();
    }

    public override void SetStartingLevel()
    {
        SetLevel(GlobalGarden.CoinDropChanceLevel);
        GlobalGarden.CoinDropChance = DropChanceAtEachLevel[CurrentLevel];
    }
}
