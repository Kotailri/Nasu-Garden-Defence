using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPassiveAdder : ItemAdder
{
    [Header("Passive")]
    public ItemPassiveEnum Passive;

    public override bool IsExcemptFromPoolRemoval()
    {
        return false;
    }

    public override void OnItemGet()
    {
        Global.itemPassiveManager.ActivatePassive(Passive);
        AddItemToUI();
    }
}
