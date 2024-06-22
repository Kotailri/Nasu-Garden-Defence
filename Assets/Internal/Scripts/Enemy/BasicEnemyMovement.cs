using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : EnemyMovement
{
    private void Update()
    {
        float appliedMovespeed = movespeed - (movespeed * currentSlowAmount);

        switch (currentTargetType)
        {
            case EnemyMovementType.TargetGarden:
                RB.velocity = new Vector3(-1, 0, 0) * appliedMovespeed;
                break;

            case EnemyMovementType.TargetPlayer:
                RB.velocity = (Global.playerTransform.position - transform.position).normalized* appliedMovespeed;
                break;
        }
    }

    
}
