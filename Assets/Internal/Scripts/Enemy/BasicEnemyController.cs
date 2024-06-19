using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicEnemyMovement))]
[RequireComponent(typeof(EnemyHealth))]
public class BasicEnemyController : EnemyController
{
    private EnemyMovement _movement;
    private EnemyHealth _health;

    private void Awake()
    {
        if (TryGetComponent(out EnemyMovement movement))
        {
            _movement = movement;
            movement.SetMovementType(enemyScriptable.MovementTargetType);
            movement.SetMovementSpeed(Random.Range(
                enemyScriptable.MovementSpeed-enemyScriptable.MovementSpeedVariance, 
                enemyScriptable.MovementSpeed + enemyScriptable.MovementSpeedVariance));
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
