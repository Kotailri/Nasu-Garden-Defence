using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RockStarShooter : PlayerAttack
{
    public float ProjectileSpeed;
    private float spreadAngle = 360f;
    private int numberOfProjectiles = 5;

    public override void DoAttack(Vector2 attackPosition, Transform attachObject = null)
    {
        float angleStep = spreadAngle / numberOfProjectiles;
        float startAngle = Random.Range(0, spreadAngle);

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = startAngle + i * angleStep;
            float angleRad = angle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
            GameObject projectile = Instantiate(AttackPrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = ProjectileSpeed * GlobalStats.GetStatValue(PlayerStatEnum.projectileSpeed) * direction.normalized;
            projectile.GetComponent<PlayerAttackPrefab>().SetDamage(BaseDamage);
            //projectile.GetComponent<PlayerAttackPrefab>().SetKnockback(KnockbackAmount);
        }
    }
}
