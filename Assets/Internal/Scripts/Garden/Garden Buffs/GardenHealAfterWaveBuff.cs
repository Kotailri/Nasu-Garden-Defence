using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenHealAfterWaveBuff : GardenBuff
{
    [Header("Garden Heal")]
    public List<int> HealAmountAtEachLevel = new();

    public override void LevelUp()
    {
        GlobalGarden.GardenHealAfterWave = HealAmountAtEachLevel[CurrentLevel - 1];
        GlobalGarden.GardenHealAfterWaveLevel = CurrentLevel;

    }

    public override void Refund()
    {
        base.Refund();
        GlobalGarden.GardenHealAfterWaveLevel = 0;
        GlobalGarden.GardenHealAfterWave = HealAmountAtEachLevel[0];
    }

    public override void UpdateLevel()
    {
        if (CurrentLevel < MaxLevel)
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + HealAmountAtEachLevel[CurrentLevel] + " > " + HealAmountAtEachLevel[CurrentLevel + 1] + "hp]";
        }
        else
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + HealAmountAtEachLevel[CurrentLevel] + "hp]";
        }


        CheckLevel();
    }

    public override void SetStartingLevel()
    {
        SetLevel(GlobalGarden.GardenHealAfterWaveLevel);
        GlobalGarden.GardenHealAfterWave = HealAmountAtEachLevel[CurrentLevel];
    }
}
