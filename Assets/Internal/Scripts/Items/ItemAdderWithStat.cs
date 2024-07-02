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

public class ItemAdderWithStat : ItemAdder
{
    public List<ItemStatAdder> statList = new();

    public override void OnItemGet()
    {
        AddItemToUI();
        foreach (var item in statList)
        {
            GlobalPlayer.GetStat(item.stat).SetLevel(item.levelup, true);
        }
    }
}
