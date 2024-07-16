using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegenerationBuff : GardenBuff
{
    [Header("Player Regen")]
    public List<float> RegenAtEachLevel = new();

    public override void LevelUp()
    {
        GlobalGarden.PlayerRegeneration = RegenAtEachLevel[CurrentLevel - 1];
        GlobalGarden.PlayerRegenerationLevel = CurrentLevel;

    }

    public override void Refund()
    {
        base.Refund();
        GlobalGarden.PlayerRegenerationLevel = 0;
        GlobalGarden.PlayerRegeneration = RegenAtEachLevel[0];
    }

    public override void UpdateLevel()
    {
        if (CurrentLevel < MaxLevel)
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + RegenAtEachLevel[CurrentLevel] + " > " + RegenAtEachLevel[CurrentLevel + 1] + "hp/sec]";
        }
        else
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + RegenAtEachLevel[CurrentLevel] + "hp/sec]";
        }


        CheckLevel();
    }

    public override void SetStartingLevel()
    {
        SetLevel(GlobalGarden.PlayerRegenerationLevel);
        GlobalGarden.PlayerRegeneration = RegenAtEachLevel[CurrentLevel];
    }
}
