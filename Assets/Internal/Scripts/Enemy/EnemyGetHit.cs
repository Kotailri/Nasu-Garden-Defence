using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHit : MonoBehaviour
{
    public void GetHit(int damage)
    {
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

        if (TryGetComponent(out DamageFlash flash)) {
            flash.DoDamageFlash();
        }
        GetComponent<EnemyHealth>().TakeDamage(newDamage);
    }
}
