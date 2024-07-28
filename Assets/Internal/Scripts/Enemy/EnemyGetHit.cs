using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGetHit : MonoBehaviour
{
    public UnityEvent OnEnemyHitEvents;

    public virtual void GetHit(GameObject attack, int damage, Vector2 location, bool destroyedByDeflection=false)
    {
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

        if (TryGetComponent(out DamageFlash flash)) {
            flash.DoDamageFlash();
        }
        GetComponent<EnemyHealth>().TakeDamage(newDamage, location);
    }
}
