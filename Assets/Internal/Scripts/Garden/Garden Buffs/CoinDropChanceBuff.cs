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
        GlobalGarden.CoinDropChanceLevel = CurrentLevel;

    }

    public override void Refund()
    {
        base.Refund();
        GlobalGarden.CoinDropChanceLevel = 0;
        GlobalGarden.CoinDropChance = DropChanceAtEachLevel[0];
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
