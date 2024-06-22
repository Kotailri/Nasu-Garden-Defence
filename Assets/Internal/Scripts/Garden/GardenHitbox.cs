using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GardenHealth))]
public class GardenHitbox : MonoBehaviour, IHasTriggerStay
{
    public void OnTriggerStayEvent(GameObject collisionObject)
    {
        if (collisionObject.CompareTag("Enemy"))
        {
            GetComponent<GardenHealth>().RemoveHealth(1);
            collisionObject.gameObject.GetComponent<EnemyDeath>().Die();
        }
    }
}
