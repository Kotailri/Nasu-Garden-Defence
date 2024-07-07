using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public virtual void Die() // children of EnemyDeath should call base.Die()
    {
        EventManager.TriggerEvent(EventStrings.ENEMY_DELETED, null);
        GetComponent<Collider2D>().enabled = false;
        if (TryGetComponent(out EnemyMovement movement))
        {
            movement.DisableMovement();
            movement.DoKnockback(7);
        }
        LeanTween.alpha(gameObject, 0, 0.5f).setOnComplete(() => { Destroy(gameObject); });
        
    }
}
