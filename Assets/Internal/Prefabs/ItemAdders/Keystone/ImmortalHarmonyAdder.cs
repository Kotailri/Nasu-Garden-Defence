using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalHarmonyAdder : ItemAdderNormal
{
    [Header("Immortal Harmony")]
    public float ShieldDuration;
    public float ExplosionDamage;
    public int HealPerHit;

    public override void OnItemGet()
    {
        base.OnItemGet();
        GlobalItemToggles.HasImmortalHarmony = true;
    }
}
