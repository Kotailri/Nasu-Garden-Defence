using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicShoot : PlayerAttack
{
    public float ProjectileSpeed = 3f;

    public override void DoAttack(Vector2 attackPosition)
    {
        if (AttackPrefab != null)
        {
            GameObject projectile = Instantiate(AttackPrefab, attackPosition + new Vector2(0.25f, -0.25f), Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(ProjectileSpeed + GlobalPlayer.PlayerMovespeed, 0);
            projectile.GetComponent<PlayerAttackPrefab>().SetDamage(BaseDamage);
        }
    }
}
