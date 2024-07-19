using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRerollBuff : GardenBuff
{
    [Header("Item Rerolls")]
    public List<int> RerollsAtEachLevel = new();

    public override void LevelUp()
    {
        GlobalGarden.ItemRerolls = RerollsAtEachLevel[CurrentLevel - 1];
        GlobalGarden.PlayerRegenerationLevel = CurrentLevel;

    }

    public override void Refund()
    {
        base.Refund();
        GlobalGarden.PlayerRegenerationLevel = 0;
        GlobalGarden.ItemRerolls = RerollsAtEachLevel[0];
    }

    public override void UpdateLevel()
    {
        if (CurrentLevel < MaxLevel)
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + RerollsAtEachLevel[CurrentLevel] + " > " + RerollsAtEachLevel[CurrentLevel + 1] + "rerolls]";
        }
        else
        {
            levelText.text = CurrentLevel.ToString() + "/" + MaxLevel.ToString()
            + " [" + RerollsAtEachLevel[CurrentLevel] + "rerolls]";
        }


        CheckLevel();
    }

    public override void SetStartingLevel()
    {
        SetLevel(GlobalGarden.ItemRerollLevel);
        GlobalGarden.ItemRerolls = RerollsAtEachLevel[CurrentLevel];
    }
}
