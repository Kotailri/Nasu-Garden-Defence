using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleFur : MonoBehaviour
{
    private Vector3 originalLocalScale = Vector3.one;
    public float SlowAmount, DamageTimer, Duration;
    public int Damage;

    [Space(5.0f)]
    public float DetectionRadius;
    public LayerMask EnemyLayer;

    private void Awake()
    {
        originalLocalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        LeanTween.scale(gameObject, originalLocalScale, 0.25f);
        StartCoroutine(DespawnFur());
    }

    private IEnumerator DespawnFur()
    {
        yield return new WaitForSeconds(Duration);
        LeanTween.alpha(gameObject, 0, 0.25f).setOnComplete(() => { Destroy(gameObject); });
    }

    public void SetParams(float _FurDuration, float _SlowAmount, float _DamageTimer, int _Damage)
    {
        SlowAmount = _SlowAmount;
        DamageTimer = _DamageTimer;
        Damage = _Damage;

        Duration = _FurDuration;

        InvokeRepeating(nameof(SlowAndDamageEnemies), 0, DamageTimer);
    }

    private List<Collider2D> colidingEnemies = new();

    private void SlowAndDamageEnemies()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, DetectionRadius, EnemyLayer))
        {
            if (!collider.gameObject.CompareTag("Enemy"))
                return;

            if (!collider.isTrigger)
                return;

            if (collider.gameObject.TryGetComponent(out EnemyMovement movement))
            {
                movement.ApplyMovementSlow(SlowAmount, 1f);
            }

            if (collider.gameObject.TryGetComponent(out EnemyGetHit hit))
            {
                hit.GetHit(Damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DetectionRadius);
    }
}
