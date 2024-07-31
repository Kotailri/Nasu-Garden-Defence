using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemStatAdder
{
    public PlayerStatEnum stat;
    public int levelup;
}

public abstract class ItemAdder : MonoBehaviour
{
    public ItemScriptable ItemScriptableInfo;
    [Tooltip("Minimum wave index for adder to appear.")]
    public int MinWaveIndex;

    [Header("Set Stats")]
    public List<ItemStatAdder> statList = new();

    [Header("Random Stat")]
    [Tooltip("Stat1 for first stat, Stat2 for second stat, etc")]
    public List<int> statLevelUps = new();

    [Space(10f)]
    public bool isExcemptFromPoolRemoval;
    public bool addsToUI = true;

    private List<KeyValuePair<string, string>> replacements = new();
    private List<PlayerStat> statsLeveled = new();

    public abstract void OnItemGet();
    public virtual bool IsExcemptFromPoolRemoval()
    {
        return isExcemptFromPoolRemoval;
    }

    public ItemScriptable GetInfo()
    {
        if (ItemScriptableInfo == null)
        {
            print(name + " adder does not have a itemscriptable info!");
        }
        return ItemScriptableInfo;
    }

    public void ApplySetStats()
    {
        foreach (ItemStatAdder _item in statList)
        {
            GlobalStats.GetStat(_item.stat).SetLevel(_item.levelup, true);
        }
    }

    public void ApplyRandomStats()
    {
        statsLeveled.Clear();
        replacements.Clear();

        foreach (int i in statLevelUps)
        {
            PlayerStat levelStat = GameUtil.GetRandomDictionaryValue(GlobalStats.GetVisiblePlayerStatDict());
            levelStat.SetLevel(i, true);
            statsLeveled.Add(levelStat);
        }

        int statCount = 1;
        foreach (int i in statLevelUps)
        {
            string key = "Stat" + statCount.ToString();
            replacements.Add(new KeyValuePair<string, string>(key, statsLeveled[statCount - 1].GetStatName()));
            statCount++;
        }
    }

    public void AddItemToUI()
    {
        ApplySetStats();
        ApplyRandomStats();
        EventManager.TriggerEvent(EventStrings.STATS_UPDATED, null);
        Global.playerTransform.gameObject.GetComponent<PlayerAttackManager>().RefreshAttackList();
        if (addsToUI)
        {
            Global.itemUI.AddItemToUI(ItemScriptableInfo, replacements);
        }
        
    }
}
