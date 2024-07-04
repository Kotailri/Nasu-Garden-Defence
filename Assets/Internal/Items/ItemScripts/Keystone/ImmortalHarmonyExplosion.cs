using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ImmortalHarmonyExplosion : MonoBehaviour
{
    private int healAmount;
    private float explosionDamage; 
    public float explosionDuration;
    private Vector3 scale;

    private void Awake()
    {
        scale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    public void Initialize(float _explosionDamage, int _healAmount)
    {
        explosionDamage = _explosionDamage;
        healAmount = _healAmount;

        LeanTween.scale(gameObject, scale * GlobalPlayer.GetStatValue(PlayerStatEnum.explosionRadius), explosionDuration).setEaseOutExpo().setOnComplete(() => { Destroy(gameObject); });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collision.CompareTag("Enemy"))
        {
            if (collisionObject.TryGetComponent(out EnemyGetHit hit))
            {
                int damage = Mathf.FloorToInt(explosionDamage * GlobalPlayer.CurrentPlayerDamageMultiplier *
                            GlobalPlayer.GetStatValue(PlayerStatEnum.damage) * GlobalPlayer.GetStatValue(PlayerStatEnum.explosionDamage));
                hit.GetHit(damage);
                Global.playerTransform.gameObject.GetComponent<PlayerHealth>().SetHealth(healAmount, true);
            }
        }
    }
}
