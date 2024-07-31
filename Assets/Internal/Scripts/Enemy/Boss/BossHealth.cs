using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{

    private void Awake()
    {
        canGetExecuted = false;
    }

    public override void TakeDamage(int damage, Vector2 location)
    {
        if (Managers.Instance.Resolve<IBossHealthBarMng>().IsBarLoaded())
        {
            base.TakeDamage(damage, location);
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
