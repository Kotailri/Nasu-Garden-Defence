using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplodeDeath : EnemyDeath
{
    public GameObject ExplosionPrefab;
    public int ExplosionDamage;

    public void CallDie()
    {
        Die();
    }

    public override void Die(Vector2? location = null)
    {
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;


        EventManager.TriggerEvent(EventStrings.ENEMY_KILLED, new Dictionary<string, object> {
            { "x", transform.position.x },
            { "y", transform.position.y }
        });

        if (TryGetComponent(out DamagesPlayerOnHit _damagesPlayerOnHit))
        {
            _damagesPlayerOnHit.SetDamage(0);
            _damagesPlayerOnHit.enabled = false;
        }

        GameObject explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        explosion.GetComponent<DamagesPlayerOnHit>().SetDamage(ExplosionDamage);

        Destroy(gameObject);
    }
}
