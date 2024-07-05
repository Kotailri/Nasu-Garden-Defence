using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float explosionDamage;
    private float explosionDuration = 0.5f;
    protected Vector3 scale;

    private void Awake()
    {
        scale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    public void Initialize(float _explosionDamage)
    {
        explosionDamage = _explosionDamage;

        LeanTween.scale(gameObject, scale * GlobalPlayer.GetStatValue(PlayerStatEnum.explosionRadius), explosionDuration).setEaseOutExpo().setOnComplete(() => { Destroy(gameObject); });
    }

    public virtual void OnEnemyHit(GameObject collisionObject)
    {
        if (collisionObject.TryGetComponent(out EnemyGetHit hit))
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
