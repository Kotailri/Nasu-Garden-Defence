using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterCoat : MonoBehaviour
{
    public float SlowAmount;
    public float SlowTime;

    private List<Collider2D> colidingEnemies = new();

    public void ActivateSlow(float slowAmount, float slowTime)
    {
        SlowAmount = slowAmount;
        SlowTime = slowTime;
        InvokeRepeating(nameof(SlowEnemies), 0, SlowTime);
    }

    private void SlowEnemies()
    {
        foreach (Collider2D collider in colidingEnemies)
        {
            if (collider.gameObject.TryGetComponent(out EnemyMovement movement))
            {
                movement.ApplyMovementSlow(SlowAmount, SlowTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;

        if (!colidingEnemies.Contains(other))
        {
            colidingEnemies.Add(other);
        }

        SlowEnemies();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;

        if (!colidingEnemies.Contains(other))
        {
            colidingEnemies.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
            return;

        if (colidingEnemies.Contains(other))
        {
            colidingEnemies.Remove(other);
        }

        SlowEnemies();
    }
}
