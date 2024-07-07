using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : ExplosionEffect
{
    public float explosionDamage;

    public void Initialize(float _explosionDamage)
    {
        explosionDamage = _explosionDamage;

        DoExplosionEffect();
    }

    protected override void Start() { }

    protected override void DoExplosionEffect()
    {
        LeanTween.scale(gameObject, scale * GlobalPlayer.GetStatValue(PlayerStatEnum.explosionRadius), explosionDuration).setEaseOutExpo().setOnComplete(() => { Destroy(gameObject); });
    }

    public virtual void OnEnemyHit(GameObject collisionObject)
    {
        if (explosionDamage >= 0 && collisionObject.TryGetComponent(out EnemyGetHit hit))
        {
            int damage = Mathf.FloorToInt(explosionDamage * GlobalPlayer.CurrentPlayerDamageMultiplier *
                        GlobalPlayer.GetStatValue(PlayerStatEnum.damage) * GlobalPlayer.GetStatValue(PlayerStatEnum.explosionDamage));
            hit.GetHit(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collision.CompareTag("Enemy"))
        {
            OnEnemyHit(collisionObject);
        }
    }
}