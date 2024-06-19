using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenHitbox : MonoBehaviour, IHasTriggerStay
{
    public void OnTriggerStayEvent(GameObject collisionObject)
    {
        if (collisionObject.CompareTag("Enemy"))
        {
            Global.GameOver();
        }
    }
}
