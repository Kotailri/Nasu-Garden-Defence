using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalHarmonyAdder : ItemAdderNormal
{
    [Header("Immortal Harmony")]
    public float ShieldDuration;
    public float ExplosionDamage;
    public int HealPerHit;

    [Space(5f)]
    public GameObject explosionPrefab;
    public GameObject shieldPrefab;

    public override void OnItemGet()
    {
        base.OnItemGet();
        GlobalItemToggles.HasImmortalHarmony = true;
        Global.keystoneItemManager.ImmortalHarmonyShieldTime = ShieldDuration;
        ImmortalHarmony m = Global.playerTransform.gameObject.AddComponent<ImmortalHarmony>();
        m.Initialize(ExplosionDamage, HealPerHit, explosionPrefab, shieldPrefab);
    }
}
