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
            AttackModuleInfoContainer info = new(Mathf.RoundToInt(explosionDamage), PlayerAttackType.Explosion, gameObject, transform.position);
            hit.GetHit(info);
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
