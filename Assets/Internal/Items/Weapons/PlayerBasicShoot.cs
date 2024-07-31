using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicShoot : PlayerAttack
{
    [Header("Basic Shoot")]
    public float ProjectileSpeed = 3f;
    public Vector2 ShootDirection;

    [Space(5f)]
    [Range(1,10)]
    public int numberOfProjectiles;
    public float delayBetweenProjectiles;

    private Vector2 _attackPosition;

    public override void DoAttack(Vector2 attackPosition, Transform attachObject = null)
    {
        _attackPosition = attackPosition;

        if (AttackPrefab != null)
        {
            if (numberOfProjectiles > 1)
            {
                StartCoroutine(ShootTimer());
            }
            else
            {
                Shoot();
            }
        }
    }

    protected virtual void Shoot()
    {
        GameObject projectile = Instantiate(AttackPrefab, _attackPosition + new Vector2(0.25f, -0.25f), Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        projectile.GetComponent<Rigidbody2D>().velocity = (ProjectileSpeed * GlobalStats.GetStatValue(PlayerStatEnum.projectileSpeed) + GlobalStats.GetStatValue(PlayerStatEnum.movespeed)) * ShootDirection.normalized;
        projectile.GetComponent<PlayerAttackPrefab>().SetDamage(BaseDamage);
        projectile.GetComponent<PlayerAttackPrefab>().SetKnockback(KnockbackAmount);
        AudioManager.instance.PlaySound(AttackSound);
    }

    private IEnumerator ShootTimer()
    {
        for (int i = 0; i  < numberOfProjectiles; i++) 
        {
            Shoot();
            yield return new WaitForSeconds(delayBetweenProjectiles);
        }
    }
}
