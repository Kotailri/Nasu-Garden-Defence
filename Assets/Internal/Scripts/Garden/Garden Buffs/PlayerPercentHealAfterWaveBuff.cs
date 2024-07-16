using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPercentHealAfterWaveBuff : GardenBuff
{
    [Header("Percent to Heal")]
    public List<float> PercentHealPerLevel = new();

    public override void LevelUp()
    {
        GlobalGarden.PlayerPercentHealAfterWave = PercentHealPerLevel[CurrentLevel - 1];
        GlobalGarden.PlayerPercentHealAfterWaveLevel = CurrentLevel;
    }

    public override void UpdateLevel()
    {
        if (CurrentLevel < MaxLevel)
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + PercentHealPerLevel[CurrentLevel] * 100 + "% > " + PercentHealPerLevel[CurrentLevel + 1] * 100 + "%]";
        }
        else
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + PercentHealPerLevel[CurrentLevel] * 100 + "%]";
        }


        CheckLevel();
    }

    public override void SetStartingLevel()
    {
        SetLevel(GlobalGarden.PlayerPercentHealAfterWaveLevel);
        GlobalGarden.PlayerPercentHealAfterWave = PercentHealPerLevel[CurrentLevel];
    }
}
