using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GardenHealth))]
public class GardenHitbox : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D _collisionObject)
    {
        GameObject collisionObject = _collisionObject.gameObject;
        if (collisionObject.transform.position.x > -15f)
        {
            return;
        }

        if (collisionObject.TryGetComponent(out EnemyController em))
        {
            if (em.GardenContactDamage > 0)
            {
                GetComponent<GardenHealth>().SetHealth(-em.GardenContactDamage, true);
                AudioManager.instance.PlaySound(AudioEnum.GardenDamaged);
                Destroy(collisionObject);
                return;
            }
        }

        if (collisionObject.TryGetComponent(out BossController boss))
        {
            if (boss.GardenContactDamage > 0)
            {
                GetComponent<GardenHealth>().SetHealth(-boss.GardenContactDamage, true);
                AudioManager.instance.PlaySound(AudioEnum.GardenDamaged);
                Destroy(collisionObject);
                return;
            }
        }
    }
}
