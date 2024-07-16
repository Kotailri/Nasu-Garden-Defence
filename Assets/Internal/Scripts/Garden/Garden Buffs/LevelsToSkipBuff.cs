using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsToSkipBuff : GardenBuff
{
    [Header("Levels to Skip")]
    public List<int> NumberOfLevelsSkipped = new();

    public override void LevelUp()
    {
        GlobalGarden.LevelsToSkip = NumberOfLevelsSkipped[CurrentLevel];
        GlobalGarden.LevelsToSkipLevel = CurrentLevel;

    }

    public override void UpdateLevel()
    {
        if (CurrentLevel < MaxLevel)
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + NumberOfLevelsSkipped[CurrentLevel] + " > " + NumberOfLevelsSkipped[CurrentLevel + 1] + "]";
        }
        else
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + NumberOfLevelsSkipped[CurrentLevel] + "]";
        }


        CheckLevel();
    }

    public override void SetStartingLevel()
    {
        SetLevel(GlobalGarden.LevelsToSkipLevel);
        GlobalGarden.LevelsToSkip = NumberOfLevelsSkipped[CurrentLevel];
    }
}
