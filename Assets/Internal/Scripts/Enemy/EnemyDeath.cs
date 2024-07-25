using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public virtual void Die(Vector2? location = null) // children of EnemyDeath should call base.Die()
    {
        EventManager.TriggerEvent(EventStrings.ENEMY_KILLED, new Dictionary<string, object> {
            { "x", transform.position.x },
            { "y", transform.position.y }
        });

        if (TryGetComponent(out DamagesPlayerOnHit _damagesPlayerOnHit))
        {
            _damagesPlayerOnHit.SetDamage(0);
        }
        
        if (TryGetComponent(out EnemyMovement movement))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            movement.DisableMovement();

            if (location != null)
            {
                movement.DoKnockback(5, PlayerKnockbackType.None,0, (Vector2)transform.position - (Vector2)location);
            }
            else
            {
                movement.DoKnockback(5);
            }
            
        }
        LeanTween.alpha(gameObject, 0, 0.5f).setOnComplete(() => { Destroy(gameObject); });
        
    }
}
