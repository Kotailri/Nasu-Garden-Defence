using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdderWithRandomStat : ItemAdder
{
    [Tooltip("Stat1 for first stat, Stat2 for second stat, etc")]
    public List<int> statLevelUps = new();
    public bool isExcemptFromPoolRemoval;
    private List<PlayerStat> statsLeveled = new();

    public override void OnItemGet()
    {
        statsLeveled.Clear();

        foreach (int i in statLevelUps) 
        {
            PlayerStat levelStat = Global.GetRandomDictionaryValue(GlobalPlayer.PlayerStatDict);
            levelStat.SetLevel(i, true);
            statsLeveled.Add(levelStat);
        }

        List<KeyValuePair<string, string>> replacements = new();

        int statCount = 1;
        foreach (int i in statLevelUps)
        {
            string key = "Stat" + statCount.ToString();
            replacements.Add(new KeyValuePair<string, string>(key, statsLeveled[statCount - 1].GetStatName()));
            statCount++;
        }

        Global.itemUI.AddItemToUI(ItemScriptableInfo, replacements);
    }

    public override bool IsExcemptFromPoolRemoval()
    {
        return isExcemptFromPoolRemoval;
    }
}
