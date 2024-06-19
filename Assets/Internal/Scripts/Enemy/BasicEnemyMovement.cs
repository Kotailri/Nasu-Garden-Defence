using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour, EnemyMovement
{
    private Transform Player;

    private float movespeed;
    private EnemyMovementType currentTargetType;

    public void SetMovementType(EnemyMovementType movementType)
    {
        currentTargetType = movementType;
    }

    public void SetMovementSpeed(float speed)
    {
        movespeed = speed;
    }

    private void Update()
    {
        switch (currentTargetType)
        {
            case EnemyMovementType.TargetGarden:
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - 100, transform.position.y), movespeed * Time.deltaTime);
                break;

            case EnemyMovementType.TargetPlayer:
                transform.position = Vector2.MoveTowards(transform.position, Global.playerTransform.position, movespeed * Time.deltaTime);
                break;
        }
    }
}
