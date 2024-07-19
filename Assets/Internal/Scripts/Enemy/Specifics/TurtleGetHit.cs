using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleGetHit : EnemyGetHit
{
    public bool IsImmune = false;

    public override void GetHit(int damage, Vector2 location)
    {
        if (IsImmune)
        {
            // make particle spawner spawn deflection particle at location
            return;
        }

        int newDamage = damage;
        if (GlobalItemToggles.HasAmplifier)
        {
            newDamage = Mathf.RoundToInt(damage * Global.keystoneItemManager.DistanceAmplificationAmount
                * Vector2.Distance(Global.playerTransform.position, transform.position));
        }

        if (newDamage < damage)
        {
            newDamage = damage;
        }

        if (TryGetComponent(out DamageFlash flash))
        {
            flash.DoDamageFlash();
        }
        GetComponent<EnemyHealth>().TakeDamage(newDamage, location);
    }
}
