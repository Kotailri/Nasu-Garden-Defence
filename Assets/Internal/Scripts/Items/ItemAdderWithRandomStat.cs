using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdderWithRandomStat : ItemAdder
{
    public List<int> statLevelUps = new();

    public override void OnItemGet()
    {
        foreach (int i in statLevelUps) 
        {
            Global.GetRandomDictionaryValue(GlobalPlayer.PlayerStatDict).SetLevel(i,true);
        }
    }
}
