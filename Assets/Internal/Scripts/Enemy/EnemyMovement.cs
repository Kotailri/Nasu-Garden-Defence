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

    protected bool isMovementStarted = false;

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

    protected abstract void StartMovement();

    protected virtual void RestartMovement()
    {
        if (isBeingPulled)
        {
            return;
        }

        StartMovement();
    }

    public virtual void DoKnockback(float force)
    {
        if (isMovementStarted && force > 0f)
        {
            RB.AddForce(new Vector2(force, 0), ForceMode2D.Impulse);
            StartCoroutine(KnockbackTime());
        }

        IEnumerator KnockbackTime()
        {
            yield return new WaitForSeconds(force/25f);
            RestartMovement();
        }
    }

    protected virtual void Update()
    {
        if (!isMovementStarted && transform.position.x > 18.5f)
        {
            transform.position += new Vector3(-Time.deltaTime, 0, 0);
        }
        else if (!isMovementStarted)
        {
            isMovementStarted = true;
            StartMovement();
        }
    }

    protected bool isBeingPulled = false;

    public void ApplyMovementPull(Vector2 pullForce)
    {
        RestartMovement();
        isBeingPulled = true;
        RB.velocity += pullForce;
    }

    public void RemoveMovementPull()
    {
        isBeingPulled = false;
        RestartMovement();
    }

    public void ApplyMovementSlow(float slowAmount, float slowTime)
    {
        if (slowAmount < currentSlowAmount) { return; }

        StopAllCoroutines();
        StartCoroutine(MovementSlowTimer());
        IEnumerator MovementSlowTimer()
        {
            currentSlowAmount = slowAmount;
            RestartMovement();
            GetComponent<SpriteRenderer>().color = Color.blue;

            yield return new WaitForSeconds(slowTime);

            currentSlowAmount = 0;
            RestartMovement();
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
