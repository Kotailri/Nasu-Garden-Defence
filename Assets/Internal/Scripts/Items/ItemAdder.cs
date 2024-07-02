using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemAdder : MonoBehaviour
{
    public ItemScriptable ItemScriptableInfo;
    public abstract void OnItemGet();
    public ItemScriptable GetInfo()
    {
        return ItemScriptableInfo;
    }

    protected void AddItemToUI()
    {
        Global.itemUI.AddItemToUI(ItemScriptableInfo);
    }
}
