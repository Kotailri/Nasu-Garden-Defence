using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemAdder : MonoBehaviour
{
    public ItemScriptable ItemScriptableInfo;
    public abstract void OnItemGet();
    public abstract bool IsExcemptFromPoolRemoval();
    public ItemScriptable GetInfo()
    {
        return ItemScriptableInfo;
    }

    public void AddItemToUI()
    {
        Global.itemUI.AddItemToUI(ItemScriptableInfo);
    }
}
