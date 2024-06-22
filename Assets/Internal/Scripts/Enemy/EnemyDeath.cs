using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public void Die() // children of EnemyDeath should call base.Die()
    {
        EventManager.TriggerEvent(EventStrings.ENEMY_KILLED, null);
        Destroy(gameObject);
    }
}
