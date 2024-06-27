using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackPrefab : MonoBehaviour, IHasTriggerEnter
{
    public bool DestroyWhenOutside;
    public bool DestroyOnContact;
    private int Damage;

    private void Awake()
    {
        gameObject.AddComponent<CallsTriggerCollisions>();
    }

    public void SetDamage(int damage)
    {
        Damage = damage;
    }

    public void OnTriggerEnterEvent(GameObject collisionObject)
    {
        if (collisionObject.CompareTag("Enemy"))
        {
            if (collisionObject.TryGetComponent(out EnemyGetHit hit))
            {
                int damage = Mathf.FloorToInt(Damage * GlobalPlayer.PlayerDamageAmp);
                hit.GetHit(damage);
            }

            if (DestroyOnContact)
            {
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        if (DestroyWhenOutside && transform.position.x >= Global.MaxX)
        {
            Destroy(gameObject);
        }
    }
}
