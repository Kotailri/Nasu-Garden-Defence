using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GardenHealth))]
public class GardenHitbox : MonoBehaviour, IHasTriggerStay
{
    public void OnTriggerStayEvent(GameObject collisionObject)
    {
        if (collisionObject.TryGetComponent(out EnemyController em))
        {
            if (em.GardenContactDamage > 0)
            {
                GetComponent<GardenHealth>().SetHealth(-em.GardenContactDamage, true);
                Destroy(collisionObject);
            }
        }
    }
}
