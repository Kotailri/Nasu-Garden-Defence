using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemPassiveEnum
{
    LowHealthExecute,
    ProjectileThroughExplosion,
    ProjectileMightExplode
}

public class ItemPassiveManager : MonoBehaviour
{
    public Dictionary<ItemPassiveEnum, bool> ItemPassiveDictionary;

    [Space(5f)]
    [Header("Passives")]
    public float LowHealthExecutePercent;
    [Space(5f)] public float ProjectileThruExplosionDmg;
    [Space(5f)] public float ProjectileExplosionChance; public float ProjectileExplosionDamage;

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
