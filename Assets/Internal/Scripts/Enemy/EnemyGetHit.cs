using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class EnemyGetHit : MonoBehaviour
{
    public UnityEvent OnEnemyHitEvents;

    public virtual void GetHit(AttackModuleInfoContainer info)
    {
        OnEnemyHitEvents?.Invoke();

        AudioManager.instance.PlaySound(AudioEnum.EnemyDamaged);

        if (TryGetComponent(out DamageFlash flash)) {
            flash.DoDamageFlash();
        }

        if (info.Damage > 0)
            info = Managers.Instance.Resolve<IAttackPipelineMng>().ProcessAttackMods(info);
        
        GetComponent<EnemyHealth>().TakeDamage(info);
    }
}
