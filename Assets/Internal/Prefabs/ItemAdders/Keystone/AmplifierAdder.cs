using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplifierAdder : ItemAdderNormal
{
    [Header("Amplification")]
    [Range(0.01f, 1f)]
    public float DistanceAmplificationAmount = 1;

    public override void OnItemGet()
    {
        Global.keystoneItemManager.DistanceAmplificationAmount = DistanceAmplificationAmount;
        GlobalItemToggles.HasAmplifier = true;
        base.OnItemGet();
    }
}
