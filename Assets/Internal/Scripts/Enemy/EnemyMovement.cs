using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BasicEnemyMovementType
{
    TargetGarden,
    TargetPlayer,
    TargetPlayerForwards,
    TargetGardenAvoidPlayer
}

public abstract class EnemyMovement : MonoBehaviour
{
    protected Transform Player;
    protected Rigidbody2D RB;

    protected float movespeed;
    protected float currentSlowAmount = 0f;
    protected BasicEnemyMovementType currentTargetType;

    protected bool isMovementStarted = false;
    protected float appliedSpeed = 0;
    protected bool isBeingKnockedBack = false;
    protected Vector2 appliedDirection = Vector2.zero;
    protected Vector2 appliedPullForce = Vector2.zero;
    public bool CanBeKnockedBack = true;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (!isMovementStarted && transform.position.x > 18.5f)
        {
            transform.position += new Vector3(-Time.deltaTime * Global.WaveSpeed, 0, 0);
            return;
        }
        else if (!isMovementStarted)
        {
            isMovementStarted = true;
            UpdateAppliedMovementDirection();
            UpdateAppliedMovementSpeed();

            if (TryGetComponent(out Animator animator))
                animator.speed = 1f;

            StartMovement();
        }

        if (isMovementStarted && !isBeingKnockedBack && !_movementDisabled)
        {
            ReapplyMovement();
        }
        
        if (isMovementStarted && transform.position.x > 17.5f)
        {
            transform.position = new Vector3(17.5f, transform.position.y, transform.position.z);
        }
    }

    public virtual void ReapplyMovement()
    {
        RB.velocity = (appliedDirection.normalized * appliedSpeed) + appliedPullForce;
    }

    public virtual void StartMovement()
    {
        RB.velocity = (appliedDirection.normalized * appliedSpeed) + appliedPullForce;
    }

    public void SetMovementType(BasicEnemyMovementType movementType)
    {
        currentTargetType = movementType;
    }

    public void SetMovementSpeed(float speed)
    {
        movespeed = speed;
    }

    public virtual void UpdateAppliedMovementSpeed()
    {
        appliedSpeed = movespeed - (movespeed * currentSlowAmount);
        appliedSpeed *= Global.EnemySpeedMultiplier;
    }

    public virtual void UpdateAppliedMovementDirection()
    {
        appliedDirection = new Vector2(-1,0);
    }

    public virtual void DoKnockback(float force, float knockbackTime=0f,Vector2? _direction=null)
    {
        if (!CanBeKnockedBack)
            return;

        Vector2 direction = Vector2.right;
        if (_direction != null)
        {
            direction = (Vector2)_direction;
        }
        direction = direction.normalized;


        if (isMovementStarted && force > 0f && !isBeingKnockedBack)
        {
            DisableMovement();
            StartCoroutine(KnockbackTime(knockbackTime==0? (force/25f) : knockbackTime));
            RB.velocity = Vector2.zero;
            RB.AddForce(force * direction, ForceMode2D.Impulse);
        }

        IEnumerator KnockbackTime(float knockbackTime)
        {
            isBeingKnockedBack = true;
            float defaultFriction = RB.drag;
            RB.drag = 0;
            yield return new WaitForSeconds(knockbackTime);
            isBeingKnockedBack = false;
            RB.drag = defaultFriction;
            RB.velocity = Vector2.zero;

            EnableMovement();
            StartMovement();
        }
    }

    protected bool _movementDisabled = false;
    public virtual void DisableMovement()
    {
        _movementDisabled = true;
    }

    public virtual void EnableMovement()
    {
        _movementDisabled = false;
    }

    public void ApplyMovementPull(Vector2 pullForce)
    {
        appliedPullForce += pullForce;
    }

    public void RemoveMovementPull(Vector2 pullForce)
    {
        appliedPullForce -= pullForce;
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
