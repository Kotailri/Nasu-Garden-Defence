using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterCoatAdder : ItemAdderNormal
{
    [Header("Winter Coat")]
    public GameObject WinterCoatPrefab;
    public float SlowAmount;
    public float SlowTime;

    public override void OnItemGet()
    {
        GameObject winterCoat = Instantiate(WinterCoatPrefab, Vector3.zero, Quaternion.identity);
        winterCoat.transform.SetParent(Global.playerTransform, false);
        GlobalItemToggles.HasWinterCoat = true;
        winterCoat.GetComponent<WinterCoat>().ActivateSlow(SlowAmount, SlowTime);
        base.OnItemGet();
    }
}
