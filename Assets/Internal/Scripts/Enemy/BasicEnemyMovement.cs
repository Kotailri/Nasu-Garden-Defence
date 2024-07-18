using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : EnemyMovement
{

    protected override void Update()
    {
        if (currentTargetType == BasicEnemyMovementType.TargetPlayer)
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
        }
    }
}
