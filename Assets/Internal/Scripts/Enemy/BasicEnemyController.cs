using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : EnemyController
{
    private EnemyMovement _movement;
    
    private new void Awake()
    {
        base.Awake();
        if (TryGetComponent(out EnemyMovement movement))
        {
            _movement = movement;
            movement.SetMovementType(enemyScriptable.MovementTargetType);
            movement.SetMovementSpeed(enemyScriptable.MovementSpeed);
        }
        else
        {
            print(gameObject.name + " does not have EnemyMovment component");
        }
    }
}
