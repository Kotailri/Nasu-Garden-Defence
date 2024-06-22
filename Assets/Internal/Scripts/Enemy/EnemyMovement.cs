using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    protected Transform Player;
    protected Rigidbody2D RB;

    protected float movespeed;
    protected float currentSlowAmount = 0f;
    protected EnemyMovementType currentTargetType;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    public void SetMovementType(EnemyMovementType movementType)
    {
        currentTargetType = movementType;
    }

    public void SetMovementSpeed(float speed)
    {
        movespeed = speed;
    }

    public void ApplyMovementSlow(float slowAmount, float slowTime)
    {
        if (slowAmount < currentSlowAmount) { return; }

        StopAllCoroutines();
        StartCoroutine(MovementSlowTimer());
        IEnumerator MovementSlowTimer()
        {
            currentSlowAmount = slowAmount;
            GetComponent<SpriteRenderer>().color = Color.blue;

            yield return new WaitForSeconds(slowTime);

            currentSlowAmount = 0;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
