using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : EnemyMovement
{

    protected override void Update()
    {
        if (currentTargetType != BasicEnemyMovementType.TargetGarden)
        {
            UpdateAppliedMovementDirection();
        }

        base.Update();
    }

    public override void UpdateAppliedMovementDirection()
    {
        switch (currentTargetType)
        {
            case BasicEnemyMovementType.TargetGarden:
                appliedDirection = new Vector3(-1, 0, 0);
                break;

            case BasicEnemyMovementType.TargetPlayer:
                appliedDirection = (Global.playerTransform.position - transform.position).normalized;
                break;

            case BasicEnemyMovementType.TargetPlayerForwards:
                if (Global.playerTransform.position.x < transform.position.x)
                {
                    appliedDirection = (Global.playerTransform.position - transform.position).normalized;
                }
                else
                {
                    appliedDirection = new Vector3(-1, (Global.playerTransform.position - transform.position).normalized.y, 0);
                }
                break;

            case BasicEnemyMovementType.TargetGardenAvoidPlayer:
                appliedDirection = new Vector3(-1, -(Global.playerTransform.position - transform.position).normalized.y, 0);
                break;
        }
    }
}
