using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{

    protected override void Awake()
    {
        canGetExecuted = false;
        base.Awake();
    }

    public override void TakeDamage(AttackModuleInfoContainer info)
    {
        if (Managers.Instance.Resolve<IBossHealthBarMng>().IsBarLoaded())
        {
            base.TakeDamage(info);
        }
    }

    protected override void CheckHealth(Vector2? location = null)
    {
        if (CurrentHealth > Health)
        {
            CurrentHealth = Health;
        }

        Managers.Instance.Resolve<IBossHealthBarMng>()?.UpdateHealthBar((float)CurrentHealth / (float)Health);

        if (CurrentHealth <= 0)
        {
            GetComponent<BossDeath>().Die();
        }
    }
}
