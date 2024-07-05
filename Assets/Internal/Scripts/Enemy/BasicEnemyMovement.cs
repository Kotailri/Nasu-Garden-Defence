using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : EnemyMovement
{

    protected override void Update()
    {
        if (currentTargetType == EnemyMovementType.TargetPlayer)
        {
            UpdateAppliedMovementDirection();
        }

        base.Update();
    }

    public override void UpdateAppliedMovementDirection()
    {
        switch (currentTargetType)
        {
            case EnemyMovementType.TargetGarden:
                appliedDirection = new Vector3(-1, 0, 0);
                break;

            case EnemyMovementType.TargetPlayer:
                appliedDirection = (Global.playerTransform.position - transform.position).normalized;
                break;
        }
    }
}
