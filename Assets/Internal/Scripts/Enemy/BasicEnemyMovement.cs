using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : EnemyMovement
{
    public float MovementUpdateTime = 0.1f;

    private void Start()
    {
        if (currentTargetType == EnemyMovementType.TargetPlayer)
        {
            InvokeRepeating(nameof(StartMovement), 0, MovementUpdateTime);
        }
    }

    protected override void StartMovement()
    {
        float appliedMovespeed = movespeed - (movespeed * currentSlowAmount);
        switch (currentTargetType)
        {
            case EnemyMovementType.TargetGarden:
                RB.velocity = new Vector3(-1, 0, 0) * appliedMovespeed;
                break;

            case EnemyMovementType.TargetPlayer:
                RB.velocity = (Global.playerTransform.position - transform.position).normalized * appliedMovespeed;
                break;
        }
    }
}
