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
        Instantiate(BwoPrefab, Global.playerTransform.position, Quaternion.identity);
        base.OnItemGet();
    }
}
