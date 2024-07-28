using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleGetHit : EnemyGetHit
{
    public bool IsImmune = false;

    public override void GetHit(GameObject attack, int damage, Vector2 location, bool destroyedByDeflection = false)
    {
        if (IsImmune)
        {
            if (destroyedByDeflection)
            {
                attack.SetActive(false);
            }
            AudioManager.instance.PlaySound(AudioEnum.Dink);
            Global.prefabManager.InstantiatePrefab(PrefabEnum.DinkEffect, location, Quaternion.identity);
            return;
        }

        OnEnemyHitEvents?.Invoke();

        AudioManager.instance.PlaySound(AudioEnum.EnemyDamaged);

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
