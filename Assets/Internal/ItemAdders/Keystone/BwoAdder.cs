using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BwoAdder : ItemAdderNormal
{
    [Header("Bwo")]
    public GameObject BwoPrefab;
    public float BwoMovespeed;

    public override void OnItemGet()
    {
        Global.keystoneItemManager.BwoMovespeed = BwoMovespeed;
        GlobalItemToggles.HasBwo = true;
        GameObject bwo = Instantiate(BwoPrefab, Global.playerTransform.position, Quaternion.identity);

        // refresh attack list
        foreach (ItemAdder adder in Global.itemInventoryManager.ItemInventory)
        {
            if (adder is ItemAdderWithPrefab prefabAdder)
            {
                if (prefabAdder.isAttachedToPet)
                {
                    prefabAdder.AttachToPet();
                }
                
            }
        }

        base.OnItemGet();
    }
}
