using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFacingDirection : MonoBehaviour
{
    private Animator animator;
    private bool isTurned = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Global.playerTransform.position.x < transform.position.x && isTurned == true)
        {
            isTurned = false;
            animator.SetBool("isTurned", isTurned);
        }
        else 
        if (Global.playerTransform.position.x > transform.position.x && isTurned == false) 
        {
            isTurned = true;
            animator.SetBool("isTurned", isTurned);
        }
    }
}
