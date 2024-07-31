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
        AudioManager.instance.PlaySound(AudioEnum.Explosion);
        LeanTween.scale(gameObject, scale * GlobalStats.GetStatValue(PlayerStatEnum.explosionRadius), explosionDuration).setEaseOutExpo().setOnComplete(() => { Destroy(gameObject); });
    }

    public virtual void OnEnemyHit(GameObject collisionObject)
    {
        if (explosionDamage >= 0 && collisionObject.TryGetComponent(out EnemyGetHit hit))
        {
            int damage = Mathf.FloorToInt(explosionDamage * GlobalStats.CurrentPlayerDamageMultiplier *
                        GlobalStats.GetStatValue(PlayerStatEnum.damage) * GlobalStats.GetStatValue(PlayerStatEnum.explosionDamage));
            hit.GetHit(gameObject, damage, transform.position);
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
