using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicShoot : PlayerAttack
{
    public float ProjectileSpeed = 3f;
    public Vector2 ShootDirection;

    public override void DoAttack(Vector2 attackPosition, Transform attachObject = null)
    {
        if (AttackPrefab != null)
        {
            GameObject projectile = Instantiate(AttackPrefab, attackPosition + new Vector2(0.25f, -0.25f), Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
            projectile.GetComponent<Rigidbody2D>().velocity = (ProjectileSpeed * GlobalPlayer.GetStatValue(PlayerStatEnum.projectileSpeed) + GlobalPlayer.GetStatValue(PlayerStatEnum.movespeed)) * ShootDirection.normalized;
            projectile.GetComponent<PlayerAttackPrefab>().SetDamage(BaseDamage);
            projectile.GetComponent<PlayerAttackPrefab>().SetKnockback(KnockbackAmount);
        }
    }
}
