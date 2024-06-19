using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour, IHasTriggerEnter
{
    public void OnTriggerEnterEvent(GameObject collisionObject)
    {
        if (collisionObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= Global.MaxX)
        {
            Destroy(gameObject);
        }
    }
}
