using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemPassiveEnum
{
    LowHealthExecute
}

public class ItemPassiveManager : MonoBehaviour
{
    public Dictionary<ItemPassiveEnum, bool> ItemPassiveDictionary;

    [Space(5f)]
    [Header("Passives")]
    public float LowHealthExecutePercent;

    private void Awake()
    {
        Global.itemPassiveManager = this;
        ItemPassiveDictionary = new();
    }

    public void ActivatePassive(ItemPassiveEnum passive)
    {
        if (ItemPassiveDictionary.ContainsKey(passive))
        {
            ItemPassiveDictionary[passive] = true;
        }
        else
        {
            ItemPassiveDictionary.Add(passive, true);
        }
    }

    public bool GetPassive(ItemPassiveEnum passive)
    {
        if (ItemPassiveDictionary.ContainsKey(passive))
        {
            return ItemPassiveDictionary[passive];
        }
        else
        {
            ItemPassiveDictionary.Add(passive, false);
            return false;
        }
    }
}
