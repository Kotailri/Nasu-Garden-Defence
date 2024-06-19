using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyHitbox : MonoBehaviour, IHasTriggerEnter
{
    public void OnTriggerEnterEvent(GameObject collisionObject)
    {
        if (collisionObject.TryGetComponent(out DamagesEnemies de))
        {
            GetComponent<EnemyHealth>().TakeDamage(de.GetDamage());
        }
    }
}
