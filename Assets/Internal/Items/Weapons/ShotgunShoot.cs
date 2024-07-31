using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunShoot : PlayerAttack
{
    [Header("Shotgun")]
    public float ProjectileSpeed = 5f;
    public int numberOfProjectiles;
    public float coneAngle = 30f;

    public override void DoAttack(Vector2 attackPosition, Transform attachObject = null)
    {
        if (AttackPrefab != null)
        {
            float angleStep = coneAngle / (numberOfProjectiles - 1);
            float startingAngle = -coneAngle / 2;

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                float currentAngle = startingAngle + i * angleStep;
                CreateProjectile(currentAngle, attackPosition);
            }
        }
    }

    private void CreateProjectile(float angle, Vector2 attackPosition)
    {
        GameObject projectile = Instantiate(AttackPrefab, attackPosition, transform.rotation);
        projectile.transform.Rotate(0, 0, angle-90f);
        projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up * (ProjectileSpeed 
            * GlobalStats.GetStatValue(PlayerStatEnum.projectileSpeed) + GlobalStats.GetStatValue(PlayerStatEnum.movespeed));
        projectile.GetComponent<PlayerAttackPrefab>().SetDamage(BaseDamage);
        projectile.GetComponent<PlayerAttackPrefab>().SetKnockback(KnockbackAmount);
    }
}
