using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleShedAdder : ItemAdderNormal
{
    [Header("Purple Shed")]
    public GameObject PurpleShedPrefab;

    [Space(5f)]
    public float ShedTimer;
    public float FurDuration;
    public float ShedSlowAmount;
    public float ShedDamageTimer;
    public int ShedDamage;

    public override void OnItemGet()
    {
        base.OnItemGet();
        GlobalItemToggles.HasPurpleShed = true;
        Global.playerTransform.gameObject.AddComponent<PurpleFurSpawner>();
        Global.playerTransform.gameObject.GetComponent<PurpleFurSpawner>().Initialize(PurpleShedPrefab, ShedTimer, FurDuration, ShedSlowAmount, ShedDamageTimer, ShedDamage);
    }
}
