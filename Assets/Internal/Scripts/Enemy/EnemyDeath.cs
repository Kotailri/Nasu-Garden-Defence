using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public virtual void Die(Vector2? location = null) // children of EnemyDeath should call base.Die()
    {
        GetComponent<Collider2D>().enabled = false;
        if (TryGetComponent(out EnemyMovement movement))
        {
            movement.DisableMovement();

            if (location != null)
            {
                movement.DoKnockback(5, ((Vector2)transform.position - (Vector2)location));
            }
            else
            {
                movement.DoKnockback(5);
            }
            
        }
        LeanTween.alpha(gameObject, 0, 0.5f).setOnComplete(() => { Destroy(gameObject); });
        
    }
}
