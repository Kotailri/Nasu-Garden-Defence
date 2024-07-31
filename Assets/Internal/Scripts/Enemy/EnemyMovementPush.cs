using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementPush : EnemyMovement
{
    public int PushFrame;
    public float PushForce;
    public float PushTimeLength;

    private float savedSpeed;
    private Animator anim;
    private bool hasPushedThisFrame = false;

    private void Start()
    {
        savedSpeed = movespeed;
        anim = GetComponent<Animator>();
        SetMovementSpeed(0);
    }


    protected override void Update()
    {
        if (isMovementStarted)
        {
            if (GameUtil.GetCurrentAnimationFrame(anim) == PushFrame)
            {
                Push();
            }
            else
            {
                hasPushedThisFrame = false;
            }
        }
        base.Update();
    }

    private void Push()
    {
        if (hasPushedThisFrame == false) 
        {
            hasPushedThisFrame = true;
            RB.velocity = Vector2.zero;
            LeanTween.value(gameObject, savedSpeed, 0, PushTimeLength).setOnUpdate((float speed) => 
            {
                if (!_movementDisabled)
                {
                    appliedSpeed = speed - (speed * currentSlowAmount);
                    appliedSpeed *= Global.EnemySpeedMultiplier;
                    RB.velocity = (new Vector2(-1, 0) * speed * appliedSpeed) + appliedPullForce;
                }
            });
        }
    }
}
