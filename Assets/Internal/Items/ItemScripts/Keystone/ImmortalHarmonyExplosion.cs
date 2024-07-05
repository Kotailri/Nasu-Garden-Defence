using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ImmortalHarmonyExplosion : Explosion
{
    private int healAmount;

    public void Initialize(float _explosionDamage, int _healAmount)
    {
        healAmount = _healAmount;

        Initialize(_explosionDamage);
    }

    public override void OnEnemyHit(GameObject collisionObject)
    {
        if (collisionObject.TryGetComponent(out EnemyGetHit hit))
        {
            base.OnEnemyHit(collisionObject);
            Global.playerTransform.gameObject.GetComponent<PlayerHealth>().SetHealth(healAmount, true);
        }
    }

   
}
