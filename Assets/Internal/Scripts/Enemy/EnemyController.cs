using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyScriptable enemyScriptable;

    protected EnemyHealth _healthComponent;
    protected DamagesPlayerOnHit _contactDamageComponent;

    protected void Awake()
    {
        _healthComponent = gameObject.AddComponent<EnemyHealth>();
        _healthComponent.SetEnemyHealth(enemyScriptable.Health, enemyScriptable.Resistance, enemyScriptable.DodgeChance, enemyScriptable.HealthRegenPerSecond);

        _contactDamageComponent = gameObject.AddComponent<DamagesPlayerOnHit>();
        _contactDamageComponent.SetDamage(enemyScriptable.ContactDamage);

        gameObject.AddComponent<EnemyHitbox>();
        gameObject.AddComponent<CallsTriggerCollisions>();
    }
}
