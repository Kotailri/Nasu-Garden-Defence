using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicEnemyMovement))]
[RequireComponent(typeof(EnemyHealth))]
public class BasicEnemyController : EnemyController
{
    public EnemyMovement _movement;
    public EnemyHealth _health;

    private void Start()
    {
        if (TryGetComponent(out EnemyMovement movement))
        {
            _movement = movement;
            movement.SetMovementType(enemyScriptable.MovementTargetType);
            movement.SetMovementSpeed(enemyScriptable.MovementSpeed);
        }
        else
        {
            print(gameObject.name + " does not have BasicEnemyMovment component");
        }

        if (TryGetComponent(out EnemyHealth health))
        {
            _health = health;
            health.SetEnemyHealth(enemyScriptable.Health, enemyScriptable.Resistance, enemyScriptable.DodgeChance, enemyScriptable.HealthRegenPerSecond);
        }
        else
        {
            print(gameObject.name + " does not have EnemyHealth component");
        }
    }
}
