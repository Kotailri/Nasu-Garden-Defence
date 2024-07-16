using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnetDistanceBuff : GardenBuff
{
    [Header("Magnet Distances")]
    public List<float> DistancesAtEachLevel = new();

    public override void LevelUp()
    {
        GlobalGarden.CoinMagnetDistance = DistancesAtEachLevel[CurrentLevel - 1];
        GlobalGarden.CoinMagnetDistanceLevel = CurrentLevel;

    }

    public override void Refund()
    {
        base.Refund();
        GlobalGarden.CoinMagnetDistanceLevel = 0;
        GlobalGarden.CoinDropChance = DistancesAtEachLevel[0];
    }

    public override void UpdateLevel()
    {
        if (CurrentLevel < MaxLevel)
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + DistancesAtEachLevel[CurrentLevel] + " > " + DistancesAtEachLevel[CurrentLevel + 1] + "]";
        }
        else
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + DistancesAtEachLevel[CurrentLevel] + "]";
        }


        CheckLevel();
    }

    public override void SetStartingLevel()
    {
        SetLevel(GlobalGarden.CoinMagnetDistanceLevel);
        GlobalGarden.CoinMagnetDistance = DistancesAtEachLevel[CurrentLevel];
    }
}
