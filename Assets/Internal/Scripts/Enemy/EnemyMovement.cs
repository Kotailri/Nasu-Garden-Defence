using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyMovement
{
    public void SetMovementType(EnemyMovementType movementType);

    public void SetMovementSpeed(float speed);
}
