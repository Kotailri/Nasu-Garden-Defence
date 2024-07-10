using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GardenHealth))]
public class GardenHitbox : MonoBehaviour, IHasTriggerStay
{
    public void OnTriggerStayEvent(GameObject collisionObject)
    {
        print(collisionObject.name);

        if (collisionObject.TryGetComponent(out EnemyController em))
        {
            if (em.GardenContactDamage > 0)
            {
                GetComponent<GardenHealth>().SetHealth(-em.GardenContactDamage, true);
                Destroy(collisionObject);
                return;
            }
        }

        if (collisionObject.TryGetComponent(out BossController boss))
        {
            if (boss.GardenContactDamage > 0)
            {
                GetComponent<GardenHealth>().SetHealth(-boss.GardenContactDamage, true);
                Destroy(collisionObject);
                return;
            }
        }
    }
}
